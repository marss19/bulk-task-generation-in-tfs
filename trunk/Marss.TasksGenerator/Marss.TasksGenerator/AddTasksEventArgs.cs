using Marss.TasksGenerator.BLL.TaskTemplates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marss.TasksGenerator
{
    public class AddTasksEventArgs : EventArgs
    {
        public AddTasksEventArgs(DataTable tasksData, TaskTemplate template)
        {
            _tasksData = tasksData;
            _template = template;
        }

        readonly DataTable _tasksData;
        readonly TaskTemplate _template;

        public DataTable TasksData
        {
            get { return _tasksData; }
        }
        public TaskTemplate Template
        {
            get { return _template; }
      
        }

        public bool Failed { get; set; }

        public bool ExternalParentWorkitemIsMandatory { get; set; }
    }
}
