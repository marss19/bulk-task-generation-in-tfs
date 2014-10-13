using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Marss.TasksGenerator.BLL.TaskTemplates;
using System.Data;
using System.Web;

namespace Marss.TasksGenerator.BLL.TFS
{
    public class TfsDataProvider
    {
        public string ServerName { get; private set; }
        public string ProjectName { get; private set; }

        public TfsDataProvider(WorkItemStore store, string projectName, string serverName)
        {
            ServerName = serverName;
            ProjectName = projectName;

            _store = store;
            _project = store.Projects[projectName];
            _taskType = _project.WorkItemTypes[TfsConstants.WorkItemTypes.Task];
            _hierarchyLinkType = store.WorkItemLinkTypes[TfsConstants.WorkItemLinkTypes.Hierarchy];

            var task = _taskType.NewWorkItem();
            OriginalEstimateFieldName = task.Fields.Contains(TfsConstants.Fields.OriginalEstimate) ? task.Fields[TfsConstants.Fields.OriginalEstimate].Name : "Original estimate";
            RemainingWorkFieldName = task.Fields.Contains(TfsConstants.Fields.RemainingWork) ? task.Fields[TfsConstants.Fields.RemainingWork].Name : "Remaining work";
            CompletedWorkFieldName = task.Fields.Contains(TfsConstants.Fields.CompletedWork) ? task.Fields[TfsConstants.Fields.CompletedWork].Name : "Completed work";
        }

        public WorkItemType TaskType
        {
            get { return _taskType; }
        }

        public string OriginalEstimateFieldName { get; private set; }
        public string RemainingWorkFieldName { get; private set; }
        public string CompletedWorkFieldName { get; private set; }

        public List<string> GetAllowedValues(string fieldName)
        {
            return GetAllowedValues(_taskType.NewWorkItem(), fieldName);
        }

        public List<string> GetAllowedValues(object item, string fieldName)
        {
            var values = new List<string>();
            var workItem = (WorkItem)item;
            foreach (string field in workItem.Fields[fieldName].AllowedValues)
            {
                values.Add(field);
            }
            return values;
        }

        public Dictionary<int, string> GetAreas()
        {
            var dic = new Dictionary<int, string>();
            foreach (Node node in _project.AreaRootNodes)
            {
                dic.Add(node.Id, node.Path);
                AddChildNodes(dic, node);
            }
            return dic;
        }

        public Dictionary<int, string> GetIterations()
        {
            var dic = new Dictionary<int, string>();
            foreach (Node node in _project.IterationRootNodes)
            {
                if (!dic.ContainsKey(node.Id))
                {
                    dic.Add(node.Id, node.Path);
                    AddChildNodes(dic, node);
                }
            }
            return dic;
        }

        public TreeItem[] GetWorkItemsAsTreeByWorkItem(int workItemId)
        {
            string linkQueryText = string.Format(@"select [System.Id], [System.WorkItemType], [System.Title] 
from WorkItemLinks 
where Source.[System.TeamProject] = '{0}' 
    and Source.[System.Id] = '{1}' 
    and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward'
    and Target.[System.WorkItemType] <> '' 
order by [System.Id] mode (Recursive)",
                ProjectName,
                workItemId
                );

            var q = new Query(_store, linkQueryText);
            _links = q.RunLinkQuery().ToList();


            int[] workItemIds = _links.Select(x => x.TargetId).ToArray();
            _cachedWorkItems = _store.Query(workItemIds, @"select [System.Id], [System.WorkItemType], [System.Title], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.OriginalEstimate], [Microsoft.VSTS.Scheduling.RemainingWork]  from WorkItems");

            return GetNodes(null);
        }

