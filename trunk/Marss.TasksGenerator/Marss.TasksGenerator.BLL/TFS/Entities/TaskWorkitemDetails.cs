using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marss.TasksGenerator.BLL.TFS
{
    public class TaskWorkitemDetails : WorkitemDetails
    {
        public double? OriginalEstimate { get; set; }
        public double? RemainingWork { get; set; }
        public double? CompletedWork { get; set; }
    }
}
