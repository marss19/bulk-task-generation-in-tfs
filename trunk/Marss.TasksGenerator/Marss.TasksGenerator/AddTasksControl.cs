using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Marss.TasksGenerator.BLL.TaskTemplates;
using Marss.TasksGenerator.BLL.TFS;

namespace Marss.TasksGenerator
{
    public partial class AddTasksControl : UserControl
    {
        public AddTasksControl()
        {
            InitializeComponent();
        }

        public static AddTasksControl LoadTemplate(TfsDataProvider provider, string templateName, Control parentPanel)
        {
            var templateUiControl = new AddTasksControl();
            if (templateUiControl.LoadTemplateForm(provider, templateName))
            {

                templateUiControl.Dock = DockStyle.Fill;
                templateUiControl.AdjustFixedFieldsPanel();

                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(templateUiControl);
            }
            return templateUiControl;
        }

        public EventHandler<AddTasksEventArgs> AddTasksButtonClicked;

        #region private

        private TaskTemplateProvider _taskTemplateProvider;
        private TaskTemplate _template;


        private int _staticBlockPreferedHeight = 0;

        private void AdjustFixedFieldsPanel()
        {
            splitContainer1.SplitterDistance = _staticBlockPreferedHeight;
        }

        private bool LoadTemplateForm(TfsDataProvider provider, string templateName)
        {
            _taskTemplateProvider = new TaskTemplateProvider(provider);

            try
            {
                _template = _taskTemplateProvider.GetTemplate(templateName);
                if (_template == null)
                    throw new Exception("Template data does not exist.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed to load task template: {0}. Cause: {1}", templateName, ex.Message),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            lblTemplate.Text = "Task Template: " + templateName;
             _template.StaticFields.Reverse();
            foreach (var field in _template.StaticFields)
            {
                var control = GetControlForStaticField(field);
                pnlFixedFields.Controls.Add(control);
            }

            gridTasks.Columns.Clear();
            gridTasks.AutoGenerateColumns = false;

            foreach (var field in _template.DynamicFields)
            {
                var column = GetColumnForDynamicField(field);
                if (_template.DynamicFieldsWidths.ContainsKey(field))
                {
                    column.Width = _template.DynamicFieldsWidths[field];
                }
                gridTasks.Columns.Add(column);
            }

            _staticBlockPreferedHeight = _template.StaticFields.Count * 30 + 35;
            return true;
        }



        private Control GetControlForStaticField(string field)
        {
            var pnl = new Panel { Dock = DockStyle.Top, Height = 30, Padding = new Padding(3) };

            List<string> availableValues;
            if (_taskTemplateProvider.IsDropDown(field, out availableValues))
            {
                var dropDown = new ComboBox { 
                    DropDownStyle = ComboBoxStyle.DropDownList, 
                    Width = 300, 
                    Dock = DockStyle.Left,
                    Tag = field
                };
                foreach (var availableValue in availableValues)
                {
                    dropDown.Items.Add(availableValue);    
                }
                pnl.Controls.Add(dropDown);
            }
            else
            {
                var fieldType = _taskTemplateProvider.GetFieldType(field);
                pnl.Controls.Add(new TextBox()
                {
                    Width = fieldType == typeof(string) ? 300 : 100,
                    Dock = DockStyle.Left,
                    Tag = field
                });
            }

            pnl.Controls.Add(new Label
            {
                Text = _taskTemplateProvider.GetFrienlyName(field) + ":",
                Dock = DockStyle.Left,
            });

            return pnl;
        }

        private string GetValueOfStaticField(string field)
        {
            var control = FindControlDisplayingStaticField(pnlFixedFields, field);
            if (control is TextBox)
                return ((TextBox)control).Text;

            if (control is ComboBox)
                return (((ComboBox)control).SelectedItem ?? string.Empty).ToString();

            else throw new NotImplementedException(string.Format("Unexpected control type: {0}", control != null ? control.GetType() : null));

        }


        private Control FindControlDisplayingStaticField(Control root, string field)
        {
            foreach (Control control in root.Controls)
            {
                if ((string)control.Tag == field)
                {
                    return control;
                }
                else
                {
                    var subControl = FindControlDisplayingStaticField(control, field);
                    if (subControl != null)
                        return subControl;
                }
            }
            return null;
        }


        private DataGridViewColumn GetColumnForDynamicField(string field)
        {
            var fieldType = _taskTemplateProvider.GetFieldType(field);
            var headerText = _taskTemplateProvider.GetFrienlyName(field);

            List<string> availableValues;
            var isDropdown = _taskTemplateProvider.IsDropDown(field, out availableValues);

            var width = isDropdown || fieldType == typeof(string) ? 150 : 75;

            if (isDropdown)
            {
                var column = new DataGridViewComboBoxColumn()
                    {
                        Name = field,
                        DataPropertyName = field,
                        HeaderText = headerText,
                        Width = width,
                    };

                foreach (var availableValue in availableValues)
                {
                    column.Items.Add(availableValue);
                }

                return column;
            }
            else
            {
                return new DataGridViewTextBoxColumn()
                {
                    Name = field,
                    DataPropertyName = field,
                    HeaderText = headerText,
                    Width = width
                };
            }
        }

        private void PasteDataFromClipboard()
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                if (lines.Length == 0)
                    return;

                foreach (var line in lines)
                {
                    var items = line.Split('\t');
                    var columnValues = new object[gridTasks.Columns.Count];
                    for(var i = 0; i < columnValues.Length; i++)
                    {
                        if (i >= items.Length)
                            break;

                        columnValues[i] = items[i];
                    }

                    gridTasks.Rows.Add(columnValues);
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell. " + fex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteCurrentRow()
        {
            if (gridTasks.CurrentRow != null && !gridTasks.CurrentRow.IsNewRow)
            {
                gridTasks.Rows.Remove(gridTasks.CurrentRow);
            }
        }

        #endregion

        #region event handlers

        private void btnAddTasks_Click(object sender, EventArgs e)
        {
            var data = new DataTable();
            foreach (var staticField in _template.StaticFields)
            {
                var column = data.Columns.Add(staticField);
                column.DefaultValue = GetValueOfStaticField(staticField);
            }
            foreach (var dynamicField in _template.DynamicFields)
            {
                data.Columns.Add(dynamicField);
            }
            foreach (DataGridViewRow gridRow in gridTasks.Rows)
            {
                if (gridRow.IsNewRow)
                    continue;

                var row = data.NewRow();
                foreach (var dynamicField in _template.DynamicFields)
                {
                    row[dynamicField] = gridRow.Cells[dynamicField].Value;
                }
                data.Rows.Add(row);
            }

            if (AddTasksButtonClicked != null)
            {
                var args = new AddTasksEventArgs(data, _template);
                args.ExternalParentWorkitemIsMandatory = !data.Columns.Contains(TfsConstants.Fields.ParentWorkItemId);
                AddTasksButtonClicked(this, args);
                if (!args.Failed)
                {
                    gridTasks.Rows.Clear();
                }
            }

        }

        private void gridTasks_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            var field = e.Column.DataPropertyName;
            var newWidth = e.Column.Width;

            if (_template.DynamicFieldsWidths.ContainsKey(field))
                _template.DynamicFieldsWidths[field] = newWidth;
            else
                _template.DynamicFieldsWidths.Add(field, newWidth);
            _taskTemplateProvider.SaveTemplate(_template);
        }


        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == miPasteFromClipboard)
            {
                PasteDataFromClipboard();
            }
            else if (e.ClickedItem == miDeleteSelectedRowsToolStripMenuItem)
            {
                DeleteCurrentRow();
            }
            
        }

        #endregion




    }
}
