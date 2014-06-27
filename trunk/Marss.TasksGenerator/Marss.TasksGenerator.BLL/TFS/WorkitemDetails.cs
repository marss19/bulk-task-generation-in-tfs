using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marss.TasksGenerator.BLL.TFS
{
    public class WorkitemDetails
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
