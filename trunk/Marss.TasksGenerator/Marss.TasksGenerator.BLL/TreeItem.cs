using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marss.TasksGenerator.BLL
{
    public class TreeItem
    {
        public int WorkItemID { get; set; }
        public string WorkItemTitle { get; set; }
        public string TypeName { get; set; }

        public ItemType Type
        {
            get
            {
                return TfsUtility.GetItemType(TypeName);
            }
        }

        public List<TreeItem> Children
        {
            get { return _children ?? (_children = new List<TreeItem>()); }
        }
        private List<TreeItem> _children;
    }

    public class TaskTreeItem : TreeItem
    {
        public double? OriginalEstimate { get; set; }
        public double? RemainingWork { get; set; }
        public double? CompletedWork { get; set; }
    }
}
