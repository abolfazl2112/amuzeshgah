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
    public partial class grooh : Form
    {
        string sqlAllReshte = "SELECT code[کد استاندارد], name[نام رشته], onvan[عنوان استاندارد],modat[مدت دوره], dateShoroo[تاریخ شروع دوره],date[تاریخ آزمون]   FROM reshte";
        string sqlAllPerson = "";
        bool flag = false;
        
        public grooh()
        {
            InitializeComponent();
        }

        private void grooh_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
            dataGridViewX1.DataSource = DataManagement.Search(sqlAllReshte);
            setData();
            flag = true;
        }

        private void setData()
        {
            clearData();
            
            if (dataGridViewX1.RowCount != 0)
            {
                sqlAllPerson = "SELECT person.codeMeli[کدملی], person.family + ' ' + person.name[نام و نام خانوادگی], [P&R].nomreA[نمره عملی], [P&R].nomreN[نمره نظری], [P&R].dateSodoor[تاریخ صدور], [P&R].shGovahi[شماره گواهینامه] " +
                               "FROM [P&R] INNER JOIN person ON [P&R].PID = person.codeMeli WHERE([P&R].Rname = N'" + dataGridViewX1.CurrentRow.Cells[1].Value.ToString() + "') AND ([P&R].Ronvan = N'" + dataGridViewX1.CurrentRow.Cells[2].Value.ToString() + "') ORDER BY [نام و نام خانوادگی]";
                dataGridViewX2.DataSource = DataManagement.Search(sqlAllPerson);

                txtCode.Text = dataGridViewX1.CurrentRow.Cells[0].Value.ToString();
                txtNameMaharat.Text = dataGridViewX1.CurrentRow.Cells[1].Value.ToString();
                txtOnvan.Text = dataGridViewX1.CurrentRow.Cells[2].Value.ToString();
                txtModat.Value = int.Parse(dataGridViewX1.CurrentRow.Cells[3].Value.ToString());
                txtShoroo.Text = dataGridViewX1.CurrentRow.Cells[4].Value.ToString();
                txtAzmoon.Text = dataGridViewX1.CurrentRow.Cells[5].Value.ToString();

                if (dataGridViewX2.RowCount != 0)
                {
                    txtNomreA.Text = dataGridViewX2.CurrentRow.Cells[2].Value.ToString();
                    txtNomreN.Text = dataGridViewX2.CurrentRow.Cells[3].Value.ToString();
                    txtSodoor.Text = dataGridViewX2.CurrentRow.Cells[4].Value.ToString();
                    txtGovahi.Text = dataGridViewX2.CurrentRow.Cells[5].Value.ToString();
                }
            }
        }

        private void clearData()
        {
            txtCode.Text = txtAzmoon.Text = txtGovahi.Text = txtNameMaharat.Text = txtNomreA.Text = txtNomreN.Text = txtOnvan.Text = txtShoroo.Text = txtSodoor.Text
                = "";
            txtModat.Value = 0;
            //dataGridViewX1.DataSource = null;
            dataGridViewX2.DataSource = null;
        }

        private void txtGovahi_Enter(object sender, EventArgs e)
        {
            //if (dataGridViewX2.RowCount == 0) return;
            //txtGovahi.Text = "9/" + txtSodoor.Text.Substring(0,5) + "/" + dataGridViewX2.RowCount.ToString() + "/" + (dataGridViewX2.CurrentRow.Index + 1).ToString();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            txtSodoor.Text = Data.Pdate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!flag) return;
            this.UseWaitCursor = true;
            clearData();

            if (dataGridViewX1.RowCount != 0)
            {
                sqlAllPerson = "SELECT  person.codeMeli AS کدملی, person.family + ' ' + person.name AS [نام و نام خانوادگی], [P&R].nomreA AS [نمره عملی], [P&R].nomreN AS [نمره نظری], "+
                               " [P&R].dateSodoor AS [تاریخ صدور], [P&R].shGovahi AS [شماره گواهینامه] FROM [P&R] INNER JOIN person ON [P&R].PID = person.codeMeli WHERE ([P&R].Rname = N'"+dataGridViewX1.Rows[e.RowIndex].Cells[1].Value.ToString()+
                               "') AND ([P&R].Ronvan = N'" + dataGridViewX1.Rows[e.RowIndex].Cells[2].Value.ToString() + "') ORDER BY [نام و نام خانوادگی]";
                dataGridViewX2.DataSource = DataManagement.Search(sqlAllPerson);

                txtCode.Text = dataGridViewX1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNameMaharat.Text = dataGridViewX1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtOnvan.Text = dataGridViewX1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtModat.Value = int.Parse(dataGridViewX1.Rows[e.RowIndex].Cells[3].Value.ToString());
                txtShoroo.Text = dataGridViewX1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtAzmoon.Text = dataGridViewX1.Rows[e.RowIndex].Cells[5].Value.ToString();

                if (dataGridViewX2.RowCount != 0)
                {
                    txtNomreA.Text = dataGridViewX2.Rows[0].Cells[2].Value.ToString();
                    txtNomreN.Text = dataGridViewX2.Rows[0].Cells[3].Value.ToString();
                    txtSodoor.Text = dataGridViewX2.Rows[0].Cells[4].Value.ToString();
                    txtGovahi.Text = dataGridViewX2.Rows[0].Cells[5].Value.ToString();
                }
            }
            btnSabt.Enabled = false;
            this.UseWaitCursor = false;

        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            if (dataGridViewX2.RowCount == 0)
            {
                btnSabt.Enabled = false; return;
            }
            this.UseWaitCursor = true;
            string sqlUpdate = "UPDATE [P&R] SET nomreA = N'" + txtNomreA.Text + "', nomreN = N'" + txtNomreN.Text + "', dateSodoor = N'" + txtSodoor.Text + "', shGovahi = N'" + txtGovahi.Text + "' WHERE (Rname = N'" + txtNameMaharat.Text +
                "') AND (PID = N'" + dataGridViewX2.CurrentRow.Cells[0].Value.ToString() + "') AND (Ronvan = N'" + txtOnvan.Text + "')";
            DataManagement.I_U_D(sqlAllPerson, sqlUpdate);
            dataGridViewX2.DataSource = DataManagement.Search(sqlAllPerson);
            FMessegeBox.FarsiMessegeBox.Show("اطلاعات با موفقیت ثبت شد");
            this.UseWaitCursor = false;
        }

        private void dataGridViewX2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX2.RowCount != 0)
            {
                this.UseWaitCursor = true;

                txtNomreA.Text = dataGridViewX2.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtNomreN.Text = dataGridViewX2.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSodoor.Text = dataGridViewX2.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtGovahi.Text = dataGridViewX2.Rows[e.RowIndex].Cells[5].Value.ToString();
                btnSabt.Enabled = false;
                this.UseWaitCursor = false;
            }
        }

        private void txtNomreA_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnSabt.Enabled = true;
        }

        private void txtGovahi_TextChanged(object sender, EventArgs e)
        {
            btnSabt.Enabled = true;
        }

        private void txtGovahi_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtSodoor_Enter(object sender, EventArgs e)
        {
            
        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            string sql = "",sqlAllPer = "SELECT  person.codeMeli AS کدملی, person.family + ' ' + person.name AS [نام و نام خانوادگی], [P&R].nomreA AS [نمره عملی], [P&R].nomreN AS [نمره نظری], " +
                               " [P&R].dateSodoor AS [تاریخ صدور], [P&R].shGovahi AS [شماره گواهینامه] FROM [P&R] INNER JOIN person ON [P&R].PID = person.codeMeli WHERE ([P&R].Rname = N'" + dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[1].Value.ToString() +
                               "') AND ([P&R].Ronvan = N'" + dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[2].Value.ToString() + "') "; 
            
            if (textBoxX1.Text == "" && textBoxX2.Text == "")
                dataGridViewX2.DataSource = DataManagement.Search(sqlAllPer + "ORDER BY [نام و نام خانوادگی]");
            else
            {
                sql = sqlAllPer + " AND (codeMeli LIKE N'" + textBoxX1.Text + "%') AND (person.family + ' ' +  person.name LIKE N'" + textBoxX2.Text + "%') ";
                dataGridViewX2.DataSource = DataManagement.Search(sql + " ORDER BY [نام و نام خانوادگی]");
            }
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            buttonX1.Visible = true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string sqlUtmp = "UPDATE [P&R] SET Rname = N'" + txtNameMaharat.Text + "' WHERE (Rname = N'" + dataGridViewX1.CurrentRow.Cells[1].Value.ToString() + "')";
            DataManagement.I_U_D(sqlAllReshte, sqlUtmp);

            string sqlIU = "UPDATE reshte SET code = N'" + txtCode.Text + "', name = N'" + txtNameMaharat.Text + "', onvan = N'" + txtOnvan.Text + "',modat = N'" +
            txtModat.Value.ToString() + "', dateShoroo = N'" + txtShoroo.Text + "',date = N'" + txtAzmoon.Text + "' where (code = N'" + dataGridViewX1.CurrentRow.Cells[0].Value.ToString() + "')";
            DataManagement.I_U_D(sqlAllReshte, sqlIU);
        }
    }
}
