using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marss.TasksGenerator.BLL.TaskTemplates
{
    public class TaskTemplate
    {
        public string Name { get; set; }

        public List<string> StaticFields { get; set; }

        public List<string> DynamicFields { get; set; }

        public Dictionary<string, int> DynamicFieldsWidths { get; set; }
    }
}
