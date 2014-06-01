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
                switch (TypeName)
                {
                    case "Task":
                        return ItemType.Task;

                    case "User Story":
                        return ItemType.UserStory;

                    case "Issue":
                        return ItemType.Issue;

                    case "Bug":
                        return ItemType.Bug;

                    case "Feature":
                        return ItemType.Feature;

                    default:
                        return ItemType.Other;
                }
            }
        }

        public List<TreeItem> Children
        {
            get { return _children ?? (_children = new List<TreeItem>()); }
        }
        private List<TreeItem> _children;
    }
}
