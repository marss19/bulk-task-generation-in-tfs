namespace Marss.TasksGenerator
{
    partial class EditTemplateForm
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
            this.tbTemplateName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddStaticField = new System.Windows.Forms.Button();
            this.btnRemoveStaticField = new System.Windows.Forms.Button();
            this.btnAddDynamicField = new System.Windows.Forms.Button();
            this.btnRemoveDynamicField = new System.Windows.Forms.Button();
            this.lbAllFields = new System.Windows.Forms.ListBox();
            this.lbStaticFields = new System.Windows.Forms.ListBox();
            this.lbDynamicFields = new System.Windows.Forms.ListBox();
            this.btnStaticUp = new System.Windows.Forms.Button();
            this.btnStaticDown = new System.Windows.Forms.Button();
            this.btnDynamicDown = new System.Windows.Forms.Button();
            this.btnDynamicUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbTemplateName
            // 
            this.tbTemplateName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTemplateName.Location = new System.Drawing.Point(114, 2);
            this.tbTemplateName.Name = "tbTemplateName";
            this.tbTemplateName.Size = new System.Drawing.Size(309, 20);
            this.tbTemplateName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Template Name";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(824, 442);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Task Fields";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(538, 253);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Dynamic Fields";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(894, 442);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 27);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(848, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 134);
            this.label5.TabIndex = 13;
            this.label5.Text = "* an unique value can be assigned to each task; will be presented as columns in t" +
    "he grid on the Add Tasks form";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(848, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 120);
            this.label6.TabIndex = 16;
            this.label6.Text = "* the same value can be assigned for all new tasks; will be presented as separate" +
    " controls on the Add Tasks form";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(538, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Static Fields";
            // 
            // btnAddStaticField
            // 
            this.btnAddStaticField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStaticField.Location = new System.Drawing.Point(429, 63);
            this.btnAddStaticField.Name = "btnAddStaticField";
            this.btnAddStaticField.Size = new System.Drawing.Size(102, 27);
            this.btnAddStaticField.TabIndex = 17;
            this.btnAddStaticField.Text = ">>";
            this.btnAddStaticField.UseVisualStyleBackColor = true;
            this.btnAddStaticField.Click += new System.EventHandler(this.btnAddStaticField_Click);
            // 
            // btnRemoveStaticField
            // 
            this.btnRemoveStaticField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveStaticField.Location = new System.Drawing.Point(429, 96);
            this.btnRemoveStaticField.Name = "btnRemoveStaticField";
            this.btnRemoveStaticField.Size = new System.Drawing.Size(102, 27);
            this.btnRemoveStaticField.TabIndex = 18;
            this.btnRemoveStaticField.Text = "<<";
            this.btnRemoveStaticField.UseVisualStyleBackColor = true;
            this.btnRemoveStaticField.Click += new System.EventHandler(this.btnRemoveStaticField_Click);
            // 
            // btnAddDynamicField
            // 
            this.btnAddDynamicField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDynamicField.Location = new System.Drawing.Point(433, 294);
            this.btnAddDynamicField.Name = "btnAddDynamicField";
            this.btnAddDynamicField.Size = new System.Drawing.Size(102, 27);
            this.btnAddDynamicField.TabIndex = 19;
            this.btnAddDynamicField.Text = ">>";
            this.btnAddDynamicField.UseVisualStyleBackColor = true;
            this.btnAddDynamicField.Click += new System.EventHandler(this.btnAddDynamicField_Click);
            // 
            // btnRemoveDynamicField
            // 
            this.btnRemoveDynamicField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveDynamicField.Location = new System.Drawing.Point(433, 327);
            this.btnRemoveDynamicField.Name = "btnRemoveDynamicField";
            this.btnRemoveDynamicField.Size = new System.Drawing.Size(102, 27);
            this.btnRemoveDynamicField.TabIndex = 20;
            this.btnRemoveDynamicField.Text = "<<";
            this.btnRemoveDynamicField.UseVisualStyleBackColor = true;
            this.btnRemoveDynamicField.Click += new System.EventHandler(this.btnRemoveDynamicField_Click);
            // 
            // lbAllFields
            // 
            this.lbAllFields.FormattingEnabled = true;
            this.lbAllFields.Location = new System.Drawing.Point(114, 34);
            this.lbAllFields.Name = "lbAllFields";
            this.lbAllFields.Size = new System.Drawing.Size(309, 394);
            this.lbAllFields.TabIndex = 21;
            // 
            // lbStaticFields
            // 
            this.lbStaticFields.FormattingEnabled = true;
            this.lbStaticFields.Location = new System.Drawing.Point(541, 63);
            this.lbStaticFields.Name = "lbStaticFields";
            this.lbStaticFields.Size = new System.Drawing.Size(301, 160);
            this.lbStaticFields.TabIndex = 22;
            // 
            // lbDynamicFields
            // 
            this.lbDynamicFields.FormattingEnabled = true;
            this.lbDynamicFields.Location = new System.Drawing.Point(541, 270);
            this.lbDynamicFields.Name = "lbDynamicFields";
            this.lbDynamicFields.Size = new System.Drawing.Size(301, 160);
            this.lbDynamicFields.TabIndex = 23;
            // 
            // btnStaticUp
            // 
            this.btnStaticUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaticUp.Location = new System.Drawing.Point(686, 34);
            this.btnStaticUp.Name = "btnStaticUp";
            this.btnStaticUp.Size = new System.Drawing.Size(70, 27);
            this.btnStaticUp.TabIndex = 24;
            this.btnStaticUp.Text = "Move Up";
            this.btnStaticUp.UseVisualStyleBackColor = true;
            this.btnStaticUp.Click += new System.EventHandler(this.btnStaticUp_Click);
            // 
            // btnStaticDown
            // 
            this.btnStaticDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaticDown.Location = new System.Drawing.Point(762, 34);
            this.btnStaticDown.Name = "btnStaticDown";
            this.btnStaticDown.Size = new System.Drawing.Size(80, 27);
            this.btnStaticDown.TabIndex = 25;
            this.btnStaticDown.Text = "Move Down";
            this.btnStaticDown.UseVisualStyleBackColor = true;
            this.btnStaticDown.Click += new System.EventHandler(this.btnStaticDown_Click);
            // 
            // btnDynamicDown
            // 
            this.btnDynamicDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDynamicDown.Location = new System.Drawing.Point(762, 239);
            this.btnDynamicDown.Name = "btnDynamicDown";
            this.btnDynamicDown.Size = new System.Drawing.Size(78, 27);
            this.btnDynamicDown.TabIndex = 27;
            this.btnDynamicDown.Text = "Move Down";
            this.btnDynamicDown.UseVisualStyleBackColor = true;
            this.btnDynamicDown.Click += new System.EventHandler(this.btnDynamicDown_Click);
            // 
            // btnDynamicUp
            // 
            this.btnDynamicUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDynamicUp.Location = new System.Drawing.Point(678, 239);
            this.btnDynamicUp.Name = "btnDynamicUp";
            this.btnDynamicUp.Size = new System.Drawing.Size(78, 27);
            this.btnDynamicUp.TabIndex = 26;
            this.btnDynamicUp.Text = "Move Up";
            this.btnDynamicUp.UseVisualStyleBackColor = true;
            this.btnDynamicUp.Click += new System.EventHandler(this.btnDynamicUp_Click);
            // 
            // EditTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 481);
            this.Controls.Add(this.btnDynamicDown);
            this.Controls.Add(this.btnDynamicUp);
            this.Controls.Add(this.btnStaticDown);
            this.Controls.Add(this.btnStaticUp);
            this.Controls.Add(this.lbDynamicFields);
            this.Controls.Add(this.lbStaticFields);
            this.Controls.Add(this.lbAllFields);
            this.Controls.Add(this.btnRemoveDynamicField);
            this.Controls.Add(this.btnAddDynamicField);
            this.Controls.Add(this.btnRemoveStaticField);
            this.Controls.Add(this.btnAddStaticField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTemplateName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditTemplateFormcs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbTemplateName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddStaticField;
        private System.Windows.Forms.Button btnRemoveStaticField;
        private System.Windows.Forms.Button btnAddDynamicField;
        private System.Windows.Forms.Button btnRemoveDynamicField;
        private System.Windows.Forms.ListBox lbAllFields;
        private System.Windows.Forms.ListBox lbStaticFields;
        private System.Windows.Forms.ListBox lbDynamicFields;
        private System.Windows.Forms.Button btnStaticUp;
        private System.Windows.Forms.Button btnStaticDown;
        private System.Windows.Forms.Button btnDynamicDown;
        private System.Windows.Forms.Button btnDynamicUp;
    }
}