using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WIN.SECURITY.GUI
{
    public partial class GestoreProfili : Form
    {
        public GestoreProfili()
        {
            InitializeComponent();
        }

        private void GestoreProfili_Load(object sender, EventArgs e)
        {
            uxProfileManagementControl1.InitializeProfiles();
        }
    }
}