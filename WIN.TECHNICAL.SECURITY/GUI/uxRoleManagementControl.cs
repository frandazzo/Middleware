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
    public partial class uxRoleManagementControl : UserControl, IDisposable
    {
        private IList<IRole> _roles;
        private List<AssociationData> _associations = new List<AssociationData>();
        private IUser _cuttedUser = null;
        private IRole _currentRole;
        List<IRole> _deletedRoles = new List<IRole>();

        public uxRoleManagementControl()
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
        }

        private void RoleManagementControl_Load(object sender, EventArgs e)
        {
            
        }

        public void InitializeRoles()
        { 
            LoadRoles();
        }

        private void LoadRoles()
        {
            //using (IPersistenceService persistence = PersistenceServiceFactory.Create())
            //{
            //    _roles = persistence.GetAll<Role>();
                
                
            //}
            _roles = SecurityManager.Instance.SecureDataAccess.GetRoles();
            RemoveAdministratorsRole();
            uxRolesListBox.DataSource = _roles;

        }

        private void RemoveAdministratorsRole()
        {
            Role role = null;
            foreach (Role r in _roles)
            {
                if (r.Description == "Administrator")
                    role = r;
            }

            if(role !=null)
                _roles.Remove(role);
        }

        private void lbRoles_MouseUp(object sender, MouseEventArgs e)
        {
            if (uxRolesListBox.SelectedValue != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    removeroleToolStripMenuItem.Enabled = true;
                    editroleToolStripMenuItem.Enabled = true;
                    addNewRoleToolStripMenuItem.Enabled = true;
                    
                    this.txtRenameRole.Text = ((Role)(uxRolesListBox.SelectedValue)).Description;
                    this.RolesMenu.Show(uxRolesListBox, e.Location);
                }
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    removeroleToolStripMenuItem.Enabled = false;
                    editroleToolStripMenuItem.Enabled = false;
                    addNewRoleToolStripMenuItem.Enabled = true;
                    
                    this.txtRenameRole.Text = "";
                    this.RolesMenu.Show(uxRolesListBox, e.Location);
                }
            }
        }

        private void lbRoles_DoubleClick(object sender, EventArgs e)
        {
            if (uxRolesListBox.SelectedValue != null)
            {
                _currentRole = uxRolesListBox.SelectedValue as Role;
                LoadAssociations();
            }
        }

        private void LoadAssociations()
        {
            if (_currentRole == null)
                return;
            
            uxAssociationsListView.ContextMenuStrip = RoleMenu;
            
            uxAssociationsGroupBox.Enabled = true;
            uxRoleLabel.Text = "Gruppo corrente: " + uxRolesListBox.SelectedValue.ToString();

            uxAssociationsListView.Items.Clear();

            //load users
            foreach (User user in _currentRole.Users)
            {
                ListViewItem item = new ListViewItem(user.Username, 0, uxAssociationsListView.Groups[0]);
                uxAssociationsListView.Items.Add(item);

                AssociationData a = new AssociationData();
                a.Role = (Role)_currentRole;
                a.User = user;
                a.ListViewItem = item;
                _associations.Add(a);
            }

            //load profiles
            foreach(Profile profile in _currentRole.Profiles)
            {
                ListViewItem item = new ListViewItem(profile.Description, 1, uxAssociationsListView.Groups[1]);
                uxAssociationsListView.Items.Add(item);
                
                AssociationData a = new AssociationData();
                a.Role = (Role)_currentRole;
                a.Profile = profile;
                a.ListViewItem = item;
                _associations.Add(a);
            }
        }

        
        private void lvAssociations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (uxAssociationsListView.SelectedItems.Count > 0)
            {
                ListViewItem item = uxAssociationsListView.SelectedItems[0];

                //if is user
                if (item.Group == uxAssociationsListView.Groups[0])
                {
                    uxAssociationsListView.ContextMenuStrip = UserMenu;
                }
                else //profile
                {
                    uxAssociationsListView.ContextMenuStrip = ProfileMenu;
                }
            }
            else //nothing selected
            {
                uxAssociationsListView.ContextMenuStrip = RoleMenu;
            }
        }


        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uxUserForm f = new uxUserForm((Role)uxRolesListBox.SelectedValue);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.LoadAssociations();
            }
        }

        private void deleteuserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssociationData association = FindAssociation(uxAssociationsListView.SelectedItems[0]);
            association.Role.Users.Remove(association.User);
            LoadAssociations();
        }

        private AssociationData FindAssociation(ListViewItem item)
        {
            foreach (AssociationData a in _associations)
            {
                if (a.ListViewItem == item)
                    return a;
            }

            return null;
        }
        
        void IDisposable.Dispose()
        {
            _associations.Clear();
            _roles.Clear();

            base.Dispose();
        }

        private void edituserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUserPropertiesForm();
        }

        private void OpenUserPropertiesForm()
        {
            AssociationData association = FindAssociation(uxAssociationsListView.SelectedItems[0]);
            OpenUserPropertiesForm(association);
        }

        private void OpenUserPropertiesForm(AssociationData association)
        {
            if (DialogResult.OK == new uxUserForm(association.User).ShowDialog(this))
                LoadAssociations();
        }

        private void lvAssociations_DoubleClick(object sender, EventArgs e)
        {
            AssociationData association = FindAssociation(uxAssociationsListView.SelectedItems[0]);
            if (association.User != null)
            {
                OpenUserPropertiesForm(association);
            }
            else if (association.Profile != null)
            {
                OpenProfilePropertiesForm(association);
            }
        }

        private void cutprofileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProfilePropertiesForm();
        }

        private void OpenProfilePropertiesForm()
        {
            AssociationData association = FindAssociation(uxAssociationsListView.SelectedItems[0]);
            if (association.Profile != null)
            {
                OpenProfilePropertiesForm(association);
            }
        }

        private void OpenProfilePropertiesForm(AssociationData association)
        {
            new uxProfileForm(association.Profile, true).ShowDialog(this);
        }

        private void deleteprofileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteProfileFromRole();
        }

        private void DeleteProfileFromRole()
        {
            AssociationData association = FindAssociation(uxAssociationsListView.SelectedItems[0]);
            association.Role.Profiles.Remove(association.Profile);
            LoadAssociations();
        }

        private void cutuserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssociationData association = FindAssociation(uxAssociationsListView.SelectedItems[0]);
            _cuttedUser = association.User;
        }

        private void RoleMenu_Opening(object sender, CancelEventArgs e)
        {
            RoleMenu.Items["pasteUser"].Enabled = _cuttedUser != null;

            LoadProfiles();
        }

        private void LoadProfiles()
        {

            IList<IProfile> profiles = SecurityManager.Instance.SecureDataAccess.GetProfiles();

                uxAddProfileMenuItem.DropDownItems.Clear();
                foreach (Profile profile in profiles)
                {
                    ToolStripItem item = new ToolStripMenuItem(profile.Description);
                    item.Click += new EventHandler(item_Click);
                    item.Tag = profile;
                    item.Enabled = !_currentRole.Profiles.Contains(profile);

                    uxAddProfileMenuItem.DropDownItems.Add(item);

                }
            }
        

        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Profile profile = item.Tag as Profile;

            _currentRole.Profiles.Add(profile);
            profile.Roles.Add(_currentRole);
            LoadAssociations();
        }

        private void pasteUser_Click(object sender, EventArgs e)
        {
            _cuttedUser.Role.Users.Remove(_cuttedUser);
            _currentRole.Users.Add(_cuttedUser);
            _cuttedUser.Role = _currentRole;
            _cuttedUser = null;
            LoadAssociations();
        }

        private void removeroleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uxRolesListBox.SelectedValue != null)
            {
                Role role = uxRolesListBox.SelectedValue as Role;

                if(DialogResult.Yes == MessageBox.Show(String.Format(
                    "Sicuro di voler eliminare il ruolo '{0}'?\nLa cancellazione si propagherà anche agli utenti ed i profili compresi nel ruolo", 
                        role.Description),
                    "Conferma eliminazione ruolo", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    _deletedRoles.Add(role);
                    _roles.Remove(role);
                    uxRolesListBox.DataSource = null;
                    uxRolesListBox.DataSource = _roles;
                    uxAssociationsGroupBox.Enabled = false;
                }
            }
        }

        class AssociationData
        {
            private ListViewItem listViewItem;

            public ListViewItem ListViewItem
            {
                get { return listViewItem; }
                set { listViewItem = value; }
            }

            private User user;

            public User User
            {
                get { return user; }
                set { user = value; }
            }

            private Profile profile;

            public Profile Profile
            {
                get { return profile; }
                set { profile = value; }
            }

            private Role role;

            public Role Role
            {
                get { return role; }
                set { role = value; }
            }

        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show((txtRenameRole.Text));
        }

        private void txtRenameRole_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtRenameRole.Text != "")
                {
                    Role role = uxRolesListBox.SelectedValue as Role;

                    if (DialogResult.Yes == MessageBox.Show(
                        String.Format(
                            "Il ruolo '{0}' verrà rinominato in '{1}'. Continuare?",
                            role.Description,
                            this.txtRenameRole.Text),
                        "Conferma rinomina",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    {
                        role.Description = this.txtRenameRole.Text;
                        uxRolesListBox.DataSource = null;
                        uxRolesListBox.DataSource = _roles;
                        RolesMenu.Close();
                    }
                }
            }
        }

        private void txtAddRole_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtAddRole.Text != "")
                {
                    if (DialogResult.Yes == MessageBox.Show(
                        String.Format(
                            "Stai per aggiungere il ruolo '{0}'. Continuare?",
                            txtAddRole.Text,
                            this.txtAddRole.Text),
                        "Conferma rinomina",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    {
                        Role role = new Role();
                        role.Description = this.txtAddRole.Text;
                        _roles.Add(role);
                        uxRolesListBox.DataSource = null;
                        uxRolesListBox.DataSource = _roles;
                        RolesMenu.Close();
                    }
                }
            }
        }

        private void uxSaveStripButton_Click(object sender, EventArgs e)
        {
            //using (IPersistenceService persistence = PersistenceServiceFactory.Create())
            //{
                foreach (Role role in _deletedRoles)
                {
                    SecurityManager.Instance.SecureDataAccess.DeleteRole(role);
                }

                foreach (Role role in _roles)
                {
                    SecurityManager.Instance.SecureDataAccess.SaveRole(role);
                }

                _deletedRoles.Clear();

                MessageBox.Show("Le modifiche sono state registrate. Riavviare l'applicazione per renderle effettive", "Conferma salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void uxCancelStripButton_Click(object sender, EventArgs e)
        {
            LoadRoles();
            uxAssociationsGroupBox.Enabled = false;
        }

    }
}
