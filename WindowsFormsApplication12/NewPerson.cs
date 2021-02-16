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
    public partial class NewPerson : Form
    {
        string oldCode = "";
        public NewPerson()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTavalod_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            txtSadere.Focus();
        }

        private void NewPerson_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
            DataManagement.DT = DataManagement.Search("SELECT distinct name FROM reshte");
            for (int i = 0; i < DataManagement.DT.Rows.Count; i++)
                txtReshte.Items.Add(DataManagement.DT.Rows[i][0].ToString());
            Languge_Keybord.Persian();
            
            txtMeli.Focus();
        }

        public void loadEdit(DataGridView dgv, int index)
        {
            oldCode = txtMeli.Text = dgv.Rows[index].Cells[0].Value.ToString();
            txtName.Text = dgv.Rows[index].Cells[1].Value.ToString();
            txtFamily.Text = dgv.Rows[index].Cells[2].Value.ToString();
            txtFather.Text = dgv.Rows[index].Cells[3].Value.ToString();
            txtShsh.Text = dgv.Rows[index].Cells[4].Value.ToString();
            txtTavalod.Text = dgv.Rows[index].Cells[5].Value.ToString();
            txtSadere.Text = dgv.Rows[index].Cells[6].Value.ToString();
            txtMtavalod.Text = dgv.Rows[index].Cells[7].Value.ToString();
            txtmobile.Text = dgv.Rows[index].Cells[8].Value.ToString();
            txtsal.Text = dgv.Rows[index].Cells[9].Value.ToString();
            txtmah.Text = dgv.Rows[index].Cells[10].Value.ToString();

            DataManagement.DT = DataManagement.Search("select Rname,Ronvan from [P&R] where (PID = N'" + txtMeli.Text + "')");
            for (int i = 0; i < DataManagement.DT.Rows.Count; i++)
            {
                listReshte.Rows.Add(DataManagement.DT.Rows[i][0].ToString(), DataManagement.DT.Rows[i][1].ToString());
            }
        }

        private void txtMeli_KeyDown(object sender, KeyEventArgs e)
        {
            Control.Focus(txtName, e);
            Control.escape(this, e);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            Control.Focus(txtFamily, e);
            Control.escape(this, e);
        }

        private void txtFamily_KeyDown(object sender, KeyEventArgs e)
        {
            Control.Focus(txtFather, e);
            Control.escape(this, e);
        }

        private void txtFather_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtShsh.Focus();
            Control.escape(this, e);
        }

        private void txtShsh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtTavalod.Focus();
            Control.escape(this, e);
        }

        private void txtTavalod_KeyDown(object sender, KeyEventArgs e)
        {
            Control.Focus(txtSadere, e);
            Control.escape(this, e);
        }

        private void txtSadere_KeyDown(object sender, KeyEventArgs e)
        {
            Control.Focus(txtMtavalod, e);
            Control.escape(this, e);
        }

        private void txtReshte_KeyDown(object sender, KeyEventArgs e)
        {
            Control.Focus(txtMtavalod, e);
            Control.escape(this, e);
        }

        private void S_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            if(txtMeli.Text == "")
            {
                FMessegeBox.FarsiMessegeBox.Show("وارد کردن کد ملی اجباری است");
                this.UseWaitCursor = false;
                return;
            }
            string str = "select codeMeli from person where codeMeli = N'" + txtMeli.Text + "' ";

            if (txtMeli.Text != oldCode)
                if (DataManagement.Search(str).Rows.Count != 0)
                {
                    FMessegeBox.FarsiMessegeBox.Show("کد ملی تکراری است");
                    this.UseWaitCursor = false;
                    return;
                }

            if (!MeliFunction())
            {
                this.UseWaitCursor = false;
                return;
            }


            string sql = "";
            if (DetectForm.F == 1)
            {
                sql = "INSERT INTO person (codeMeli, name, family, father, shsh, tarikht, sadere, mtavalod,sal,mah) VALUES (N'" +
                    txtMeli.Text + "', N'" + txtName.Text + "', N'" + txtFamily.Text + "', N'" + txtFather.Text + "', N'" + txtShsh.Value.ToString() +
                    "', N'" + txtTavalod.Text + "', N'" + txtSadere.Text + "', N'" + txtMtavalod.Text + "', N'" + txtsal.Text + "', N'" + txtmah.Text + "')";
                DataManagement.I_U_D("select * from reshte", sql);
                if (listReshte.Rows.Count != 0)
                    for(int i = 0; i < listReshte.Rows.Count; i++)
                    {
                        DataManagement.DT = DataManagement.Search("SELECT code, name,onvan FROM reshte WHERE(name = N'" + listReshte.Rows[i].Cells[0].Value.ToString() + "')AND(onvan = N'" + listReshte.Rows[i].Cells[1].Value.ToString() + "')");
                        DataManagement.I_U_D("select * from reshte", "INSERT INTO [P&R] (PID, RID, Rname,Ronvan) VALUES (N'" + txtMeli.Text + "', N'" + DataManagement.DT.Rows[0][0].ToString() + "', N'" + listReshte.Rows[i].Cells[0].Value.ToString() + "', N'" + listReshte.Rows[i].Cells[1].Value.ToString() + "')");
                    }
            }
            else
            {
                sql = "UPDATE person SET codeMeli = N'" + txtMeli.Text + "', name = N'" + txtName.Text + "', family = N'" + txtFamily.Text + "', father = N'" + txtFather.Text + "', shsh = N'" + txtShsh.Value.ToString() + "', tarikht = N'" + txtTavalod.Text +
                    "', sadere = N'" + txtSadere.Text + "', mtavalod = N'" + txtMtavalod.Text + "', sal = N'" + txtsal.Text + "', mah = N'" + txtmah.Text + "' WHERE (codeMeli = N'" + oldCode + "')";
                DataManagement.I_U_D("select name from reshte", sql);
                if (listReshte.Rows.Count != 0 && isChange)
                {
                    DataManagement.I_U_D("select name from reshte", "DELETE FROM [P&R] WHERE (PID = N'" + oldCode + "')");
                    for (int i = 0; i < listReshte.Rows.Count; i++)
                    {
                        if (listReshte.Rows[i].Cells[0].ToString() == "") continue;
                        DataManagement.DT = DataManagement.Search("SELECT code from reshte WHERE(name = N'" + listReshte.Rows[i].Cells[0].Value.ToString() + "') AND (onvan = N'" + listReshte.Rows[i].Cells[1].Value.ToString() + "')");
                        DataManagement.I_U_D("select name from reshte", "INSERT INTO [P&R] (PID, RID, Rname,Ronvan) VALUES (N'" + txtMeli.Text + "', N'" + DataManagement.DT.Rows[0][0].ToString() + "', N'" + listReshte.Rows[i].Cells[0].Value.ToString() + "', N'" + listReshte.Rows[i].Cells[1].Value.ToString() + "')");
                    }
                }
            }

            this.UseWaitCursor = false;
            FMessegeBox.FarsiMessegeBox.Show("اطلاعات با موفقیت ثبت شد");
            this.Close();
        }

        private bool MeliFunction()
        {
            try
            {
                char[] chArray = this.txtMeli.Text.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                switch (this.txtMeli.Text)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":
                        MessageBox.Show("کد ملی وارد شده صحیح نمی باشد");
                        return false;
                }
                int num3 = ((((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4)) + (numArray[7] * 3)) + (numArray[8] * 2);
                int num4 = num3 - ((num3 / 11) * 11);
                if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                {
                    return true;
                }
                else
                {
                    FMessegeBox.FarsiMessegeBox.Show("کد ملی نامعتبر است");
                    return false;
                }
            }
            catch (Exception)
            {
                FMessegeBox.FarsiMessegeBox.Show("لطفا یک عدد 10 رقمی وارد کنید");
                return false;
            }
        }

        private void txtMtavalod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtReshte.Focus();
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtReshte.Text == "" || txtonvan.Text == "") return;
            int i = 0;
            for (; i<listReshte.RowCount; i++)
                if (listReshte.Rows[i].Cells[0].Value.ToString() == txtReshte.Text && listReshte.Rows[i].Cells[1].Value.ToString() == txtonvan.Text)
                    return;
            listReshte.Rows.Add();
            listReshte.Rows[i].Cells[0].Value = txtReshte.Text;
            listReshte.Rows[i].Cells[1].Value = txtonvan.Text;
            isChange = true;
        }

        bool isChange = false;
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listReshte.Rows.Count == 0) return;
            if (FMessegeBox.FarsiMessegeBox.Show("درصورت حذف،سایر اطلاعات مرتبط حذف خواهد شد،رشته حذف شود؟", "توجه", FMessegeBox.FMessegeBoxButtons.YesNo) == DialogResult.Yes)
            {
                listReshte.Rows.RemoveAt(listReshte.CurrentRow.Index);
                isChange = true;
            }

        }

        private void txtReshte_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataManagement.DT = DataManagement.Search("select onvan from reshte where (name = N'" + txtReshte.Text + "')");
            txtonvan.Items.Clear();
            for (int i = 0; i < DataManagement.DT.Rows.Count; i++)
            {
                string tmp = DataManagement.DT.Rows[i][0].ToString();
                txtonvan.Items.Add(tmp);
            }
        }
    }
}
