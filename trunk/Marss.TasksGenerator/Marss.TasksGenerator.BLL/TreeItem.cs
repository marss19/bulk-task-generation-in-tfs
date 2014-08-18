using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marss.TasksGenerator.BLL
{
    public class TreeItem
    {
        public object TfsItem { get; set; }
        public string Text { get; set; }
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
}
