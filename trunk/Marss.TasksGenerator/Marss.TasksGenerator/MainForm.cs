using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Marss.TasksGenerator.BLL.TaskTemplates;
using Marss.TasksGenerator.BLL.TFS;
using Marss.TasksGenerator.Utility;
using System.Threading.Tasks;
using Marss.TasksGenerator.BLL;
using System.Text.RegularExpressions;

namespace Marss.TasksGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TryConnectToTfs(true);
        }

        #region private

        private TfsDataProvider _tfsDataProvider;
        private int? _selectedAreaId;
        private int? _selectedIterationId;

        private void TryConnectToTfs(bool clearOnCancel)
        {
            _selectedAreaId = null;
            _selectedIterationId = null;

            var tfsDataConnector = new TfsConnector();
            _tfsDataProvider = tfsDataConnector.ConnectToTfsUsingTeamProjectPicker();

            if (_tfsDataProvider != null)
            {
                UpdateFormForSelectedProject();
            }
            else
            {
                if (clearOnCancel)
                    ResetConnectionRelatedItems();
            }
        }

        private void UpdateFormForSelectedProject()
        {
            ResetConnectionRelatedItems();

            toolBtnConnect.Text = string.Format("Project: {0} ({1})", _tfsDataProvider.ProjectName, _tfsDataProvider.ServerName);

            btnLoadItems.Enabled = true;
            btnLoadSpecifiedItem.Enabled = true;
            pnlTaskTemplate.Enabled = true; 

            BindAreas();
            BindIterations();
            BindTaskTemplates();
        }

        private void ResetConnectionRelatedItems()
        {
            toolBtnArea.Enabled = false;
            toolBtnIteration.Enabled = false;
            toolBtnTaskTemplate.Enabled = false;

            toolBtnConnect.Text = "Connect to TFS project";

            toolBtnArea.Text = "Area: Any";
            toolBtnArea.DropDown.Items.Clear();

            toolBtnIteration.Text = "Iteration: Any";
            toolBtnIteration.DropDown.Items.Clear();

            treeWorkItems.Nodes.Clear();

            btnLoadItems.Enabled = false;
            btnLoadSpecifiedItem.Enabled = false;
            pnlTaskTemplate.Enabled = false;
        }

        private void BindAreas()
        {
            toolBtnArea.Enabled = true;
            toolBtnArea.DropDown.Items.Clear();
            toolBtnArea.DropDown.Items.Add("<Any>", null, toolBtnArea_Click);
            foreach (var area in _tfsDataProvider.GetAreas())
            {
                var item = toolBtnArea.DropDown.Items.Add(area.Value, null, toolBtnArea_Click);
                item.Tag = area.Key;
            }
        }

        private void BindIterations()
        {
            toolBtnIteration.Enabled = true;
            toolBtnIteration.DropDown.Items.Clear();
            toolBtnIteration.DropDown.Items.Add("<Any>", null, toolBtnIteration_Click);
            foreach (var area in _tfsDataProvider.GetIterations())
            {
                var item = toolBtnIteration.DropDown.Items.Add(area.Value, null, toolBtnIteration_Click);
                item.Tag = area.Key;
            }
        }

        private void BindTaskTemplates(string selectedTemplateName = null)
        {
            toolBtnTaskTemplate.Enabled = true;
            toolBtnTaskTemplate.DropDown.Items.Clear();

            var templatesProvider = new TaskTemplateProvider(_tfsDataProvider);
            var templates = templatesProvider.GetTemplates();
            foreach (var templateName in templates)
            {
                var item = toolBtnTaskTemplate.DropDown.Items.Add(templateName, null, toolBtnTaskTemplate_Click);
                item.Tag = templateName;
            }

            toolBtnTaskTemplate.DropDown.Items.Add(new ToolStripSeparator());
            toolBtnTaskTemplate.DropDown.Items.Add("Manage Templates...", null, toolBtnTaskTemplate_Click);

            int templateIndex = 0;
            if (!string.IsNullOrWhiteSpace(selectedTemplateName))
            {
                templateIndex = Array.IndexOf(templates, selectedTemplateName);
                if (templateIndex == -1)
                    templateIndex = 0;
            }

            toolBtnTaskTemplate_Click(toolBtnTaskTemplate.DropDown.Items[templateIndex], EventArgs.Empty);
        }

        private void RefreshTree(int? selectedWorkItemId)
        {
            int? rootWorkItemId = treeWorkItems.Tag != null ? (int?)treeWorkItems.Tag : null;
            LoadWorkItems(rootWorkItemId);

            if (selectedWorkItemId.HasValue)
                SelectNodeAndExpandBranch(selectedWorkItemId.Value);
        }

        private void LoadWorkItems(int? rootWorkItemId)
        {
            treeWorkItems.BeginUpdate();
            treeWorkItems.Nodes.Clear();
            if (rootWorkItemId.HasValue)
                treeWorkItems.Nodes.AddRange(new TreeHelper().ConvertToTreeNodes(_tfsDataProvider.GetWorkItemsAsTreeByWorkItem(rootWorkItemId.Value)));
            else
                treeWorkItems.Nodes.AddRange(new TreeHelper().ConvertToTreeNodes(_tfsDataProvider.GetWorkItemsAsTreeByAreaAndIteration(_selectedAreaId, _selectedIterationId)));

            treeWorkItems.EndUpdate();
            treeWorkItems.Tag = rootWorkItemId;

            CleanUpDetailsPane();
        }

        private void SelectNodeAndExpandBranch(int worksiteItemId)
        {
            var node = FindNode(treeWorkItems.Nodes, worksiteItemId);
            if (node != null)
            {
                treeWorkItems.SelectedNode = node;
                treeWorkItems.Focus();

                do
                {
                    node.Expand();
                    node = node.Parent;
                }
                while (node != null);
            }
         }

        private TreeNode FindNode(TreeNodeCollection nodes, int worksiteItemId)
        {
            foreach(TreeNode node in nodes)
            {
                if (((TreeItem)node.Tag).WorkItemID == worksiteItemId)
                    return node;

                var subNode = FindNode(node.Nodes, worksiteItemId);
                if (subNode != null)
                    return subNode;
            }

            return null;
        }

        private void UpdateSelectedNodeDetailsPane(TreeNode node)
        {
            var treeItem = (TreeItem)node.Tag;
            

            var sb = new StringBuilder();
            sb.Append("<div style='font-family:Segoe UI;font-size:9pt;'>");
            sb.AppendFormat("<b>{0} #{1}.</b> {2}<hr size='1'/>", treeItem.TypeName, treeItem.WorkItemID, treeItem.WorkItemTitle);

            var description = _tfsDataProvider.GetWorkitemDescriptionHtml(treeItem.WorkItemID);
            sb.Append(description);

            if (!string.IsNullOrWhiteSpace(description))
            {
                sb.Append("<hr size='1'/>");
            }

            if (treeItem.Type == ItemType.Task)
            {
                var taskTreeItem = (TaskTreeItem)treeItem;
                sb.AppendFormat("Original Estimate: {0}<br/>", taskTreeItem.OriginalEstimate);
                sb.AppendFormat("Remaining Work: {0}<br/>", taskTreeItem.RemainingWork);
                sb.AppendFormat("Completed Work: {0}<br/>", taskTreeItem.CompletedWork);
            }
            else
            {
                double estimate = 0, remaining = 0, completed = 0;
                CalculateSummaryValues(node, ref estimate, ref remaining, ref completed);

                sb.AppendFormat("Summary Original Estimate: {0}<br/>", estimate);
                sb.AppendFormat("Summary Remaining Work: {0}<br/>", remaining);
                sb.AppendFormat("Summary Completed Work: {0}<br/>", completed);
            }

            sb.Append("</div>");
            wbWorkItemDetails.DocumentText = sb.ToString();
        }
        private void CleanUpDetailsPane()
        {
            wbWorkItemDetails.DocumentText = string.Empty;
        }

        private void CalculateSummaryValues(TreeNode node, ref double estimate, ref double remaining, ref double completed)
        {
            foreach (TreeNode subNode in node.Nodes)
            {
                var task = subNode.Tag as TaskTreeItem;
                if (task != null)
                {
                    if (task.OriginalEstimate.HasValue)
                        estimate += task.OriginalEstimate.Value;
                    if (task.RemainingWork.HasValue)
                        remaining += task.RemainingWork.Value;
                    if (task.CompletedWork.HasValue)
                        completed += task.CompletedWork.Value;
                }
                CalculateSummaryValues(subNode, ref estimate, ref remaining, ref completed);
            }
        }

        #endregion

        #region event handlers

        private void toolBtnConnect_Click(object sender, EventArgs e)
        {
            TryConnectToTfs(false);
        }

        void toolBtnArea_Click(object sender, EventArgs e)
        {
            var selelectedItem = ((ToolStripItem) sender);
            _selectedAreaId = (int?) selelectedItem.Tag;
            var selectedText = selelectedItem.Text;
            toolBtnArea.Text = selectedText == "<Any>"
                ? "Area: Any"
                : "Area: " + selectedText;
        }

        void toolBtnIteration_Click(object sender, EventArgs e)
        {
            var selelectedItem = ((ToolStripItem)sender);
            _selectedIterationId = (int?)selelectedItem.Tag;
            var selectedText = selelectedItem.Text;
            toolBtnIteration.Text = selectedText == "<Any>"
                ? "Iteration: All"
                : "Iteration: " + selectedText;
        }

        void toolBtnTaskTemplate_Click(object sender, EventArgs e)
        {
            var selectedItem = ((ToolStripItem)sender);
            var selectedText = selectedItem.Text;
            if (selectedItem.Tag == null)
            {
                var currentTemplate = (string)toolBtnTaskTemplate.Tag;
                ManageTemplatesForm.ShowForm(this, _tfsDataProvider);
                BindTaskTemplates(currentTemplate);
            }
            else
            {
                toolBtnTaskTemplate.Text = "Task Template: " + selectedText;
                toolBtnTaskTemplate.Tag = selectedText;

                var control = AddTasksControl.LoadTemplate(_tfsDataProvider, selectedText, pnlTaskTemplate);
                control.AddTasksButtonClicked += AddTasks;
            }
        }


        private void btnLoadItems_Click(object sender, EventArgs e)
        {
            LoadWorkItems(null);
        }

        private void btnLoadSpecifiedItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbWorkItemId.Text))
                return;

            int workItemId = int.Parse(tbWorkItemId.Text);

            LoadWorkItems(workItemId);
        }

        private void tbWorkItemId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddTasks(object sender, AddTasksEventArgs e)
        {
            var selectedNode = treeWorkItems.SelectedNode;
            if (selectedNode == null && e.ExternalParentWorkitemIsMandatory)
            {

                MessageBox.Show("Select a parent work item for tasks");
                treeWorkItems.Focus();
                return;
            }

            var parentWorksiteItemId = selectedNode != null ? (int?)GetIdOfWorkitemBoundToNode(selectedNode) : null;
            try
            {
                var template = e.Template;
                var tasksData = e.TasksData;
                AddTasksProgressForm.ExecuteAction(this, "Adding tasks, please wait...", () => _tfsDataProvider.AddTasks(template, tasksData, parentWorksiteItemId));
              
            }
            catch (Exception ex)
            {
                e.Failed = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!e.Failed)
            {
                RefreshTree(parentWorksiteItemId);
            }
        }

        private void tooBtnAbout_Click(object sender, EventArgs e)
        {
            AboutForm.Show(this);
        }

        private void treeWorkItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            wbWorkItemDetails.Font = Font;

            if (e.Node != null && e.Node.Tag != null)
            {
                UpdateSelectedNodeDetailsPane(e.Node);
            }
            else
            {
                CleanUpDetailsPane();
            }
        }

        private void treeWorkItems_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeWorkItems_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                var pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                var destinationNode = ((TreeView)sender).GetNodeAt(pt);
                var movedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                try
                {
                    var moved = false;
                    AddTasksProgressForm.ExecuteAction(this, "Moving item, please wait...", () =>
                    {
                        moved = _tfsDataProvider.MoveWorkItem(GetIdOfWorkitemBoundToNode(movedNode), GetIdOfWorkitemBoundToNode(destinationNode));
                    });

                    if (moved)
                        RefreshTree(GetIdOfWorkitemBoundToNode(destinationNode));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int GetIdOfWorkitemBoundToNode(TreeNode node)
        {
            return ((TreeItem)node.Tag).WorkItemID;
        }

        private void treeWorkItems_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeWorkItems_DoubleClick(object sender, EventArgs e)
        {
            if (treeWorkItems.SelectedNode != null && ((TreeItem)treeWorkItems.SelectedNode.Tag).Type == ItemType.Task)
            {
                var form = EditTaskForm.EditTask(_tfsDataProvider, ((TreeItem)treeWorkItems.SelectedNode.Tag).WorkItemID);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (form.IsTaskRemoved)
                    {
                        treeWorkItems.Nodes.Remove(treeWorkItems.SelectedNode);
                        CleanUpDetailsPane();
                    }
                    else
                    {
                        var treeItem = (TaskTreeItem)treeWorkItems.SelectedNode.Tag;
                        treeItem.WorkItemTitle = form.TaskTitle;
                        treeItem.OriginalEstimate = form.OriginalEstimate;
                        treeItem.RemainingWork = form.RemainingWork;

                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            treeWorkItems.SelectedNode.Text = new TreeHelper().GetNodeTitle(treeItem);
                            UpdateSelectedNodeDetailsPane(treeWorkItems.SelectedNode);
                        });
                    }
                }
            }
        }

        #endregion
       
    }
}
