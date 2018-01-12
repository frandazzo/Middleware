namespace WIN.SECURITY.GUI
{
    partial class uxProfileManagementControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uxProfileManagementControl));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Utenti associati", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Profili associati", System.Windows.Forms.HorizontalAlignment.Left);
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.ProfileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uxAddMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxSaveToolStrip = new System.Windows.Forms.ToolStrip();
            this.uxSaveStripButton = new System.Windows.Forms.ToolStripButton();
            this.uxCancelStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.uxProfilesListView = new System.Windows.Forms.ListView();
            this.ProfileMenu.SuspendLayout();
            this.uxSaveToolStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "User");
            this.ImageList.Images.SetKeyName(1, "Profile");
            // 
            // ProfileMenu
            // 
            this.ProfileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxAddMenuItem,
            this.uxDeleteMenuItem,
            this.uxPropertiesMenuItem});
            this.ProfileMenu.Name = "ProfileMenu";
            this.ProfileMenu.Size = new System.Drawing.Size(214, 92);
            this.ProfileMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ProfileMenu_Opening);
            // 
            // uxAddMenuItem
            // 
            this.uxAddMenuItem.Name = "uxAddMenuItem";
            this.uxAddMenuItem.Size = new System.Drawing.Size(213, 22);
            this.uxAddMenuItem.Text = "Aggiungi profilo";
            this.uxAddMenuItem.Click += new System.EventHandler(this.uxAddMenuItem_Click);
            // 
            // uxDeleteMenuItem
            // 
            this.uxDeleteMenuItem.Name = "uxDeleteMenuItem";
            this.uxDeleteMenuItem.Size = new System.Drawing.Size(213, 22);
            this.uxDeleteMenuItem.Text = "Elimina profilo da database";
            this.uxDeleteMenuItem.Click += new System.EventHandler(this.uxDeleteMenuItem_Click);
            // 
            // uxPropertiesMenuItem
            // 
            this.uxPropertiesMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxPropertiesMenuItem.Name = "uxPropertiesMenuItem";
            this.uxPropertiesMenuItem.Size = new System.Drawing.Size(213, 22);
            this.uxPropertiesMenuItem.Text = "Proprieta";
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
            this.toolStripContainer1.ContentPanel.Controls.Add(this.uxProfilesListView);
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
            // uxProfilesListView
            // 
            this.uxProfilesListView.ContextMenuStrip = this.ProfileMenu;
            this.uxProfilesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Utenti associati";
            listViewGroup1.Name = "lvgUsers";
            listViewGroup1.Tag = "Utenti associati";
            listViewGroup2.Header = "Profili associati";
            listViewGroup2.Name = "lvgProfiles";
            listViewGroup2.Tag = "Profili associati";
            this.uxProfilesListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.uxProfilesListView.LargeImageList = this.ImageList;
            this.uxProfilesListView.Location = new System.Drawing.Point(0, 0);
            this.uxProfilesListView.MultiSelect = false;
            this.uxProfilesListView.Name = "uxProfilesListView";
            this.uxProfilesListView.Size = new System.Drawing.Size(683, 489);
            this.uxProfilesListView.SmallImageList = this.ImageList;
            this.uxProfilesListView.TabIndex = 13;
            this.uxProfilesListView.UseCompatibleStateImageBehavior = false;
            this.uxProfilesListView.DoubleClick += new System.EventHandler(this.uxProfilesListView_DoubleClick);
            // 
            // uxProfileManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "uxProfileManagementControl";
            this.Size = new System.Drawing.Size(683, 528);
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

        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.ContextMenuStrip ProfileMenu;
        private System.Windows.Forms.ToolStripMenuItem uxPropertiesMenuItem;
        private System.Windows.Forms.ToolStrip uxSaveToolStrip;
        private System.Windows.Forms.ToolStripButton uxSaveStripButton;
        private System.Windows.Forms.ToolStripButton uxCancelStripButton;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ListView uxProfilesListView;
        private System.Windows.Forms.ToolStripMenuItem uxDeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uxAddMenuItem;
    }
}
