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
    public partial class ManageTemplatesForm : Form
    {
        public ManageTemplatesForm()
        {
            InitializeComponent();
        }

        #region public static

        public static string ShowForm(Form parent, TfsDataProvider dataProvider)
        {
            var form = new ManageTemplatesForm();
            form._taskTemplateProvider = new TaskTemplateProvider(dataProvider);
            form.BindTemplates();
            return form.ShowDialog(parent) == DialogResult.OK
                ? form.lbTemplates.SelectedItem.ToString()
                : null;
        }

        #endregion

        #region event handlers

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            EditTemplateForm.AddNewTemplate(this, _taskTemplateProvider);
            BindTemplates();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ActionIsPossibile("Update"))
            {
                EditTemplateForm.EditTemplate(this, (string)lbTemplates.SelectedItem, _taskTemplateProvider);
                BindTemplates();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ActionIsPossibile("Deletion"))
            {
                _taskTemplateProvider.DeleteTemplate((string)lbTemplates.SelectedItem);
                BindTemplates();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lbTemplates.SelectedItem != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select a template", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region private

        TaskTemplateProvider _taskTemplateProvider;
  
        private bool ActionIsPossibile(string action)
        {
            if (string.IsNullOrWhiteSpace((string)lbTemplates.SelectedItem))
            {
                MessageBox.Show("Select a template.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((string)lbTemplates.SelectedItem == "Default")
            {
                MessageBox.Show(action + " of the default template is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private void BindTemplates()
        {
            var templates = _taskTemplateProvider.GetTemplates();

            lbTemplates.Items.Clear();
            foreach(var template in templates)
                lbTemplates.Items.Add(template);
        }

        #endregion

    }
}