        public TreeItem[] GetWorkItemsAsTreeByAreaAndIteration(int? areaId, int? iterationId)
        {
            areaId = areaId > 0 ? areaId : null;
            iterationId = iterationId > 0 ? iterationId : null;

            _cachedWorkItems = _store.Query(string.Format("select [System.Id], [System.WorkItemType], [System.Title], [Microsoft.VSTS.Scheduling.CompletedWork], [Microsoft.VSTS.Scheduling.OriginalEstimate], [Microsoft.VSTS.Scheduling.RemainingWork] from WorkItems where ([System.TeamProject] = '{0}' {1} {2}) ",
                ProjectName,
                areaId.HasValue ? string.Format(" and [System.AreaId] = " + areaId) : string.Empty,
                iterationId.HasValue ? string.Format(" and [System.IterationId] = " + iterationId) : string.Empty
                ));


            string linkQueryText = string.Format(@"select [System.Id], [System.WorkItemType], [System.Title] 
from WorkItemLinks 
where Source.[System.TeamProject] = '{0}' {1} {2}  
    and [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward'
    and Target.[System.WorkItemType] <> '' 
order by [System.Id] mode (Recursive)",
                ProjectName,
                areaId.HasValue ? string.Format(" and Source.[System.AreaId] = " + areaId) : string.Empty,
                iterationId.HasValue ? string.Format(" and Source.[System.IterationId] = " + iterationId) : string.Empty
                );

            var q = new Query(_store, linkQueryText);
            _links = q.RunLinkQuery().ToList();

            return GetNodes(null);
        }


        public void AddTasks(TaskTemplate _template, System.Data.DataTable data, int? defaultParentWorkItemId)
        {
            var validTasks = new List< Tuple<WorkItem, WorkItem>>();
            var defaultParentWorkItem = defaultParentWorkItemId.HasValue
                ? GetWorkItemFromCache(defaultParentWorkItemId.Value)
                : null;

            foreach (DataRow row in data.Rows)
            {
                string parentWorkItemId = null;
                var task = _taskType.NewWorkItem();
                foreach (DataColumn column in data.Columns)
                {
                    if (column.ColumnName == TfsConstants.Fields.ParentWorkItemId)
                    {
                        if (row[column.ColumnName] != DBNull.Value)
                            parentWorkItemId = row[column.ColumnName].ToString();
                        continue;
                    }

                    task.Fields[column.ColumnName].Value = row[column.ColumnName];
                }

                var result = task.Validate();
                if (result.Count != 0)
                {
                    foreach (Field field in result)
                    {
                        if (!field.IsValid)
                        {
                            throw new Exception(string.Format("Invalid field value. Field: {0}, value: {1}, status: {2}",
                                                    field.Name, field.Value, field.Status));
                        }
                    }
                }

                WorkItem ownParentWorkitem = null;
                if (!string.IsNullOrEmpty(parentWorkItemId))
                {
                    int id;
                    if (!int.TryParse(parentWorkItemId, out id))
                    {
                        throw new Exception(string.Format("Invalid value of parent workitem by ID: \"{0}\" for task: {1}", parentWorkItemId, task.Title));
                    }

                    ownParentWorkitem = GetWorkItem(id);
                    if (ownParentWorkitem == null)
                    {
                        throw new Exception(string.Format("Unable to find parent workitem by ID: {0} for task: {1}", parentWorkItemId, task.Title));
                    }
                }

                if (ownParentWorkitem == null && defaultParentWorkItem == null)
                {
                    throw new Exception(string.Format("Unable to find parent workitem for task: {0}", task.Title));
                }

                validTasks.Add(new Tuple<WorkItem, WorkItem>( task, ownParentWorkitem));
            }

            foreach (var pair in validTasks)
            {
                var task = pair.Item1;
                var parent = pair.Item2 ?? (WorkItem)defaultParentWorkItem;
                task.AreaId = parent.AreaId;
                task.IterationId = parent.IterationId;
                task.Links.Add(new WorkItemLink(_hierarchyLinkType.ReverseEnd, parent.Id));
                task.Save();
            }
        }

        public void UpdateWorkitem(object item, string title, string state, Dictionary<string, object> fieldValues)
        {
            var workItem = ((WorkItem)item);
            workItem.Open();
            workItem.Title = title;
            workItem.State = state;
            foreach (var pair in fieldValues)
            {
                workItem.Fields[pair.Key].Value = pair.Value;
            }
            workItem.Save();

        }

        public string GetWorkitemDescriptionHtml(int itemId)
        {
            var workItem = GetWorkItemFromCache(itemId);

            string description = string.Empty;

            if (workItem.Fields.Contains(TfsConstants.Fields.DescriptionHtml))
                description = (string)workItem.Fields[TfsConstants.Fields.DescriptionHtml].Value;

            if (string.IsNullOrWhiteSpace(description))
                description = HttpUtility.HtmlEncode(workItem.Description).Replace(Environment.NewLine, "<br/>");

            return description;
        }

        public bool MoveWorkItem(int movedItemId, int newParentItemId)
        {
            var movedWorkitem = GetWorkItemFromCache(movedItemId);
            var newParentWorkitem = GetWorkItemFromCache(newParentItemId);

            if (movedWorkitem == null || newParentWorkitem == null)
                return false;

           
            for (var i = 0; i < movedWorkitem.Links.Count; i++)
            {
                var relatedLink = movedWorkitem.Links[i] as RelatedLink;
                if (relatedLink != null && relatedLink.LinkTypeEnd.Name == "Parent")
                {
                    if (relatedLink.RelatedWorkItemId == newParentWorkitem.Id)
                        return false;

                    movedWorkitem.Links.Remove (relatedLink);
                    break;
                }
            }

            movedWorkitem.Links.Add(new WorkItemLink(_hierarchyLinkType.ReverseEnd, newParentWorkitem.Id));
            movedWorkitem.Save();
            return true;
        }

        public bool HasField(object item, string fieldName)
        {
            var workItem = (WorkItem)item;
            return workItem.Fields.Contains(fieldName);
        }

        public string GetFieldCaption(object item, string fieldName)
        {
            var workItem = (WorkItem)item;
            return workItem.Fields.Contains(fieldName) ? workItem.Fields[fieldName].Name : string.Format("[{0}]", fieldName);
        }

        public object GetFieldValue(object item, string fieldName)
        {
            var workItem = (WorkItem)item;
            return workItem.Fields.Contains(fieldName) ? workItem.Fields[fieldName].Value : null;
        }

        public object GetWorkItemObject(int id)
        {
            return GetWorkItemFromCache(id);
        }

        #region private


        private Project _project;
        private WorkItemType _taskType;
        private WorkItemLinkType _hierarchyLinkType;
        private WorkItemStore _store;
        private WorkItemCollection _cachedWorkItems;
        private IEnumerable<WorkItemLinkInfo> _links;

        private void AddChildNodes(Dictionary<int, string> dic, Node node)
        {
            if (node.HasChildNodes)
            {
                foreach (Node childNode in node.ChildNodes)
                {
                    if (!dic.ContainsKey(childNode.Id))
                    {
                        dic.Add(childNode.Id, childNode.Path);
                        AddChildNodes(dic, childNode);
                    }
                }
            }
        }

        private TreeItem[] GetNodes(TreeItem current)
        {
            var currentId = 0;
            if (current != null)
            {
                currentId = current.WorkItemID;
            }

            var workItems = _links.Where(x => x.SourceId == currentId)
                .Select(x =>
                {
                    var item = GetWorkItemFromCache(x.TargetId);
                    if (item == null)
                        return null;

                    var isTask = TfsUtility.GetItemType(item.Type.Name) == ItemType.Task;
                    TreeItem treeItem = isTask ? new TaskTreeItem() : new TreeItem();
                    treeItem.WorkItemID = item.Id;
                    treeItem.WorkItemTitle = item.Title;
                    treeItem.TypeName = item.Type.Name;

                    if (isTask)
                    {
                        ((TaskTreeItem)treeItem).OriginalEstimate = (double?)GetFieldValue(item, TfsConstants.Fields.OriginalEstimate);
                        ((TaskTreeItem)treeItem).RemainingWork = (double?)GetFieldValue(item, TfsConstants.Fields.RemainingWork);
                        ((TaskTreeItem)treeItem).CompletedWork = (double?)GetFieldValue(item, TfsConstants.Fields.CompletedWork);
                    }

                    return treeItem;
                }).Where(x => x != null).ToArray();
            Array.ForEach(workItems, w => w.Children.AddRange(GetNodes(w)));

            return workItems;
        }

        private WorkItem GetWorkItemFromCache(int id)
        {
            foreach (WorkItem item in _cachedWorkItems)
            {
                if (item.Id == id)
                {
                    return item.State != TfsConstants.States.Removed ? item : null;
                }
            }

            return null;
        }

        private WorkItem GetWorkItem(int id)
        {
            return _store.GetWorkItem(id);
        }


        #endregion
    }
}
