using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marss.TasksGenerator.BLL.TFS
{
    public class ShortTaskInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double? Estimate { get; set; }
    }
}
