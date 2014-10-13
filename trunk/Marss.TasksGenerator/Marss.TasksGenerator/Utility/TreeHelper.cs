using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Marss.TasksGenerator.BLL;
using Marss.TasksGenerator.BLL.TFS;

namespace Marss.TasksGenerator.Utility
{
    public class TreeHelper
    {
        public TreeNode[] ConvertToTreeNodes(TreeItem[] treeItems)
        {
            return treeItems.Select(Convert).ToArray();
        }

        public string GetNodeTitle(TreeItem treeItem)
        {
            return string.Format("{0}. {1}", treeItem.WorkItemID, treeItem.WorkItemTitle);
        }

        private TreeNode Convert(TreeItem treeItem)
        {
            var nodeText = GetNodeTitle(treeItem);

            var node = new TreeNode(nodeText);
            node.Tag = treeItem;
            node.ImageIndex = GetImageIndex(treeItem);
            node.SelectedImageIndex = node.ImageIndex;

            foreach (var child in treeItem.Children)
            {
                node.Nodes.Add(Convert(child));
            }
            return node;
        }

        private int GetImageIndex(TreeItem treeItem)
        {
            //acording to images indexes in the image list on the main form
            switch(treeItem.Type)
            {
                case ItemType.UserStory:
                case ItemType.Issue:
                case ItemType.BacklogItem:
                    return 1;//blue

                case ItemType.Task:
                    return 2; //yellow

                case ItemType.Feature:
                    return 3; //violet

                case ItemType.Bug:
                    return 4; //red

                default:
                    return 0;
            }
        }


    }
}
