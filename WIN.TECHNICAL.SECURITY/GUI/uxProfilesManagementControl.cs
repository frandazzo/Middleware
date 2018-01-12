using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using WIN.SECURITY.Core;


using WIN.SECURITY;


namespace WIN.SECURITY.GUI
{
    public partial class uxProfileManagementControl : UserControl, IDisposable
    {
        IList<IProfile> _profiles;
        IList<IProfile> _deletedProfiles;

        public uxProfileManagementControl()
        {
            InitializeComponent();

            _deletedProfiles = new List<IProfile>();
            _profiles = new List<IProfile>();

            this.Dock = DockStyle.Fill;

            
        }

        public void InitializeProfiles()
        { 
            LoadProfiles();
            LoadListView();
        }

        private void LoadProfiles()
        {
            //using (IPersistenceService persistence = PersistenceServiceFactory.Create())
            //{
            //    _profiles = persistence.GetAll<Profile>();
            //}
            _profiles = SecurityManager.Instance.SecureDataAccess.GetProfiles();
        }
        
        private void LoadListView()
        {
            uxProfilesListView.Items.Clear();
            
            //load profiles
            foreach(IProfile profile in _profiles)
            {
                ListViewItem item = new ListViewItem(profile.Description, 1, uxProfilesListView.Groups[1]);
                uxProfilesListView.Items.Add(item);
                item.Tag = profile;
            }
        }


        private void OpenProfilePropertiesForm()
        {
            Profile profile = uxProfilesListView.SelectedItems[0].Tag as Profile;
            
            if (DialogResult.OK == new uxProfileForm(profile).ShowDialog(this))
                LoadListView();
        }

        private void DeleteProfile()
        {
            Profile profile = uxProfilesListView.SelectedItems[0].Tag as Profile;
            _profiles.Remove(profile);
            _deletedProfiles.Add(profile);
            LoadListView();
        }


        private void uxSaveStripButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            //using (IPersistenceService persistence = PersistenceServiceFactory.Create())
            //{
                foreach (IProfile profile in _deletedProfiles)
                {
                    SecurityManager.Instance.SecureDataAccess.DeleteProfile(profile);
                }

                foreach (IProfile profile in _profiles)
                {
                    SecurityManager.Instance.SecureDataAccess.Save(profile);
                }
            //}

            _deletedProfiles.Clear();

            MessageBox.Show("Le modifiche sono state registrate. Riavviare l'applicazione per renderle effettive", "Salvato", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void uxCancelStripButton_Click(object sender, EventArgs e)
        {
            LoadProfiles();            
            LoadListView();
        }

        private void UpdateContextMenu()
        {
            if (uxProfilesListView.SelectedItems.Count > 0)
            {
                uxPropertiesMenuItem.Enabled = true;
                uxDeleteMenuItem.Enabled = true;
            }
            else //nothing selected
            {
                uxPropertiesMenuItem.Enabled = false;
                uxDeleteMenuItem.Enabled = false;
            }
        }

        private void uxProfilesListView_DoubleClick(object sender, EventArgs e)
        {
            OpenProfilePropertiesForm();
        }

        private void uxAddMenuItem_Click(object sender, EventArgs e)
        {
            AddNew();
        }

        private void AddNew()
        {
            uxProfileForm f = new uxProfileForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                _profiles.Add(f.Value);
                LoadListView();
            }
        }

        private void uxDeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteProfile();
        }

        private void ProfileMenu_Opening(object sender, CancelEventArgs e)
        {
            UpdateContextMenu();
        }
    
    }
}
