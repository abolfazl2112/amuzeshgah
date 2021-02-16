using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;

namespace amoozeshgah
{
    public partial class importExel : Form
    {

        public importExel()
        {
            InitializeComponent();
        }

        private void importExel_Load(object sender, EventArgs e)
        {

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (txtSheet.Text == "")
            {
                FMessegeBox.FarsiMessegeBox.Show("نام شیت فایل اکسل را وارد کنید");
                return;
            }
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "فایل اکسل(*.xls)|*.xls";
                open.Title = "انتخاب فایل اکسل";
                if (open.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string path = open.FileName;
                        System.Data.OleDb.OleDbConnection MyConnection;
                        System.Data.DataSet DtSet;
                        System.Data.OleDb.OleDbDataAdapter MyCommand;
                        MyConnection = new System.Data.OleDb.OleDbConnection(@"provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties=Excel 8.0;");
                        MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [" + txtSheet.Text + "$]", MyConnection);
                        MyCommand.TableMappings.Add("Table", "Names");
                        DtSet = new System.Data.DataSet();
                        MyCommand.Fill(DtSet);
                        dataGridViewX1.DataSource = DtSet.Tables[0];
                        MyConnection.Close();

                        for (int i = 0; i < dataGridViewX1.Columns.Count; i++)
                        {
                            txtExel.Items.Add(dataGridViewX1.Columns[i].HeaderText);
                        }

                        groupPanel1.Enabled = groupPanel2.Enabled = groupPanel3.Enabled = true;
                    }
                    catch(Exception ex)
                    {
                        FMessegeBox.FarsiMessegeBox.Show("مشکل در خواندن فایل اکسل\n" + ex.Message);
                        return;
                    }
                }
            }
            catch(Exception ex) 
            {
                FMessegeBox.FarsiMessegeBox.Show("مشکل در باز کردن فایل اکسل\n" + ex.Message);
            }
        }

        bool flag = false, flagName = false, flagFamily = false, flagfather = false, flagmobile = false, flagtarikht = false, flagShSh = false, flagsadere = false, flagmtavalod = false, flagsal = false, flagmah = false;
        string SelectedName = "";
        private void btnSabt_Click(object sender, EventArgs e)
        {
            if (txtExel.Text == "" || txtItemPaygah.Text == "") return;
            dataGridViewX1.Columns[txtExel.SelectedIndex].HeaderText = txtItemPaygah.Text;
            dataGridViewX1.Columns[txtExel.SelectedIndex].Name = SelectedName;
            
            if(txtItemPaygah.SelectedIndex == 0)
                flag = true;
            if (txtItemPaygah.SelectedIndex == 1)
                flagName = true;
            if (txtItemPaygah.SelectedIndex == 2)
                flagFamily = true;
            if (txtItemPaygah.SelectedIndex == 3)
                flagfather = true;
            if (txtItemPaygah.SelectedIndex == 4)
                flagShSh = true;
            if (txtItemPaygah.SelectedIndex == 5)
                flagmobile = true;
            if (txtItemPaygah.SelectedIndex == 6)
                flagtarikht = true;
            if (txtItemPaygah.SelectedIndex == 7)
                flagsadere = true;
            if (txtItemPaygah.SelectedIndex == 8)
                flagmtavalod = true;
            if (txtItemPaygah.SelectedIndex == 9)
                flagsal = true;
            if (txtItemPaygah.SelectedIndex == 10)
                flagmah = true;

   
        }

        private void txtItemPaygah_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (txtItemPaygah.SelectedIndex)
            {
                case 0:
                    SelectedName = "codeMeli";
                    break;
                case 1:
                    SelectedName = "name";
                    break;
                case 2:
                    SelectedName = "family";
                    break;
                case 3:
                    SelectedName = "father";
                    break;
                case 4:
                    SelectedName = "shsh";
                    break;
                case 5:
                    SelectedName = "mobile";
                    break;
                case 6:
                    SelectedName = "tarikht";
                    break;
                case 7:
                    SelectedName = "sadere";
                    break;
                case 8:
                    SelectedName = "mtavalod";
                    break;
                case 9:
                    SelectedName = "sal";
                    break;
                case 10:
                    SelectedName = "mah";
                    break;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!flag) return;

            string sql = "INSERT INTO person (codeMeli{0}{1}{2}{3}{4}{5}{6}{7}{8}{9})VALUES({10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20})";
           
            try
            {
                int count = 0;
                DataTable ans ;
                for (int i = 0; i < dataGridViewX1.RowCount; i++)
                {
                    string Sql = string.Format(sql, (flagName ? ",name" : ""), (flagFamily ? ",family" : ""), (flagfather ? ",father" : ""), (flagShSh ? ",shsh" : ""), (flagmobile ? ",mobile" : ""), (flagtarikht ? ",tarikht" : ""), (flagsadere ? ",sadere" : ""), (flagmtavalod ? ",mtavalod" : ""), (flagsal ? ",sal" : ""), (flagmah ? ",mah" : ""),
                        "N'" + dataGridViewX1.Rows[i].Cells["codeMeli"].Value.ToString() + "'", (flagName ? ",N'" + dataGridViewX1.Rows[i].Cells["name"].Value.ToString() + "'" : ""), (flagFamily ? ",N'" + dataGridViewX1.Rows[i].Cells["family"].Value.ToString() + "'" : ""), (flagfather ? ",N'" + dataGridViewX1.Rows[i].Cells["father"].Value.ToString() + "'" : ""),
                        (flagShSh ? ",N'" + dataGridViewX1.Rows[i].Cells["shsh"].Value.ToString() + "'" : ""), (flagmobile ? ",N'" + dataGridViewX1.Rows[i].Cells["mobile"].Value.ToString() + "'" : ""), (flagtarikht ? ",N'" + dataGridViewX1.Rows[i].Cells["tarikht"].Value.ToString() + "'" : ""), (flagsadere ? ",N'" + dataGridViewX1.Rows[i].Cells["sadere"].Value.ToString() + "'" : ""),
                        (flagmtavalod ? ",N'" + dataGridViewX1.Rows[i].Cells["mtavalod"].Value.ToString() + "'" : ""), (flagsal ? ",N'" + dataGridViewX1.Rows[i].Cells["sal"].Value.ToString() + "'" : ""),(flagmah ? ",N'" + dataGridViewX1.Rows[i].Cells["mah"].Value.ToString() + "'" : ""));

                    if (dataGridViewX1.Rows[i].Cells["codeMeli"].Value.ToString() != "")
                    {
                        ans = DataManagement.I_U_D("select codeMeli from person where codeMeli = N'" + dataGridViewX1.Rows[i].Cells["codeMeli"].Value.ToString() + "'", Sql);
                        if (ans.Rows.Count >= 0)
                            count++;
                    }
                    
                }
                if (count > 0)
                    lblAns.Text = "تعداد " + count.ToString() + " ثبت شد";
            }
            catch(Exception ex)
            {
                FMessegeBox.FarsiMessegeBox.Show("مشکل در ثبت اطلاعات\n" + ex.Message, "خطا", FMessegeBox.FMessegeBoxButtons.Ok, FMessegeBox.FMessegeBoxIcons.Error);
            }
        }
    }
}
