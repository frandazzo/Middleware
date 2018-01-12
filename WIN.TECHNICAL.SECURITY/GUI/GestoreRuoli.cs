using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WIN.SECURITY.GUI
{
    public partial class GestoreRuoli : Form
    {
        public GestoreRuoli()
        {
            InitializeComponent();
        }

        private void GestoreRuoli_Load(object sender, EventArgs e)
        {
            uxRoleManagementControl1.InitializeRoles();
        }
    }
}