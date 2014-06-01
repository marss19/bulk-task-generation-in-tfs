using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marss.TasksGenerator.BLL.TaskTemplates;

namespace Marss.TasksGenerator.BLL.DataStorage
{
    public interface ITaskTemplateRepository : IDisposable
    {
        TaskTemplate GetTemplate(string serverName, string projectName, string templateName);
        string[] GetAvailableTemplates(string serverName, string projectName);
        void AddTaskTemplate(string serverName, string projectName, TaskTemplate template);
        void UpdateTaskTemplate(string serverName, string projectName, TaskTemplate template);
        void DeleteTaskTemplate(string serverName, string projectName, string templateName);
    }
}
