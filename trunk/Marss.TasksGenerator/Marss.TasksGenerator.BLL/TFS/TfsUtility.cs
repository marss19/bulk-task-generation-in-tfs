﻿using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marss.TasksGenerator.BLL
{
    public class TfsUtility
    {
 
        public static bool AreEqual(object workItem1, object workItem2)
        {
            if (workItem1 == null && workItem2 == null)
                return true;

            if (workItem1 != null && workItem2 != null)
                return ((WorkItem)workItem1).Id == ((WorkItem)workItem2).Id;

            return false;
        }

        public static ItemType GetItemType(string typeName)
        {
            switch (typeName)
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

                case "Product Backlog Item":
                    return ItemType.BacklogItem;

                default:
                    return ItemType.Other;
            }
        }
    }
}
