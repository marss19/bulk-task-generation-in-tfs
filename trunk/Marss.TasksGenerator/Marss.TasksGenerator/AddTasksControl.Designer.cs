namespace Marss.TasksGenerator
{
    partial class AddTasksControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPasteFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteSelectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAddTasks = new System.Windows.Forms.Button();
            this.gridTasks = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.pnlFixedFields = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPasteFromClipboard,
            this.miDeleteSelectedRowsToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(228, 48);
            this.contextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenu_ItemClicked);
            // 
            // miPasteFromClipboard
            // 
            this.miPasteFromClipboard.Name = "miPasteFromClipboard";
            this.miPasteFromClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miPasteFromClipboard.Size = new System.Drawing.Size(227, 22);
            this.miPasteFromClipboard.Text = "Paste from Clipboard";
            // 
            // miDeleteSelectedRowsToolStripMenuItem
            // 
            this.miDeleteSelectedRowsToolStripMenuItem.Name = "miDeleteSelectedRowsToolStripMenuItem";
            this.miDeleteSelectedRowsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.miDeleteSelectedRowsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.miDeleteSelectedRowsToolStripMenuItem.Text = "Delete Current Row";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAddTasks);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 278);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(795, 42);
            this.panel2.TabIndex = 2;
            // 
            // btnAddTasks
            // 
            this.btnAddTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddTasks.Location = new System.Drawing.Point(693, 6);
            this.btnAddTasks.Name = "btnAddTasks";
            this.btnAddTasks.Size = new System.Drawing.Size(97, 27);
            this.btnAddTasks.TabIndex = 2;
            this.btnAddTasks.Text = "Add Tasks";
            this.btnAddTasks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddTasks.UseVisualStyleBackColor = true;
            this.btnAddTasks.Click += new System.EventHandler(this.btnAddTasks_Click);
            // 
            // gridTasks
            // 
            this.gridTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTasks.ContextMenuStrip = this.contextMenu;
            this.gridTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTasks.Location = new System.Drawing.Point(0, 0);
            this.gridTasks.Name = "gridTasks";
            this.gridTasks.Size = new System.Drawing.Size(795, 278);
            this.gridTasks.TabIndex = 0;
            this.gridTasks.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.gridTasks_ColumnWidthChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTemplate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(795, 30);
            this.panel1.TabIndex = 1;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTemplate.Location = new System.Drawing.Point(3, 10);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(66, 17);
            this.lblTemplate.TabIndex = 0;
            this.lblTemplate.Text = "Template";
            // 
            // pnlFixedFields
            // 
            this.pnlFixedFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFixedFields.Location = new System.Drawing.Point(0, 30);
            this.pnlFixedFields.Name = "pnlFixedFields";
            this.pnlFixedFields.Padding = new System.Windows.Forms.Padding(3);
            this.pnlFixedFields.Size = new System.Drawing.Size(795, 181);
            this.pnlFixedFields.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlFixedFields);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridTasks);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(795, 535);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 0;
            // 
            // AddTasksControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "AddTasksControl";
            this.Size = new System.Drawing.Size(795, 535);
            this.contextMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miPasteFromClipboard;
        private System.Windows.Forms.ToolStripMenuItem miDeleteSelectedRowsToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAddTasks;
        private System.Windows.Forms.DataGridView gridTasks;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Panel pnlFixedFields;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
