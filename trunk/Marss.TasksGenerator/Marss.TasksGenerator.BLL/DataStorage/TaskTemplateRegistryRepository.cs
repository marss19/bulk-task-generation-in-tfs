using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Win32;
using Marss.TasksGenerator.BLL.TaskTemplates;
using System.Runtime.Serialization.Json;

namespace Marss.TasksGenerator.BLL.DataStorage
{
    public class TaskTemplateRegistryRepository : ITaskTemplateRepository
    {
        public TaskTemplate GetTemplate(string serverName, string projectName, string templateName)
        {
            var projectHive = GetProjectHive(serverName, projectName);
            var value = projectHive.GetValue(templateName);
            return value == null
                       ? null
                       : DeserializeTaskTemplate(value.ToString());
        }

        public string[] GetAvailableTemplates(string serverName, string projectName)
        {
            var projectHive = GetProjectHive(serverName, projectName);
            return projectHive.GetValueNames();
        }

        public void AddTaskTemplate(string serverName, string projectName, TaskTemplate template)
        {
            var serializedObj = SerializeTaskTemplate(template);

            var projectHive = GetProjectHive(serverName, projectName);
            projectHive.SetValue(template.Name, serializedObj, RegistryValueKind.String);
        }

        public void UpdateTaskTemplate(string serverName, string projectName, TaskTemplate template)
        {
            var serializedObj = SerializeTaskTemplate(template);

            var projectHive = GetProjectHive(serverName, projectName);
            projectHive.SetValue(template.Name, serializedObj, RegistryValueKind.String);
        }

        public void DeleteTaskTemplate(string serverName, string projectName, string templateName)
        {
            var projectHive = GetProjectHive(serverName, projectName);
            projectHive.DeleteValue(templateName);

        }

        #region private 

        public RegistryKey ManufacturerHive { get { return Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("Marss"); } }
        public RegistryKey ProductHive { get { return ManufacturerHive.CreateSubKey("TasksGenerator"); } }
        public RegistryKey GetProjectHive(string serverName, string projectName)
        {
            try
            {
                return ProductHive.CreateSubKey(serverName.ToUpper()).CreateSubKey(projectName.ToUpper());
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to access to registry, Current User branch: " + ex.Message, ex);
            }
        }
        
        private string SerializeTaskTemplate(TaskTemplate template)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(template);
        }

        private TaskTemplate DeserializeTaskTemplate(string serializedTemplateData)
        {
            var serializer = new JavaScriptSerializer();
            var template = serializer.Deserialize<TaskTemplate>(serializedTemplateData);
            if (template.DynamicFieldsWidths == null)
                template.DynamicFieldsWidths = new Dictionary<string, int>();
            return template;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            ManufacturerHive.Close();
        }

        #endregion
    }
}
