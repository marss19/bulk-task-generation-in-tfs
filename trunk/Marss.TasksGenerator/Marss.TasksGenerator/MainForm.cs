using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Marss.TasksGenerator.BLL.TaskTemplates;
using Marss.TasksGenerator.BLL.TFS;
using Marss.TasksGenerator.Utility;
using System.Threading.Tasks;
using Marss.TasksGenerator.BLL;

namespace Marss.TasksGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TryConnectToTfs(true);
        }

        #region private

        private TfsDataProvider _tfsDataProvider;
        private int? _selectedAreaId;
        private int? _selectedIterationId;

        private void TryConnectToTfs(bool clearOnCancel)
        {
            _selectedAreaId = null;
            _selectedIterationId = null;

            var tfsDataConnector = new TfsConnector();
            _tfsDataProvider = tfsDataConnector.ConnectToTfsUsingTeamProjectPicker();

            if (_tfsDataProvider != null)
            {
                UpdateFormForSelectedProject();
            }
            else
            {
                if (clearOnCancel)
                    ResetConnectionRelatedItems();
            }
        }

        private void UpdateFormForSelectedProject()
        {
            ResetConnectionRelatedItems();

            toolBtnConnect.Text = string.Format("Project: {0} ({1})", _tfsDataProvider.ProjectName, _tfsDataProvider.ServerName);

            btnLoadItems.Enabled = true;
            btnLoadSpecifiedItem.Enabled = true;
            pnlTaskTemplate.Enabled = true; 

            BindAreas();
            BindIterations();
            BindTaskTemplates();
        }

        private void ResetConnectionRelatedItems()
        {
            toolBtnArea.Enabled = false;
            toolBtnIteration.Enabled = false;
            toolBtnTaskTemplate.Enabled = false;

            toolBtnConnect.Text = "Connect to TFS project";

            toolBtnArea.Text = "Area: Any";
            toolBtnArea.DropDown.Items.Clear();

            toolBtnIteration.Text = "Iteration: Any";
            toolBtnIteration.DropDown.Items.Clear();

            treeWorkItems.Nodes.Clear();

            btnLoadItems.Enabled = false;
            btnLoadSpecifiedItem.Enabled = false;
            pnlTaskTemplate.Enabled = false;
        }

        private void BindAreas()
        {
            toolBtnArea.Enabled = true;
            toolBtnArea.DropDown.Items.Clear();
            toolBtnArea.DropDown.Items.Add("<Any>", null, toolBtnArea_Click);
            foreach (var area in _tfsDataProvider.GetAreas())
            {
                var item = toolBtnArea.DropDown.Items.Add(area.Value, null, toolBtnArea_Click);
                item.Tag = area.Key;
            }
        }

        private void BindIterations()
        {
            toolBtnIteration.Enabled = true;
            toolBtnIteration.DropDown.Items.Clear();
            toolBtnIteration.DropDown.Items.Add("<Any>", null, toolBtnIteration_Click);
            foreach (var area in _tfsDataProvider.GetIterations())
            {
                var item = toolBtnIteration.DropDown.Items.Add(area.Value, null, toolBtnIteration_Click);
                item.Tag = area.Key;
            }
        }

        private void BindTaskTemplates(string selectedTemplateName = null)
        {
            toolBtnTaskTemplate.Enabled = true;
            toolBtnTaskTemplate.DropDown.Items.Clear();

            var templatesProvider = new TaskTemplateProvider(_tfsDataProvider);
            var templates = templatesProvider.GetTemplates();
            foreach (var templateName in templates)
            {
                var item = toolBtnTaskTemplate.DropDown.Items.Add(templateName, null, toolBtnTaskTemplate_Click);
                item.Tag = templateName;
            }

            toolBtnTaskTemplate.DropDown.Items.Add(new ToolStripSeparator());
            toolBtnTaskTemplate.DropDown.Items.Add("Manage Templates...", null, toolBtnTaskTemplate_Click);

            int templateIndex = 0;
            if (!string.IsNullOrWhiteSpace(selectedTemplateName))
            {
                templateIndex = Array.IndexOf(templates, selectedTemplateName);
                if (templateIndex == -1)
                    templateIndex = 0;
            }

            toolBtnTaskTemplate_Click(toolBtnTaskTemplate.DropDown.Items[templateIndex], EventArgs.Empty);
        }

        private void RefreshTree(object selectedWorkItem)
        {
            int? rootWorkItemId = treeWorkItems.Tag != null ? (int?)treeWorkItems.Tag : null;
            LoadWorkItems(rootWorkItemId);
            SelectNodeAndExpandBranch(selectedWorkItem);
        }

        private void LoadWorkItems(int? rootWorkItemId)
        {
            treeWorkItems.BeginUpdate();
            treeWorkItems.Nodes.Clear();
            if (rootWorkItemId.HasValue)
                treeWorkItems.Nodes.AddRange(TreeHelper.ConvertToTreeNodes(_tfsDataProvider.GetWorkItemsAsTreeByWorkItem(rootWorkItemId.Value)));
            else
                treeWorkItems.Nodes.AddRange(TreeHelper.ConvertToTreeNodes(_tfsDataProvider.GetWorkItemsAsTreeByAreaAndIteration(_selectedAreaId, _selectedIterationId)));

            treeWorkItems.EndUpdate();
            treeWorkItems.Tag = rootWorkItemId;
        }

        private void SelectNodeAndExpandBranch(object worksiteItemToSelect)
        {
            var node = FindNode(treeWorkItems.Nodes, worksiteItemToSelect);
            if (node != null)
            {
                treeWorkItems.SelectedNode = node;
                treeWorkItems.Focus();

                do
                {
                    node.Expand();
                    node = node.Parent;
                }
                while (node != null);
            }
         }

        private TreeNode FindNode(TreeNodeCollection nodes, object worksiteItem)
        {
            foreach(TreeNode node in nodes)
            {
                if (TfsUtility.AreEqual(node.Tag, worksiteItem))
                    return node;

                var subNode = FindNode(node.Nodes, worksiteItem);
                if (subNode != null)
                    return subNode;
            }

            return null;
        }

        #endregion

        #region event handlers

        private void toolBtnConnect_Click(object sender, EventArgs e)
        {
            TryConnectToTfs(false);
        }

        void toolBtnArea_Click(object sender, EventArgs e)
        {
            var selelectedItem = ((ToolStripItem) sender);
            _selectedAreaId = (int?) selelectedItem.Tag;
            var selectedText = selelectedItem.Text;
            toolBtnArea.Text = selectedText == "<Any>"
                ? "Area: Any"
                : "Area: " + selectedText;
        }

        void toolBtnIteration_Click(object sender, EventArgs e)
        {
            var selelectedItem = ((ToolStripItem)sender);
            _selectedIterationId = (int?)selelectedItem.Tag;
            var selectedText = selelectedItem.Text;
            toolBtnIteration.Text = selectedText == "<Any>"
                ? "Iteration: All"
                : "Iteration: " + selectedText;
        }

        void toolBtnTaskTemplate_Click(object sender, EventArgs e)
        {
            var selectedItem = ((ToolStripItem)sender);
            var selectedText = selectedItem.Text;
            if (selectedItem.Tag == null)
            {
                var currentTemplate = (string)toolBtnTaskTemplate.Tag;
                ManageTemplatesForm.ShowForm(this, _tfsDataProvider);
                BindTaskTemplates(currentTemplate);
            }
            else
            {
                toolBtnTaskTemplate.Text = "Task Template: " + selectedText;
                toolBtnTaskTemplate.Tag = selectedText;

                var control = AddTasksControl.LoadTemplate(_tfsDataProvider, selectedText, pnlTaskTemplate);
                control.AddTasksButtonClicked += AddTasks;
            }
        }


        private void btnLoadItems_Click(object sender, EventArgs e)
        {
            LoadWorkItems(null);
        }

        private void btnLoadSpecifiedItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbWorkItemId.Text))
                return;

            int workItemId = int.Parse(tbWorkItemId.Text);

            LoadWorkItems(workItemId);
        }

        private void tbWorkItemId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddTasks(object sender, AddTasksEventArgs e)
        {
            var selectedNode = treeWorkItems.SelectedNode;
            if (selectedNode == null && e.ExternalParentWorkitemIsMandatory)
            {

                MessageBox.Show("Select a parent work item for tasks");
                treeWorkItems.Focus();
                return;
            }

            var parentWorksiteItem = selectedNode != null ? selectedNode.Tag : null;
            try
            {
                var frm = new AddTasksProgressForm();

                var template = e.Template;
                var tasksData = e.TasksData;
                frm.ExecuteAction(this, () => _tfsDataProvider.AddTasks(template, tasksData, parentWorksiteItem));
              
            }
            catch (Exception ex)
            {
                e.Failed = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!e.Failed)
            {
                RefreshTree(parentWorksiteItem);
            }
        }

        private void tooBtnAbout_Click(object sender, EventArgs e)
        {
            AboutForm.Show(this);
        }


        #endregion

       
    }
}
