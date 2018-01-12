using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


using WIN.SECURITY;
using WIN.SECURITY.Core;



namespace WIN.SECURITY.GUI
{
    public partial class uxLogonForm : Form
    {
        private LinearGradientBrush _logonBrush;
        private Font _drawFont;
        
        public uxLogonForm()
        {
            InitializeComponent();

            uxLogonPictureBox.Paint += new PaintEventHandler(uxLogonPictureBox_Paint);
            _logonBrush = new LinearGradientBrush(uxLogonPictureBox.DisplayRectangle, Color.White, SystemColors.ActiveCaption, 0f);
            _drawFont = new Font("Tahoma", 8.0f, FontStyle.Bold);

        }

        void uxLogonPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(_logonBrush, uxLogonPictureBox.DisplayRectangle);
            g.DrawImage(WIN.SECURITY.Properties.Resources.Key, 5, 3, 77, 77);
            g.DrawString("    WIRELESS v1.0", _drawFont, Brushes.White, uxLogonPictureBox.Width - 100, uxLogonPictureBox.Height - 16);
            g.DrawLine(Pens.Black, 0, uxLogonPictureBox.Height - 1, uxLogonPictureBox.Width, uxLogonPictureBox.Height - 1);
        }

        private void uxLogonButton_Click(object sender, EventArgs e)
        {
            if (SecurityManager.Instance.Logon(uxUserTextBox.Text, uxPassTextBox.Text))
            {
                //aggiungo tutte le company all'utente admin
                User user = (User)SecurityManager.Instance.CurrentUser;
                
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
                uxErrorLabel.Text = SecurityManager.Instance.LastError;
                uxErrorLabel.Visible = true;
            }
        }

        private void uxUserTextBox_Enter(object sender, EventArgs e)
        {
            uxErrorLabel.Visible = false;
            uxUserTextBox.SelectAll();
        }

        private void uxPassTextBox_Enter(object sender, EventArgs e)
        {
            uxErrorLabel.Visible = false;
            uxPassTextBox.SelectAll();
        }

       
    }
}