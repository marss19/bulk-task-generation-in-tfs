using Marss.TasksGenerator.BLL.TaskTemplates;
using Marss.TasksGenerator.BLL.TFS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marss.TasksGenerator
{
    public partial class EditTemplateForm : Form
    {
        public EditTemplateForm()
        {
            InitializeComponent();
        }

        public static bool AddNewTemplate(Form parentForm, TaskTemplateProvider taskTemplateProvider)
        {
            var form = new EditTemplateForm();
            form._editing = false;
            form.StartPosition = FormStartPosition.CenterParent;
            form.Text = "Add New Template";
            form._taskTemplateProvider = taskTemplateProvider;

            form.FillAllFields();

            return form.ShowDialog(parentForm) == DialogResult.OK;
        }

        public static bool EditTemplate(Form parentForm, string templateName, TaskTemplateProvider taskTemplateProvider)
        {
            var form = new EditTemplateForm();
            form._editing = true;
            form.StartPosition = FormStartPosition.CenterParent;
            form.Text = "Edit Template";
            form._taskTemplateProvider = taskTemplateProvider;
            form.FillAllFields();

            var template = taskTemplateProvider.GetTemplate(templateName);

            form.tbTemplateName.Text = template.Name;
            form.tbTemplateName.Enabled = false;

            foreach (var item in template.StaticFields)
            {
                form.lbStaticFields.Items.Add(item);
                form.lbAllFields.Items.Remove(item);
            }

            foreach (var item in template.DynamicFields)
            {
                form.lbDynamicFields.Items.Add(item);
                form.lbAllFields.Items.Remove(item);
            }
                

            return form.ShowDialog(parentForm) == DialogResult.OK;
        
        }

        private void btnAddStaticField_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lbAllFields, lbStaticFields);
        }

        private void btnRemoveStaticField_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lbStaticFields, lbAllFields);
        }

        private void btnAddDynamicField_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lbAllFields, lbDynamicFields);
        }

        private void btnRemoveDynamicField_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lbDynamicFields, lbAllFields);
        }
    
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveTemplate())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void btnStaticUp_Click(object sender, EventArgs e)
        {
            MoveItemWithinList(lbStaticFields, true);
        }

        private void btnStaticDown_Click(object sender, EventArgs e)
        {
            MoveItemWithinList(lbStaticFields, false);
        }

        private void btnDynamicUp_Click(object sender, EventArgs e)
        {
            MoveItemWithinList(lbDynamicFields, true);
        }

        private void btnDynamicDown_Click(object sender, EventArgs e)
        {
            MoveItemWithinList(lbDynamicFields, false);
        }

        #region private

        TaskTemplateProvider _taskTemplateProvider;
        bool _editing;

        private void MoveSelectedItems(ListBox source, ListBox destination)
        {
            for (var i = source.Items.Count - 1; i >= 0; i--)
            {
                if (source.SelectedIndices.Contains(i))
                {
                    var item = source.Items[i];
                    source.Items.RemoveAt(i);
                    destination.Items.Add(item);
                }
            }

        }

        private void FillAllFields()
        {
            var allFields = _taskTemplateProvider.GetAllTasksFields();
            lbAllFields.Items.AddRange(allFields.ToArray<object>());
        }

        private bool SaveTemplate()
        {
            var template = new TaskTemplate();
            template.Name = tbTemplateName.Text.Trim();
            template.StaticFields = new List<string>();
            foreach (string item in lbStaticFields.Items)
            {
                template.StaticFields.Add(item);
            }
            template.DynamicFields = new List<string>();
            foreach (string item in lbDynamicFields.Items)
            {
                template.DynamicFields.Add(item);
            }

            List<string> errors;
            if (_taskTemplateProvider.ValidateTemplate(template, _editing, out errors))
            {
                _taskTemplateProvider.SaveTemplate(template);
                return true;
            }
            else
            {
                MessageBox.Show(string.Join(Environment.NewLine, errors),
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private void MoveItemWithinList(ListBox list, bool moveUp)
        {
            if (list.SelectedIndex != -1)
            {
                if ((moveUp && list.SelectedIndex > 0)
                || (!moveUp && list.SelectedIndex < list.Items.Count - 1))
                {
                    var selectedIndex = list.SelectedIndex;
                    var item = list.SelectedItem;
                    list.Items.Remove(item);

                    if (moveUp)
                    {
                        selectedIndex--;
                    }
                    else
                    {
                        selectedIndex++;
                    }
                    list.Items.Insert(selectedIndex, item);
                    list.SelectedIndex = selectedIndex;
                }
               

            }

            
        }

        #endregion

    }
}
