using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marss.TasksGenerator.BLL.TFS
{
    public class TfsConstants
    {
        public class WorkItemTypes
        {
            public const string Task = "Task";
        }

        public class WorkItemLinkTypes
        {
            public const string Hierarchy = "System.LinkTypes.Hierarchy";
        }

        public class Fields
        {
            public const string ParentWorkItemId = "Parent Workitem ID";
            public const string DescriptionHtml = "Description HTML";

            public const string OriginalEstimate = "Microsoft.VSTS.Scheduling.OriginalEstimate";
            public const string RemainingWork = "Microsoft.VSTS.Scheduling.RemainingWork";
            public const string CompletedWork = "Microsoft.VSTS.Scheduling.CompletedWork";
        }

    }
}
