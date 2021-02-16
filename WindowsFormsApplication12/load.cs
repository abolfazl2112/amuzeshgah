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
    public partial class load : DevComponents.DotNetBar.Office2007Form
    {
        public load()
        {
            InitializeComponent();
        }

        private void load_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
            timer1.Start();
        }

        bool flag = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval >= 50)
            {
                DataManagement.Search("select name from reshte");
                this.Hide();
                flag = true;
            }

            if (flag)
            {
                new Main().Show();
                timer1.Stop();
            }
            timer1.Interval++;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
