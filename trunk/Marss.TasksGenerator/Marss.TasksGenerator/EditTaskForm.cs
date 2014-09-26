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
    public partial class EditTaskForm : Form
    {
        public EditTaskForm()
        {
            InitializeComponent();
        }

        public static EditTaskForm EditTask(TfsDataProvider provider, object workItem)
        {
            var form = new EditTaskForm();
            form._provider = provider;
            form._workItem = workItem;

            form.tbTitle.Text = provider.GetFieldValue(workItem, TfsConstants.Fields.Title).ToString();
            form.SetField(workItem, TfsConstants.Fields.OriginalEstimate, form.lblEstimate, form.tbEstimate);
            form.SetField(workItem, TfsConstants.Fields.RemainingWork, form.lblRemaining, form.tbRemaining);
            form.lblStatus.Text = provider.GetFieldCaption(workItem, TfsConstants.Fields.State);
            form.cbState.Items.AddRange(provider.GetAllowedValues(workItem, TfsConstants.Fields.State).ToArray());
            form.cbState.SelectedItem = provider.GetFieldValue(workItem, TfsConstants.Fields.State);

            return form;
        }

        public string TaskTitle
        {
            get
            {
                return tbTitle.Text;
            }
        }

        public double? OriginalEstimate
        {
            get
            {
                return GetDoubleValue(tbEstimate);
            }
        }

        public double? RemainingWork
        {
            get
            {
                return GetDoubleValue(tbRemaining);
            }
        }

        public bool IsTaskRemoved
        {
            get
            {
                return TfsConstants.States.Removed.Equals(cbState.SelectedItem);
            }
        }

        #region private

        private TfsDataProvider _provider;
        private object _workItem;

        private void SetField(object workItem, string field, Label lbl, TextBox tb)
        {
            if (_provider.HasField(workItem, field))
            {
                lbl.Text = _provider.GetFieldCaption(workItem, field);

                var value = _provider.GetFieldValue(workItem, field);
                if (value != null)
                {
                    tb.Text = value.ToString();
                }
            }
            else
            {
                lbl.Visible = false;
                tb.Visible = false;
            }
        }

        private double? GetDoubleValue(TextBox tb)
        {
            if (string.IsNullOrEmpty(tb.Text))
                return null;

            
            double d;
            if (double.TryParse(tb.Text, out d))
                return d;
            else
                throw new Exception(string.Format("Failed to convert \"{0}\" to number.", tb.Text));
            
        }

        #endregion

        #region event handlers

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TfsConstants.States.Removed.Equals(cbState.SelectedItem))
            {
                tbEstimate.Text = string.Empty;
                tbRemaining.Text = string.Empty;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var fieldValues = new Dictionary<string, object>();
                if (tbEstimate.Visible)
                    fieldValues.Add(TfsConstants.Fields.OriginalEstimate, GetDoubleValue(tbEstimate));
                if (tbRemaining.Visible)
                    fieldValues.Add(TfsConstants.Fields.RemainingWork, GetDoubleValue(tbRemaining));
                _provider.UpdateWorkitem(_workItem, tbTitle.Text, cbState.SelectedItem.ToString(), fieldValues); 

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to update task. Cause: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        #endregion

    }
}
