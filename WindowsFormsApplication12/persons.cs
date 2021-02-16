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
    public partial class persons : Form
    {
        string sqlAll = "SELECT codeMeli[کد ملی], name[نام], family[نام خانوادگی], father[نام پدر], shsh[ش.شناسنامه], tarikht[تاریخ تولد], sadere[صادره], mtavalod[متولد], mobile[شماره موبایل], sal[سال], mah[ماه] FROM person";
        public persons()
        {
            InitializeComponent();
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Hide();
            NewPerson newp = new NewPerson();
            DetectForm.F = 1;
            newp.ShowDialog(this);
            Show();
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount == 0) return;
            NewPerson np = new NewPerson();
            DetectForm.F = 2;
            np.loadEdit(dataGridViewX1, dataGridViewX1.CurrentRow.Index);
            np.ShowDialog(this);
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
        }

        private void persons_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount == 0) return;
            if (FMessegeBox.FarsiMessegeBox.Show("آیا برای حذف مطمئن هستید؟", "اخطار", FMessegeBox.FMessegeBoxButtons.YesNo) != DialogResult.Yes) return;
            DataManagement.I_U_D("select * from reshte", "DELETE FROM person WHERE (codeMeli = N'" + dataGridViewX1.CurrentRow.Cells[0].Value.ToString() + "')");
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (textBoxX1.Text == "" && textBoxX2.Text == "" && textBoxX3.Text == "")
                dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
            else
            {
                sql = sqlAll + " WHERE (codeMeli LIKE N'" + textBoxX1.Text + "%') AND (name LIKE N'" + textBoxX2.Text + "%') AND (family LIKE N'" + textBoxX3.Text + "%')";
                dataGridViewX1.DataSource = DataManagement.Search(sql);
            }

        }
    }
}
