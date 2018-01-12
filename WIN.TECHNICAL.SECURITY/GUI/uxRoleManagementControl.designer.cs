namespace WIN.SECURITY.GUI
{
    partial class uxRoleManagementControl
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
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Utenti associati", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Profili associati", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uxRoleManagementControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uxRolesListBox = new System.Windows.Forms.ListBox();
            this.uxAssociationsGroupBox = new System.Windows.Forms.GroupBox();
            this.uxAssociationsListView = new System.Windows.Forms.ListView();
            this.RoleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxAddProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.uxRoleLabel = new System.Windows.Forms.Label();
            this.RolesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeroleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editroleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRenameRole = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtAddRole = new System.Windows.Forms.ToolStripTextBox();
            this.UserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutuserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteuserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edituserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uxDeleteProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxProfilePropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSaveToolStrip = new System.Windows.Forms.ToolStrip();
            this.uxSaveStripButton = new System.Windows.Forms.ToolStripButton();
            this.uxCancelStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.uxAssociationsGroupBox.SuspendLayout();
            this.RoleMenu.SuspendLayout();
            this.RolesMenu.SuspendLayout();
            this.UserMenu.SuspendLayout();
            this.ProfileMenu.SuspendLayout();
            this.uxSaveToolStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uxAssociationsGroupBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 489);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uxRolesListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 483);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleziona ruolo";
            // 
            // uxRolesListBox
            // 
            this.uxRolesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxRolesListBox.FormattingEnabled = true;
            this.uxRolesListBox.Location = new System.Drawing.Point(3, 17);
            this.uxRolesListBox.Name = "uxRolesListBox";
            this.uxRolesListBox.Size = new System.Drawing.Size(192, 459);
            this.uxRolesListBox.TabIndex = 0;
            this.uxRolesListBox.DoubleClick += new System.EventHandler(this.lbRoles_DoubleClick);
            this.uxRolesListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbRoles_MouseUp);
            // 
            // uxAssociationsGroupBox
            // 
            this.uxAssociationsGroupBox.Controls.Add(this.uxAssociationsListView);
            this.uxAssociationsGroupBox.Controls.Add(this.uxRoleLabel);
            this.uxAssociationsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxAssociationsGroupBox.Enabled = false;
            this.uxAssociationsGroupBox.Location = new System.Drawing.Point(207, 3);
            this.uxAssociationsGroupBox.Name = "uxAssociationsGroupBox";
            this.uxAssociationsGroupBox.Size = new System.Drawing.Size(473, 483);
            this.uxAssociationsGroupBox.TabIndex = 4;
            this.uxAssociationsGroupBox.TabStop = false;
            this.uxAssociationsGroupBox.Text = "Associazioni";
            // 
            // uxAssociationsListView
            // 
            this.uxAssociationsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.uxAssociationsListView.ContextMenuStrip = this.RoleMenu;
            listViewGroup3.Header = "Utenti associati";
            listViewGroup3.Name = "lvgUsers";
            listViewGroup3.Tag = "Utenti associati";
            listViewGroup4.Header = "Profili associati";
            listViewGroup4.Name = "lvgProfiles";
            listViewGroup4.Tag = "Profili associati";
            this.uxAssociationsListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.uxAssociationsListView.LargeImageList = this.ImageList;
            this.uxAssociationsListView.Location = new System.Drawing.Point(3, 40);
            this.uxAssociationsListView.MultiSelect = false;
            this.uxAssociationsListView.Name = "uxAssociationsListView";
            this.uxAssociationsListView.Size = new System.Drawing.Size(467, 440);
            this.uxAssociationsListView.SmallImageList = this.ImageList;
            this.uxAssociationsListView.TabIndex = 12;
            this.uxAssociationsListView.UseCompatibleStateImageBehavior = false;
            this.uxAssociationsListView.DoubleClick += new System.EventHandler(this.lvAssociations_DoubleClick);
            this.uxAssociationsListView.SelectedIndexChanged += new System.EventHandler(this.lvAssociations_SelectedIndexChanged);
            // 
            // RoleMenu
            // 
            this.RoleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewUserToolStripMenuItem,
            this.uxAddProfileMenuItem,
            this.toolStripMenuItem2,
            this.pasteUser});
            this.RoleMenu.Name = "RoleMenu";
            this.RoleMenu.Size = new System.Drawing.Size(162, 76);
            this.RoleMenu.Opening += new System.ComponentModel.CancelEventHandler(this.RoleMenu_Opening);
            // 
            // addNewUserToolStripMenuItem
            // 
            this.addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            this.addNewUserToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addNewUserToolStripMenuItem.Text = "Aggiungi utente";
            this.addNewUserToolStripMenuItem.Click += new System.EventHandler(this.addNewUserToolStripMenuItem_Click);
            // 
            // uxAddProfileMenuItem
            // 
            this.uxAddProfileMenuItem.Name = "uxAddProfileMenuItem";
            this.uxAddProfileMenuItem.Size = new System.Drawing.Size(161, 22);
            this.uxAddProfileMenuItem.Text = "Aggiungi profilo";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 6);
            // 
            // pasteUser
            // 
            this.pasteUser.Enabled = false;
            this.pasteUser.Name = "pasteUser";
            this.pasteUser.Size = new System.Drawing.Size(161, 22);
            this.pasteUser.Text = "Incolla utente";
            this.pasteUser.Click += new System.EventHandler(this.pasteUser_Click);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "User");
            this.ImageList.Images.SetKeyName(1, "Profile");
            // 
            // uxRoleLabel
            // 
            this.uxRoleLabel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.uxRoleLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uxRoleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxRoleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxRoleLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.uxRoleLabel.Location = new System.Drawing.Point(3, 17);
            this.uxRoleLabel.Name = "uxRoleLabel";
            this.uxRoleLabel.Size = new System.Drawing.Size(467, 20);
            this.uxRoleLabel.TabIndex = 11;
            this.uxRoleLabel.Text = "Gruppo corrente";
            this.uxRoleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RolesMenu
            // 
            this.RolesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeroleToolStripMenuItem,
            this.editroleToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addNewRoleToolStripMenuItem});
            this.RolesMenu.Name = "RolesMenu";
            this.RolesMenu.Size = new System.Drawing.Size(187, 76);
            // 
            // removeroleToolStripMenuItem
            // 
            this.removeroleToolStripMenuItem.Name = "removeroleToolStripMenuItem";
            this.removeroleToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.removeroleToolStripMenuItem.Text = "Elimina";
            this.removeroleToolStripMenuItem.Click += new System.EventHandler(this.removeroleToolStripMenuItem_Click);
            // 
            // editroleToolStripMenuItem
            // 
            this.editroleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtRenameRole});
            this.editroleToolStripMenuItem.Name = "editroleToolStripMenuItem";
            this.editroleToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.editroleToolStripMenuItem.Text = "Rinomina";
            // 
            // txtRenameRole
            // 
            this.txtRenameRole.Name = "txtRenameRole";
            this.txtRenameRole.Size = new System.Drawing.Size(100, 21);
            this.txtRenameRole.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRenameRole_KeyUp);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(183, 6);
            // 
            // addNewRoleToolStripMenuItem
            // 
            this.addNewRoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAddRole});
            this.addNewRoleToolStripMenuItem.Name = "addNewRoleToolStripMenuItem";
            this.addNewRoleToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addNewRoleToolStripMenuItem.Text = "Aggiungi nuovo ruolo";
            // 
            // txtAddRole
            // 
            this.txtAddRole.Name = "txtAddRole";
            this.txtAddRole.Size = new System.Drawing.Size(100, 21);
            this.txtAddRole.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddRole_KeyUp);
            // 
            // UserMenu
            // 
            this.UserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutuserToolStripMenuItem,
            this.deleteuserToolStripMenuItem,
            this.edituserToolStripMenuItem});
            this.UserMenu.Name = "UserMenu";
            this.UserMenu.Size = new System.Drawing.Size(153, 70);
            this.UserMenu.Text = "User";
            // 
            // cutuserToolStripMenuItem
            // 
            this.cutuserToolStripMenuItem.Name = "cutuserToolStripMenuItem";
            this.cutuserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutuserToolStripMenuItem.Text = "Taglia utente";
            this.cutuserToolStripMenuItem.Click += new System.EventHandler(this.cutuserToolStripMenuItem_Click);
            // 
            // deleteuserToolStripMenuItem
            // 
            this.deleteuserToolStripMenuItem.Name = "deleteuserToolStripMenuItem";
            this.deleteuserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteuserToolStripMenuItem.Text = "Elimina utente";
            this.deleteuserToolStripMenuItem.Click += new System.EventHandler(this.deleteuserToolStripMenuItem_Click);
            // 
            // edituserToolStripMenuItem
            // 
            this.edituserToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edituserToolStripMenuItem.Name = "edituserToolStripMenuItem";
            this.edituserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.edituserToolStripMenuItem.Text = "Proprietà";
            this.edituserToolStripMenuItem.Click += new System.EventHandler(this.edituserToolStripMenuItem_Click);
            // 
            // ProfileMenu
            // 
            this.ProfileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxDeleteProfileMenuItem,
            this.uxProfilePropertiesMenuItem});
            this.ProfileMenu.Name = "ProfileMenu";
            this.ProfileMenu.Size = new System.Drawing.Size(205, 70);
            // 
            // uxDeleteProfileMenuItem
            // 
            this.uxDeleteProfileMenuItem.Name = "uxDeleteProfileMenuItem";
            this.uxDeleteProfileMenuItem.Size = new System.Drawing.Size(204, 22);
            this.uxDeleteProfileMenuItem.Text = "Elimina profilo dal gruppo";
            this.uxDeleteProfileMenuItem.Click += new System.EventHandler(this.deleteprofileToolStripMenuItem_Click);
            // 
            // uxProfilePropertiesMenuItem
            // 
            this.uxProfilePropertiesMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxProfilePropertiesMenuItem.Name = "uxProfilePropertiesMenuItem";
            this.uxProfilePropertiesMenuItem.Size = new System.Drawing.Size(204, 22);
            this.uxProfilePropertiesMenuItem.Text = "Proprieta";
            this.uxProfilePropertiesMenuItem.Click += new System.EventHandler(this.cutprofileToolStripMenuItem_Click);
            // 
            // uxSaveToolStrip
            // 
            this.uxSaveToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.uxSaveToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.uxSaveToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxSaveStripButton,
            this.uxCancelStripButton});
            this.uxSaveToolStrip.Location = new System.Drawing.Point(3, 0);
            this.uxSaveToolStrip.Name = "uxSaveToolStrip";
            this.uxSaveToolStrip.Size = new System.Drawing.Size(157, 39);
            this.uxSaveToolStrip.TabIndex = 5;
            // 
            // uxSaveStripButton
            // 
            this.uxSaveStripButton.Image = ((System.Drawing.Image)(resources.GetObject("uxSaveStripButton.Image")));
            this.uxSaveStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxSaveStripButton.Name = "uxSaveStripButton";
            this.uxSaveStripButton.Size = new System.Drawing.Size(69, 36);
            this.uxSaveStripButton.Text = "Salva";
            this.uxSaveStripButton.Click += new System.EventHandler(this.uxSaveStripButton_Click);
            // 
            // uxCancelStripButton
            // 
            this.uxCancelStripButton.Image = ((System.Drawing.Image)(resources.GetObject("uxCancelStripButton.Image")));
            this.uxCancelStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.uxCancelStripButton.Name = "uxCancelStripButton";
            this.uxCancelStripButton.Size = new System.Drawing.Size(78, 36);
            this.uxCancelStripButton.Text = "Annulla";
            this.uxCancelStripButton.Click += new System.EventHandler(this.uxCancelStripButton_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(683, 489);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(683, 528);
            this.toolStripContainer1.TabIndex = 4;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.uxSaveToolStrip);
            // 
            // uxRoleManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "uxRoleManagementControl";
            this.Size = new System.Drawing.Size(683, 528);
            this.Load += new System.EventHandler(this.RoleManagementControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.uxAssociationsGroupBox.ResumeLayout(false);
            this.RoleMenu.ResumeLayout(false);
            this.RolesMenu.ResumeLayout(false);
            this.UserMenu.ResumeLayout(false);
            this.ProfileMenu.ResumeLayout(false);
            this.uxSaveToolStrip.ResumeLayout(false);
            this.uxSaveToolStrip.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox uxRolesListBox;
        private System.Windows.Forms.GroupBox uxAssociationsGroupBox;
        private System.Windows.Forms.Label uxRoleLabel;
        private System.Windows.Forms.ContextMenuStrip RolesMenu;
        private System.Windows.Forms.ToolStripMenuItem removeroleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editroleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addNewRoleToolStripMenuItem;
        private System.Windows.Forms.ListView uxAssociationsListView;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.ContextMenuStrip UserMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteuserToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ProfileMenu;
        private System.Windows.Forms.ContextMenuStrip RoleMenu;
        private System.Windows.Forms.ToolStripMenuItem edituserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutuserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxDeleteProfileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxProfilePropertiesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxAddProfileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pasteUser;
        private System.Windows.Forms.ToolStripTextBox txtRenameRole;
        private System.Windows.Forms.ToolStripTextBox txtAddRole;
        private System.Windows.Forms.ToolStrip uxSaveToolStrip;
        private System.Windows.Forms.ToolStripButton uxSaveStripButton;
        private System.Windows.Forms.ToolStripButton uxCancelStripButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    }
}
