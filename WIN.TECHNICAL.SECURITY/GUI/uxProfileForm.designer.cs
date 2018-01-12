namespace WIN.SECURITY.GUI
{
    partial class uxProfileForm
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
            this.uxOKButton = new System.Windows.Forms.Button();
            this.uxCancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uxTreeView = new System.Windows.Forms.TreeView();
            this.uxPermissionsListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxOKButton
            // 
            this.uxOKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxOKButton.Location = new System.Drawing.Point(471, 14);
            this.uxOKButton.Name = "uxOKButton";
            this.uxOKButton.Size = new System.Drawing.Size(85, 48);
            this.uxOKButton.TabIndex = 19;
            this.uxOKButton.Text = "OK";
            this.uxOKButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.uxOKButton.UseVisualStyleBackColor = true;
            this.uxOKButton.Click += new System.EventHandler(this.uxOKButton_Click);
            // 
            // uxCancelButton
            // 
            this.uxCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancelButton.Location = new System.Drawing.Point(562, 14);
            this.uxCancelButton.Name = "uxCancelButton";
            this.uxCancelButton.Size = new System.Drawing.Size(85, 48);
            this.uxCancelButton.TabIndex = 20;
            this.uxCancelButton.TabStop = false;
            this.uxCancelButton.Text = "Annulla";
            this.uxCancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.uxCancelButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.uxCancelButton);
            this.panel1.Controls.Add(this.uxOKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 321);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 73);
            this.panel1.TabIndex = 24;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(26, 87);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uxTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.uxPermissionsListBox);
            this.splitContainer1.Size = new System.Drawing.Size(576, 230);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 21;
            // 
            // uxTreeView
            // 
            this.uxTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxTreeView.Location = new System.Drawing.Point(0, 0);
            this.uxTreeView.Name = "uxTreeView";
            this.uxTreeView.Size = new System.Drawing.Size(255, 230);
            this.uxTreeView.TabIndex = 22;
            this.uxTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.uxTreeView_AfterSelect);
            // 
            // uxPermissionsListBox
            // 
            this.uxPermissionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPermissionsListBox.FormattingEnabled = true;
            this.uxPermissionsListBox.Location = new System.Drawing.Point(0, 0);
            this.uxPermissionsListBox.Name = "uxPermissionsListBox";
            this.uxPermissionsListBox.Size = new System.Drawing.Size(317, 228);
            this.uxPermissionsListBox.TabIndex = 18;
            this.uxPermissionsListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.uxPermissionsListBox_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(27, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Descrizione";
            // 
            // uxDescriptionTextBox
            // 
            this.uxDescriptionTextBox.Location = new System.Drawing.Point(26, 60);
            this.uxDescriptionTextBox.Name = "uxDescriptionTextBox";
            this.uxDescriptionTextBox.Size = new System.Drawing.Size(300, 21);
            this.uxDescriptionTextBox.TabIndex = 17;
            // 
            // uxProfileForm
            // 
            this.AcceptButton = this.uxOKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uxCancelButton;
            this.ClientSize = new System.Drawing.Size(684, 394);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uxDescriptionTextBox);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "uxProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proprietà profilo";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uxProfileForm_KeyUp);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox uxPermissionsListBox;
        private System.Windows.Forms.TextBox uxDescriptionTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uxOKButton;
        private System.Windows.Forms.Button uxCancelButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView uxTreeView;
        private System.Windows.Forms.Panel panel1;
    }
}