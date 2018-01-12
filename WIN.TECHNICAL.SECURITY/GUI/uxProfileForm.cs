using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WIN.SECURITY;
using WIN.SECURITY.Core;
using WIN.SECURITY.Attributes;





namespace WIN.SECURITY.GUI
{
    public partial class uxProfileForm : Form
    {
        private List<IPermission> _permissions;        
        Profile _profile;
        private bool _loading;
        private bool _showFullMethodNames = false;

        public Profile Value
        {
            get { return _profile; }
        }

        public uxProfileForm(Profile profile)
        {
            InitializeComponent();
           


            _profile = profile;
            uxDescriptionTextBox.Text = profile.Description;

            this.Text += profile.Description;

            _permissions = new List<IPermission>(_profile.Permissions);

            LoadTreeView();
        }

        public uxProfileForm()
        {
            InitializeComponent();
           
            
            this.Text = "Nuovo profilo";

            _permissions = new List<IPermission>();
            LoadTreeView();
        }

        public uxProfileForm(Profile profile, bool readOnly) : this(profile)
        {
            uxOKButton.Enabled = false;
        }

        private void LoadTreeView()
        {
            uxTreeView.Nodes.Clear();
            foreach (KeyValuePair<string, Secure> secure in SecurityManager.Instance.SecureMethods)
            {
                TreeNode macroNode = GetMacroNode(secure.Value.MacroArea);
                CheckAreaNode(macroNode, secure.Value.Area);
            }
            uxTreeView.CollapseAll();
        }

        private void CheckAreaNode(TreeNode macroNode, string area)
        {
            if (!macroNode.Nodes.ContainsKey(area))
            {
                TreeNode areaNode = new TreeNode();
                areaNode.Text = area;
                areaNode.Name = area;
                areaNode.Tag = "area";
                macroNode.Nodes.Add(areaNode);
            }

        }

        private TreeNode GetMacroNode(string macroarea)
        {
            TreeNode macroNode;

            if (!uxTreeView.Nodes.ContainsKey(macroarea))
            {
                macroNode = new TreeNode();
                macroNode.Text = macroarea;
                macroNode.Name = macroarea;
                uxTreeView.Nodes.Add(macroNode);
            }
            else
            {
                macroNode = uxTreeView.Nodes[macroarea];
            }

            return macroNode;
        }

        private void LoadPermissions(string macroarea, string area)
        {
            _loading = true;
            this.uxPermissionsListBox.Items.Clear();
            foreach (KeyValuePair<string, Secure> secure in SecurityManager.Instance.SecureMethods)
            {                
                if(secure.Value.MacroArea == macroarea)
                    if (secure.Value.Area == area)
                    {
                        secure.Value.FullToString = _showFullMethodNames;
                        this.uxPermissionsListBox.Items.Add(secure.Value, ProfileHasMethod(secure.Value.FullName));
                    }
            }
            _loading = false;
        }

        private bool ProfileHasMethod(string fullName)
        {
            foreach (IPermission method in _permissions)
            {
                if (method.FullMethodName.Equals(fullName))
                    return true;
            }

            return false;
        }

        private bool ProfileContainsMethod(Secure secure)
        {
            bool contains = false;

            foreach (IPermission permission in _profile.Permissions)
            {
                if (permission.FullMethodName.Equals(secure.FullName))
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        private void DeletePermission(string fullName)
        {
            IPermission toRemove = null;
            
            foreach (IPermission permission in _permissions)
            {
                if (permission.FullMethodName.Equals(fullName))
                {
                    toRemove = permission;
                    break;
                }
            }

            if(toRemove != null)
                _permissions.Remove(toRemove);
        }

        private void uxOKButton_Click(object sender, EventArgs e)
        {
            if (uxDescriptionTextBox.Text == "")
            {
                uxDescriptionTextBox.Focus();
                return;
            }

            if (_permissions.Count == 0)
            {
                MessageBox.Show("Selezionare almeno un metodo");
                return;
            }

            if (_profile == null)
            {
                _profile = new Profile();
                foreach (IPermission permission in _permissions)
                {
                    permission.Profile = _profile;
                }
            }

            _profile.Description = uxDescriptionTextBox.Text;
            _profile.Permissions.Clear();

            foreach (IPermission permission in _permissions)
            {
                _profile.Permissions.Add(permission);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void uxTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null && e.Node.Tag.ToString() == "area")
            {
                TreeNode macroNode = e.Node.Parent;
                uxPermissionsListBox.Enabled = true;
                LoadPermissions(macroNode.Name, e.Node.Name);
            }
            else
            {
                uxPermissionsListBox.Items.Clear();
                uxPermissionsListBox.Enabled = false;
            }
        }

        private void uxPermissionsListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_loading)
                return;
            
            if (e.NewValue == CheckState.Checked)
            {
                Secure secure = (Secure)uxPermissionsListBox.Items[e.Index];
                Permission permission = new Permission();
                permission.FullMethodName = secure.FullName;
                permission.Profile = _profile;
                permission.Macroarea = secure.MacroArea;
                permission.Alias = secure.Alias;
                _permissions.Add(permission);
            }
            else
            {
                Secure secure = (Secure)uxPermissionsListBox.Items[e.Index];
                DeletePermission(secure.FullName);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, 0, panel1.Width, 0);
        }

        private void uxProfileForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift)
                if (e.Control)
                    if (e.Alt)
                        if (e.KeyData == Keys.M)
                            _showFullMethodNames = !_showFullMethodNames;
        }
    }
}