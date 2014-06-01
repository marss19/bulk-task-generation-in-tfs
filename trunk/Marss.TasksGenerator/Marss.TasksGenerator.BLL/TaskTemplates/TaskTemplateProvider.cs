using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Marss.TasksGenerator.BLL.DataStorage;
using Marss.TasksGenerator.BLL.TFS;

namespace Marss.TasksGenerator.BLL.TaskTemplates
{
    public class TaskTemplateProvider
    {
        public TaskTemplateProvider(TfsDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #region templates

        public List<string> GetAllTasksFields()
        {
            var fields = GetAllEditableTaskFields();
            fields.Add(TfsConstants.Fields.ParentWorkItemId);
            return fields;
        }


        public TaskTemplate GetTemplate(string templateName)
        {
            using (ITaskTemplateRepository repository = new TaskTemplateRegistryRepository())
            {
                return repository.GetTemplate(_dataProvider.ServerName, _dataProvider.ProjectName, templateName);
            }
        }

        public string[] GetTemplates()
        {
            string[] templates;
            using (ITaskTemplateRepository repository = new TaskTemplateRegistryRepository())
            {
                templates = repository.GetAvailableTemplates(_dataProvider.ServerName, _dataProvider.ProjectName);
            }

            if (templates.Any())
            {
                return templates;
            }
            else
            {
                var defaultTemplate = CreateDefaultTemplate();
                using (ITaskTemplateRepository repository = new TaskTemplateRegistryRepository())
                {
                    repository.AddTaskTemplate(_dataProvider.ServerName, _dataProvider.ProjectName, defaultTemplate);
                }

                return new[] { defaultTemplate.Name };
            }
        }

        public bool ValidateTemplate(TaskTemplate template, bool editing, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(template.Name))
            {
                errors.Add("Template name should not be empty.");
            }
            else
            {
                if (!editing)
                {
                    var existingTemplates = GetTemplates();
                    if (existingTemplates.Contains(template.Name, StringComparer.CurrentCultureIgnoreCase))
                    {
                        errors.Add("Template with this name already exists.");
                    }
                }
            }

            var requiredFields = GetRequiredTaskFields();
            foreach (var requiredField in requiredFields)
            {
                if (!template.StaticFields.Contains(requiredField) 
                    && !template.DynamicFields.Contains(requiredField)
                    && !_autoSetByTFSFields.Contains(requiredField)
                    && !_autoSetByTaskGeneratorFields.Contains(requiredField)) 
                {
                    errors.Add(string.Format("'{0}' is a mandatory field. Please add it either to the static fields list or to the dynamic fields list", requiredField));
                }
            }

            return errors.Count == 0;
        }

        public void SaveTemplate(TaskTemplate template)
        {
            if (GetTemplates().Contains(template.Name))
            {
                using (ITaskTemplateRepository repository = new TaskTemplateRegistryRepository())
                {
                    repository.UpdateTaskTemplate(_dataProvider.ServerName, _dataProvider.ProjectName, template);
                }
            }
            else
            {
                using (ITaskTemplateRepository repository = new TaskTemplateRegistryRepository())
                {
                    repository.AddTaskTemplate(_dataProvider.ServerName, _dataProvider.ProjectName, template);
                }

            }
        }

        public void DeleteTemplate(string templateName)
        {
            using (ITaskTemplateRepository repository = new TaskTemplateRegistryRepository())
            {
                repository.DeleteTaskTemplate(_dataProvider.ServerName, _dataProvider.ProjectName, templateName);
            }

        }

        
        #endregion

        #region fields

        public string GetFrienlyName(string field)
        {
            if (field == TfsConstants.Fields.ParentWorkItemId)
                return field;

            if (_dataProvider.TaskType.FieldDefinitions.Contains(field))
            {
                return _dataProvider.TaskType.FieldDefinitions[field].Name;
            }
            throw new Exception(string.Format("Unexpected field: {0}; it does not exist in the fields definitions for this project", field));
        }

        public bool IsDropDown(string field, out List<string> allowedValues)
        {
            allowedValues = null;

            if (field == TfsConstants.Fields.ParentWorkItemId)
                return false;
           
            if (_dataProvider.TaskType.FieldDefinitions.Contains(field))
            {
                var definition = _dataProvider.TaskType.FieldDefinitions[field];
                var isDropDown = definition.AllowedValues.Count > 0;
                if (isDropDown)
                {
                    allowedValues = new List<string>();
                    foreach (var allowedValue in definition.AllowedValues)
                    {
                        allowedValues.Add(allowedValue.ToString());
                    }
                }
                return isDropDown;
            }
            throw new Exception(string.Format("Unexpected field: {0}; it does not exist in the fields definitions for this project", field));
        }

        public Type GetFieldType(string field)
        {
            if (field == TfsConstants.Fields.ParentWorkItemId)
                return typeof(string);

            if (_dataProvider.TaskType.FieldDefinitions.Contains(field))
            {
                return _dataProvider.TaskType.FieldDefinitions[field].SystemType;
            }
            throw new Exception(string.Format("Unexpected field: {0}; it does not exist in the fields definitions for this project", field));
        }

        
        #endregion

        #region private 

        //no need to set, they will be filled automatically
        private string[] _autoSetByTFSFields = { "Work Item Type", "Created Date", "Created By" };

        //these fields will be set to the same values as parent work item
        private string[] _autoSetByTaskGeneratorFields = { "Iteration Path", "Iteration ID", "Area Path", "Area ID" };

        //fields that usually contain the same values for all new added tasks
        private string[] _potentialStaticFields = { "State", "Reason" };

        //fields related to estimates
        private string[] _estimateFields = { "Microsoft.VSTS.Scheduling.OriginalEstimate", "Microsoft.VSTS.Scheduling.RemainingWork" };

        private TfsDataProvider _dataProvider;

        private List<string> GetRequiredTaskFields()
        {
            var requiredFields = new List<string>();
            var task = _dataProvider.TaskType.NewWorkItem();
            foreach (Field field in task.Fields)
            {
                if (field.IsRequired && field.IsEditable && !field.IsComputed)
                {
                    requiredFields.Add(field.Name);
                }
            }
            return requiredFields;
        }

        private List<string> GetAllEditableTaskFields()
        {
            var requiredFields = new List<string>();
            var task = _dataProvider.TaskType.NewWorkItem();
            foreach (Field field in task.Fields)
            {
                if (field.IsEditable && !field.IsComputed)
                {
                    requiredFields.Add(field.Name);
                }
            }
            return requiredFields;
        }


        private TaskTemplate CreateDefaultTemplate()
        {
            var template = new TaskTemplate();
            template.Name = "Default";

            var requiredFields = GetRequiredTaskFields();
            requiredFields.RemoveAll(x => _autoSetByTFSFields.Contains(x) || _autoSetByTaskGeneratorFields.Contains(x));

            template.StaticFields = new List<string>();
            foreach (var field in _potentialStaticFields)
            {
                if (requiredFields.Contains(field))
                {
                    template.StaticFields.Add(field);
                    requiredFields.Remove(field);
                }
            }

            template.DynamicFields = new List<string>();
            template.DynamicFields.AddRange(requiredFields);

            foreach (var field in _estimateFields)
            {
                if (_dataProvider.TaskType.FieldDefinitions.Contains(field))
                {
                    template.DynamicFields.Add(field);
                }
            }

            return template;
        }

        #endregion

    }
}
