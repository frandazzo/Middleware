using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WIN.SECURITY.Core;



namespace WIN.SECURITY.GUI
{
    public partial class uxUserForm : Form
    {
        //IPersistenceService _persistence = PersistenceServiceFactory.Create();
        Role _role;
        User _user;

        public uxUserForm(Role role)
        {
            InitializeComponent();

            _role = role;
            uxInfoLabel.Text = string.Format(uxInfoLabel.Text, role.Description);

            
        }

        public uxUserForm(User user)
        {
            InitializeComponent();

            _user = user;

            uxUsernameTextBox.Text = user.Username;
            uxPasswordTextBox.Text = user.Password;
            uxNameTextBox.Text = user.Name;
            uxSurnameTextBox.Text = user.Surname;
           
            uxMailTextBox.Text = _user.Mail;
           


            this.Text += user.Username;
            uxInfoLabel.Text = string.Format(uxInfoLabel.Text, user.Role.Description);


           
        }

        

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(uxUsernameTextBox.Text == "") {
                uxUsernameTextBox.Focus();
                return;
            }
            
            if(uxPasswordTextBox.Text == "") {
                uxPasswordTextBox.Focus();
                return;
            }

            if (_user == null)
                _user = new User(_role);

            _user.Username = uxUsernameTextBox.Text;
            _user.Password = uxPasswordTextBox.Text;
            _user.Name = uxNameTextBox.Text;
            _user.Surname = uxSurnameTextBox.Text;
            _user.Mail = uxMailTextBox.Text;
           

           

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NewUserForm_Load(object sender, EventArgs e)
        {
            uxUsernameTextBox.Focus();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, 0, panel1.Width, 0);
        }

       

    }
}