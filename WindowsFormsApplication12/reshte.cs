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
    public partial class reshte : Form
    {
        string sqlAll = "SELECT code[کد], name[رشته],onvan[عنوان رشته], modat[مدت دوره], dateShoroo[تاریخ شروع دوره], date[تاریخ آزمون],vamali[واحد عملی],vnazari[واحد نظری] FROM reshte";
        public reshte()
        {
            InitializeComponent();
        }

        private void groupPanel1_EnabledChanged(object sender, EventArgs e)
        {
            groupPanel2.Enabled = groupPanel3.Enabled = !groupPanel1.Enabled;
        }

        private void reshte_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount == 0) return;
            if (FMessegeBox.FarsiMessegeBox.Show("آیا برای حذف مطمئن هستید؟", "توجه", FMessegeBox.FMessegeBoxButtons.YesNo) != DialogResult.Yes) return;
            string sqldel = "DELETE FROM reshte WHERE (code = N'" + dataGridViewX1.CurrentRow.Cells[0].Value.ToString() + "')";
            DataManagement.I_U_D(sqlAll, sqldel);
            sqldel = "DELETE FROM [P&R] WHERE (RID = N'" + dataGridViewX1.CurrentRow.Cells[0].Value.ToString() + "')";
            DataManagement.I_U_D(sqlAll, sqldel);
            
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            groupPanel1.Enabled = true;
            groupPanel2.Enabled = groupPanel3.Enabled = false;
            DetectForm.F = 1;
            txtCode.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.RowCount == 0) return;
            groupPanel1.Enabled = true;
            groupPanel2.Enabled = groupPanel3.Enabled = false;
            DetectForm.F = 2;

            DetectForm.Cod = txtCode.Text = dataGridViewX1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridViewX1.CurrentRow.Cells[1].Value.ToString();
            txtonvan.Text = dataGridViewX1.CurrentRow.Cells[2].Value.ToString();
            txtmodat.Value = int.Parse(dataGridViewX1.CurrentRow.Cells[3].Value.ToString());
            txtShoro.Text = dataGridViewX1.CurrentRow.Cells[4].Value.ToString();
            txtDate.Text = dataGridViewX1.CurrentRow.Cells[5].Value.ToString();
            txtamali.Text = dataGridViewX1.CurrentRow.Cells[6].Value.ToString();
            txtnazari.Text = dataGridViewX1.CurrentRow.Cells[7].Value.ToString();
            
            txtCode.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupPanel1.Enabled = !groupPanel1.Enabled;
            groupPanel2.Enabled = groupPanel3.Enabled = true;
            txtDate.Text = txtonvan.Text = txtName.Text = txtCode.Text = txtShoro.Text = "";
            txtmodat.Value = 0;
        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            string sqlIU = "", sqlUtmp = "";
            if (DetectForm.F == 1)
            {
                sqlIU = "INSERT INTO reshte (code, name, onvan ,modat, dateShoroo, date, vamali, vnazari) VALUES(N'" + txtCode.Text +
                    "', N'" + txtName.Text + "',N'" + txtonvan.Text + "', N'" + txtmodat.Value.ToString() + "', N'" + txtShoro.Text +
                    "',N'" + txtDate.Text + "',N'" + txtamali.Text + "',N'" + txtnazari.Text + "')";
            }
            else
            {
                sqlUtmp = "UPDATE [P&R] SET Rname = N'" + txtName.Text + "' WHERE (Rname = N'" + dataGridViewX1.CurrentRow.Cells[1].Value.ToString() + "')";
                DataManagement.I_U_D(sqlAll, sqlUtmp);

                sqlIU = "UPDATE reshte SET code = N'" + txtCode.Text + "', name = N'" + txtName.Text + "', onvan = N'" + txtonvan.Text + "',modat = N'" +
                    txtmodat.Value.ToString() + "', dateShoroo = N'" + txtShoro.Text + "',date = N'" + txtDate.Text + "',vamali = N'" + txtamali.Text + "',vnazari = N'" + txtnazari.Text + "' where (code = N'" + DetectForm.Cod + "')";
            }
            DataManagement.I_U_D(sqlAll, sqlIU);
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
            btnCancel_Click(null, null);
        }
    }
}
