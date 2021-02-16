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
    public partial class Main : DevComponents.DotNetBar.Office2007Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroTileItem5_Click(object sender, EventArgs e)
        {
            reshte r = new reshte();
            r.ShowDialog(this);
        }

        private void metroTileItem7_Click(object sender, EventArgs e)
        {
            persons p = new persons();
            p.ShowDialog(this);
        }

        private void metroTileItem2_Click(object sender, EventArgs e)
        {
            if(FMessegeBox.FarsiMessegeBox.Show("برای خروج مطمئن هستید؟","توجه",
                FMessegeBox.FMessegeBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();

        }

        private void metroTileItem6_Click(object sender, EventArgs e)
        {
            grooh g = new grooh();
            g.ShowDialog(this);
        }

        private void metroTileItem8_Click(object sender, EventArgs e)
        {
            gozareshList gl = new gozareshList();
            gl.ShowDialog(this);
        }

        private void metroTileItem1_Click(object sender, EventArgs e)
        {
            new about().ShowDialog(this);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void metroTileItem4_Click(object sender, EventArgs e)
        {

        }

        private void metroTileItem9_Click(object sender, EventArgs e)
        {

        }

        private void metroTileItem3_Click(object sender, EventArgs e)
        {
            new sms().ShowDialog(this);
        }

        private void metroTileItem4_Click_1(object sender, EventArgs e)
        {
            new setup().ShowDialog(this);
        }

        private void metroTileItem9_Click_1(object sender, EventArgs e)
        {
            new importExel().ShowDialog(this);
        }

        private void metroTileItem11_Click(object sender, EventArgs e)
        {
            new frm_rptsabtenam().ShowDialog(this);
        }
    }
}
