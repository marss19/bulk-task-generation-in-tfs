namespace Marss.TasksGenerator
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.toolBtnConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnArea = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolBtnIteration = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnTaskTemplate = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tooBtnAbout = new System.Windows.Forms.ToolStripButton();
            this.horizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.verticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.treeWorkItems = new System.Windows.Forms.TreeView();
            this.treeImages = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbWorkItemId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadSpecifiedItem = new System.Windows.Forms.Button();
            this.btnLoadItems = new System.Windows.Forms.Button();
            this.pnlTaskTemplate = new System.Windows.Forms.Panel();
            this.wbWorkItemDetails = new System.Windows.Forms.WebBrowser();
            this.toolMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSplitContainer)).BeginInit();
            this.horizontalSplitContainer.Panel1.SuspendLayout();
            this.horizontalSplitContainer.Panel2.SuspendLayout();
            this.horizontalSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitContainer)).BeginInit();
            this.verticalSplitContainer.Panel1.SuspendLayout();
            this.verticalSplitContainer.Panel2.SuspendLayout();
            this.verticalSplitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMenu
            // 
            this.toolMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnConnect,
            this.toolStripSeparator1,
            this.toolBtnArea,
            this.toolBtnIteration,
            this.toolStripSeparator2,
            this.toolBtnTaskTemplate,
            this.toolStripSeparator3,
            this.tooBtnAbout});
            this.toolMenu.Location = new System.Drawing.Point(0, 0);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(1234, 25);
            this.toolMenu.TabIndex = 18;
            // 
            // toolBtnConnect
            // 
            this.toolBtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnConnect.Name = "toolBtnConnect";
            this.toolBtnConnect.Size = new System.Drawing.Size(110, 22);
            this.toolBtnConnect.Text = "Connect to Project";
            this.toolBtnConnect.ToolTipText = "Click to connect to another project";
            this.toolBtnConnect.Click += new System.EventHandler(this.toolBtnConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolBtnArea
            // 
            this.toolBtnArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnArea.Name = "toolBtnArea";
            this.toolBtnArea.Size = new System.Drawing.Size(71, 22);
            this.toolBtnArea.Text = "Area: Any";
            this.toolBtnArea.ToolTipText = "Select an area";
            // 
            // toolBtnIteration
            // 
            this.toolBtnIteration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnIteration.Name = "toolBtnIteration";
            this.toolBtnIteration.Size = new System.Drawing.Size(91, 22);
            this.toolBtnIteration.Text = "Iteration: Any";
            this.toolBtnIteration.ToolTipText = "Select an iteration";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolBtnTaskTemplate
            // 
            this.toolBtnTaskTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTaskTemplate.Name = "toolBtnTaskTemplate";
            this.toolBtnTaskTemplate.Size = new System.Drawing.Size(147, 22);
            this.toolBtnTaskTemplate.Text = "Task Template: Minimal";
            this.toolBtnTaskTemplate.ToolTipText = "Select a template form for addition of new tasks";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tooBtnAbout
            // 
            this.tooBtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tooBtnAbout.Image = ((System.Drawing.Image)(resources.GetObject("tooBtnAbout.Image")));
            this.tooBtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooBtnAbout.Name = "tooBtnAbout";
            this.tooBtnAbout.Size = new System.Drawing.Size(44, 22);
            this.tooBtnAbout.Text = "About";
            this.tooBtnAbout.Click += new System.EventHandler(this.tooBtnAbout_Click);
            // 
            // horizontalSplitContainer
            // 
            this.horizontalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.horizontalSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.horizontalSplitContainer.Name = "horizontalSplitContainer";
            this.horizontalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // horizontalSplitContainer.Panel1
            // 
            this.horizontalSplitContainer.Panel1.Controls.Add(this.verticalSplitContainer);
            // 
            // horizontalSplitContainer.Panel2
            // 
            this.horizontalSplitContainer.Panel2.Controls.Add(this.wbWorkItemDetails);
            this.horizontalSplitContainer.Size = new System.Drawing.Size(1234, 697);
            this.horizontalSplitContainer.SplitterDistance = 607;
            this.horizontalSplitContainer.TabIndex = 22;
            // 
            // verticalSplitContainer
            // 
            this.verticalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.verticalSplitContainer.Name = "verticalSplitContainer";
            // 
            // verticalSplitContainer.Panel1
            // 
            this.verticalSplitContainer.Panel1.Controls.Add(this.treeWorkItems);
            this.verticalSplitContainer.Panel1.Controls.Add(this.panel1);
            // 
            // verticalSplitContainer.Panel2
            // 
            this.verticalSplitContainer.Panel2.Controls.Add(this.pnlTaskTemplate);
            this.verticalSplitContainer.Size = new System.Drawing.Size(1234, 607);
            this.verticalSplitContainer.SplitterDistance = 410;
            this.verticalSplitContainer.TabIndex = 22;
            // 
            // treeWorkItems
            // 
            this.treeWorkItems.AllowDrop = true;
            this.treeWorkItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeWorkItems.HideSelection = false;
            this.treeWorkItems.ImageIndex = 0;
            this.treeWorkItems.ImageList = this.treeImages;
            this.treeWorkItems.Location = new System.Drawing.Point(0, 34);
            this.treeWorkItems.Name = "treeWorkItems";
            this.treeWorkItems.SelectedImageIndex = 0;
            this.treeWorkItems.Size = new System.Drawing.Size(410, 573);
            this.treeWorkItems.TabIndex = 0;
            this.treeWorkItems.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeWorkItems_ItemDrag);
            this.treeWorkItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeWorkItems_AfterSelect);
            this.treeWorkItems.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeWorkItems_DragDrop);
            this.treeWorkItems.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeWorkItems_DragEnter);
            this.treeWorkItems.DoubleClick += new System.EventHandler(this.treeWorkItems_DoubleClick);
            // 
            // treeImages
            // 
            this.treeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImages.ImageStream")));
            this.treeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImages.Images.SetKeyName(0, "transparent.png");
            this.treeImages.Images.SetKeyName(1, "blue.png");
            this.treeImages.Images.SetKeyName(2, "yellow.png");
            this.treeImages.Images.SetKeyName(3, "violet.png");
            this.treeImages.Images.SetKeyName(4, "red.png");
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbWorkItemId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLoadSpecifiedItem);
            this.panel1.Controls.Add(this.btnLoadItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 34);
            this.panel1.TabIndex = 1;
            // 
            // tbWorkItemId
            // 
            this.tbWorkItemId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbWorkItemId.Location = new System.Drawing.Point(232, 6);
            this.tbWorkItemId.MaxLength = 9;
            this.tbWorkItemId.Name = "tbWorkItemId";
            this.tbWorkItemId.Size = new System.Drawing.Size(69, 23);
            this.tbWorkItemId.TabIndex = 3;
            this.tbWorkItemId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWorkItemId_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "or go to work item";
            // 
            // btnLoadSpecifiedItem
            // 
            this.btnLoadSpecifiedItem.Location = new System.Drawing.Point(307, 3);
            this.btnLoadSpecifiedItem.Name = "btnLoadSpecifiedItem";
            this.btnLoadSpecifiedItem.Size = new System.Drawing.Size(37, 27);
            this.btnLoadSpecifiedItem.TabIndex = 1;
            this.btnLoadSpecifiedItem.Text = "Go";
            this.btnLoadSpecifiedItem.UseVisualStyleBackColor = true;
            this.btnLoadSpecifiedItem.Click += new System.EventHandler(this.btnLoadSpecifiedItem_Click);
            // 
            // btnLoadItems
            // 
            this.btnLoadItems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadItems.Location = new System.Drawing.Point(3, 3);
            this.btnLoadItems.Name = "btnLoadItems";
            this.btnLoadItems.Size = new System.Drawing.Size(112, 27);
            this.btnLoadItems.TabIndex = 0;
            this.btnLoadItems.Text = "Load All Items";
            this.btnLoadItems.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLoadItems.UseVisualStyleBackColor = true;
            this.btnLoadItems.Click += new System.EventHandler(this.btnLoadItems_Click);
            // 
            // pnlTaskTemplate
            // 
            this.pnlTaskTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTaskTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaskTemplate.Location = new System.Drawing.Point(0, 0);
            this.pnlTaskTemplate.Name = "pnlTaskTemplate";
            this.pnlTaskTemplate.Size = new System.Drawing.Size(820, 607);
            this.pnlTaskTemplate.TabIndex = 0;
            // 
            // wbWorkItemDetails
            // 
            this.wbWorkItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbWorkItemDetails.Location = new System.Drawing.Point(0, 0);
            this.wbWorkItemDetails.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbWorkItemDetails.Name = "wbWorkItemDetails";
            this.wbWorkItemDetails.Size = new System.Drawing.Size(1234, 86);
            this.wbWorkItemDetails.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 722);
            this.Controls.Add(this.horizontalSplitContainer);
            this.Controls.Add(this.toolMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TFS Tasks Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.horizontalSplitContainer.Panel1.ResumeLayout(false);
            this.horizontalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.horizontalSplitContainer)).EndInit();
            this.horizontalSplitContainer.ResumeLayout(false);
            this.verticalSplitContainer.Panel1.ResumeLayout(false);
            this.verticalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.verticalSplitContainer)).EndInit();
            this.verticalSplitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.ToolStripButton toolBtnConnect;
        private System.Windows.Forms.ToolStripDropDownButton toolBtnArea;
        private System.Windows.Forms.ToolStripDropDownButton toolBtnIteration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolBtnTaskTemplate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tooBtnAbout;
        private System.Windows.Forms.SplitContainer horizontalSplitContainer;
        private System.Windows.Forms.SplitContainer verticalSplitContainer;
        private System.Windows.Forms.TreeView treeWorkItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbWorkItemId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadSpecifiedItem;
        private System.Windows.Forms.Button btnLoadItems;
        private System.Windows.Forms.Panel pnlTaskTemplate;
        private System.Windows.Forms.WebBrowser wbWorkItemDetails;
        private System.Windows.Forms.ImageList treeImages;
    }
}

