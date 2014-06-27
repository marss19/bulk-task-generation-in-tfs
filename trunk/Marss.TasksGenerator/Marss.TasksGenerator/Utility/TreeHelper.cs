using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Marss.TasksGenerator.BLL;

namespace Marss.TasksGenerator.Utility
{
    public static class TreeHelper
    {
        public static TreeNode[] ConvertToTreeNodes(TreeItem[] treeItems)
        {
            return treeItems.Select(Convert).ToArray();
        }

        private static TreeNode Convert(TreeItem treeItem)
        {
            var node = new TreeNode(treeItem.Text);
            node.Tag = treeItem.TfsItem;
            node.ImageIndex = GetImageIndex(treeItem);
            node.SelectedImageIndex = node.ImageIndex;

            if (node.ImageIndex == 0)//other type
            {
                node.Text = string.Format("{0} ({1})", treeItem.Text, treeItem.TypeName.ToLower());
            }

            foreach (var child in treeItem.Children)
            {
                node.Nodes.Add(Convert(child));
            }
            return node;
        }


        private static int GetImageIndex(TreeItem treeItem)
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
