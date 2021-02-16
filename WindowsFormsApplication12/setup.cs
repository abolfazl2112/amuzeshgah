using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace amoozeshgah
{
    public partial class setup : Form
    {
        public setup()
        {
            InitializeComponent();
        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            DataManagement.I_U_D("select * from setup", "UPDATE setup SET usernamesms = N'" + txtUser.Text + "', passwordsms = N'" + txtPass.Text + "'");
            btnSabt.Enabled = false;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            btnSabt.Enabled = true;
        }

        private void txtNazari_TextChanged(object sender, EventArgs e)
        {
            buttonX1.Enabled = true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataManagement.I_U_D("select * from setup", "UPDATE setup SET nazari = N'" + txtNazari.Text + "', amali = N'" + txtAmali.Text + "'");
            btnSabt.Enabled = false;
        }

        private void setup_Load(object sender, EventArgs e)
        {
            DataTable dt = DataManagement.Search("select nazari, amali, usernamesms, passwordsms from setup");
            if (dt.Rows.Count > 0)
            {
                txtNazari.Text = dt.Rows[0][0].ToString();
                txtAmali.Text = dt.Rows[0][1].ToString();
                txtUser.Text = dt.Rows[0][2].ToString();
                txtPass.Text = dt.Rows[0][3].ToString();
                buttonX1.Enabled = btnSabt.Enabled = false;
            }
        }
    }
}
