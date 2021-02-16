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
    public partial class gozareshList : Form
    {
        string sqlAll = "SELECT person.codeMeli[کدملی], person.family + ' ' +  person.name[نام و نام خانوادگی], person.father[نام پدر], person.shsh[ش.شناسنامه], person.tarikht[سال تولد], person.sadere[صادره], person.mtavalod[محل تولد], [P&R].nomreA[نمره عملی], [P&R].nomreN[نمره نظری], " +
                                                              " [P&R].dateSodoor[تاریخ صدور], [P&R].shGovahi[ش.گواهینامه],[P&R].Ronvan[عنوان رشته],[P&R].Rname[نام رشته] FROM [P&R] INNER JOIN person ON [P&R].PID = person.codeMeli ";
        public gozareshList()
        {
            InitializeComponent();
        }

        private void gozareshList_Load(object sender, EventArgs e)
        {
            txtNameMarkaz.Text = "آموزشگاه آزاد هنری معرق روشن";
            comboreshte1.Items.Clear();
            comboonvan1.Items.Clear();
            Languge_Keybord.Persian();
            setItemscomboBox();
        }

        void setItemscomboBox()
        {
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
            DataManagement.DT = DataManagement.Search("select distinct name from reshte");
            for (int i = 0; i < DataManagement.DT.Rows.Count; i++)
            {
                comboreshte1.Items.Add(DataManagement.DT.Rows[i][0]);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataManagement.DT = DataManagement.Search("select distinct onvan from reshte where(name = N'" + comboreshte1.Text + "')");
            comboonvan1.Items.Clear();
            for (int i = 0;i < DataManagement.DT.Rows.Count ; i++)
            {
                comboonvan1.Items.Add(DataManagement.DT.Rows[i][0].ToString());
            }
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll +  " WHERE ([P&R].Rname = N'" + comboreshte1.SelectedItem.ToString() + "') ORDER BY [نام و نام خانوادگی]");
            comboonvan1.Text = combosal1.Text = combomah1.Text = "";
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
                printPerson();
        }

        private void printPerson()
        {
            if (comboreshte1.Text == "" || dataGridViewX1.RowCount == 0||comboonvan1.Text=="") return;
            
            string sqlperson = "SELECT person.family + ' ' +  person.name[name], person.father, person.tarikht[tavalod], " +
                " person.codeMeli[meli], person.shsh, person.sadere, [P&R].nomreN[nazari],[P&R].nomreA[amali], [P&R].shGovahi[govahiname] " +
                " FROM person INNER JOIN [P&R] ON person.codeMeli = [P&R].PID WHERE ([P&R].Rname = N'" + comboreshte1.Text + "'){0}{1}{2} ORDER BY name";

            string.Format(sqlperson, (comboonvan1.Text == "" ? "" : "AND([P&R].Ronvan = N'" + comboreshte1.Text + "')"), (combosal1.Text == "" ? "" : "AND(person.sal = N'" + combosal1.Text + "')")
                , (combomah1.Text == "" ? "" : "AND(person.mah = N'" + combomah1.Text + "')"));

            string sqlVariable = "SELECT reshte.code[codeEstandard], reshte.name[nameReshte], reshte.date[DateAzmoon], reshte.onvan[onvanReshte], reshte.dateShoroo[DateShoroo], [P&R].dateSodoor[DateSodoor] " +
                                 "FROM reshte INNER JOIN [P&R] ON reshte.code = [P&R].RID WHERE (reshte.name = N'" + comboreshte1.Text + "')AND(reshte.onvan = N'" + comboonvan1.Text + "')";

            string str = Application.StartupPath + "\\rep\\perrep.mrt";
            DataManagement.DT = DataManagement.Search(sqlperson);
            try
            {
                for(int i=0;i<DataManagement.DT.Rows.Count;i++)
                {
                    bool x = false;
                    int j = 0;
                    for (; j < dataGridViewX1.SelectedRows.Count; j++)
                        if (dataGridViewX1.SelectedRows[j].Cells[0].Value.ToString()
                            == DataManagement.DT.Rows[i][0].ToString())
                        {
                            x = true;
                            break;
                        }
                    if (x) continue;
                    DataManagement.DT.Rows.RemoveAt(i);
                    i--;
                }
                Stimulsoft.Report.StiReport stikol = new Stimulsoft.Report.StiReport();
                stikol.Load(str);
                stikol.RegData("person", DataManagement.DT);
                stikol.Dictionary.DataSources.Items[0].DataTable = DataManagement.DT;

                DataManagement.DT = DataManagement.Search(sqlVariable);
                DataManagement.DT.Columns.Add("number");
                DataManagement.DT.Columns.Add("nameMarkaz");
                DataManagement.DT.Columns.Add("nahiye");
                DataManagement.DT.Columns.Add("sal");
                DataManagement.DT.Columns.Add("nobat");
                DataManagement.DT.Rows[0]["number"] = dataGridViewX1.RowCount.ToString();
                DataManagement.DT.Rows[0]["nameMarkaz"] = txtNameMarkaz.Text;
                DataManagement.DT.Rows[0]["nahiye"] = txtNahiye.Text;
                DataManagement.DT.Rows[0]["sal"] = txtSal.Text;
                DataManagement.DT.Rows[0]["nobat"] = txtNobat.Text;

                stikol.Load(str);
                stikol.RegData("variable", DataManagement.DT);
                stikol.Dictionary.DataSources.Items[0].DataTable = DataManagement.DT;
                stikol.Show();
            }
            catch
            {
                FMessegeBox.FarsiMessegeBox.Show("مشکل در چاپ اطلاعات","اخطار");
            }
            
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 1) return;
            if (dataGridViewX1.CurrentRow.Cells[0].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[1].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[2].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[3].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[4].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[5].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[6].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[7].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[8].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[9].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[10].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[11].Value.ToString() == "" ||
                dataGridViewX1.CurrentRow.Cells[12].Value.ToString() == "" ||
                txtAzmoon.Text == "" || txtOnvan.Text == "" || txtModat.Text == "")
            {
                FMessegeBox.FarsiMessegeBox.Show("اطلاعات کارآموز برای چاپ کامل نیست");
            }
            string type = "one";
            printGovahi(type);
        }

        private void printGovahi(string type)
        {
            string CodeMeli = "", NameReshte = "";
            string sqlGovahi = "";
            string str = Application.StartupPath + "\\rep\\govarpt.mrt";
            Stimulsoft.Report.StiReport stikol = new Stimulsoft.Report.StiReport();
            try
            {
                stikol.Load(str);
            }
            catch(Exception ex)
            {
                FMessegeBox.FarsiMessegeBox.Show(ex.Message);
            }
            if (type == "one")
            {
                CodeMeli = dataGridViewX1.CurrentRow.Cells[0].Value.ToString();
                NameReshte = comboreshte1.Text;

                sqlGovahi = "SELECT person.codeMeli AS meli, person.name + ' ' + person.family AS name, person.father, person.shsh, person.tarikht AS tavalod, person.sadere, [P&R].nomreA, " +
                            "[P&R].nomreN, [P&R].dateSodoor AS sodoor, [P&R].shGovahi AS govahi, person.mtavalod AS shahr, [P&R].RID AS codeStandard FROM person INNER JOIN [P&R] ON person.codeMeli = [P&R].PID " +
                            "WHERE (person.codeMeli = N'" + CodeMeli + "') AND ([P&R].Rname = N'" + dataGridViewX1.CurrentRow.Cells[12].Value.ToString() +
                               "')AND([P&R].Ronvan = N'" + dataGridViewX1.CurrentRow.Cells[11].Value.ToString() + "')";

                //sqlGovahi = "SELECT person.codeMeli[meli], person.name + ' ' + person.family[name], person.father, person.shsh, person.tarikht[tavalod], person.sadere, [P&R].nomreA, " +
                //               "[P&R].nomreN, [P&R].dateSodoor[sodoor], [P&R].shGovahi[govahi], reshte.onvan, reshte.date[azmon], reshte.modat, reshte.code[codeStandard], person.mtavalod[shahr] " +
                //               "FROM person INNER JOIN [P&R] ON person.codeMeli = [P&R].PID INNER JOIN reshte ON [P&R].RID = reshte.code " +
                //               "WHERE (person.codeMeli = N'" + CodeMeli + "') AND ([P&R].Rname = N'" + comboBoxEx1.SelectedItem.ToString() +
                //               "')AND([P&R].Ronvan = N'" + comboBoxEx2.Text + "')";

                DataManagement.DT = DataManagement.Search(sqlGovahi);
                try
                {

                    stikol.RegData("variable", DataManagement.DT);

                    DataManagement.DT.Columns.Add("mahal");
                    DataManagement.DT.Columns.Add("onvan");
                    DataManagement.DT.Columns.Add("azmon");
                    DataManagement.DT.Columns.Add("modat");
                    DataManagement.DT.Rows[0]["mahal"] = txtNameMarkaz.Text;
                    DataManagement.DT.Rows[0]["onvan"] = txtOnvan.Text;
                    DataManagement.DT.Rows[0]["azmon"] = txtAzmoon.Text;
                    DataManagement.DT.Rows[0]["modat"] = txtModat.Text;

                    stikol.Dictionary.DataSources.Items[0].DataTable = DataManagement.DT;
                    stikol.Show(this);
                }
                catch(Exception ex)
                {
                    FMessegeBox.FarsiMessegeBox.Show(ex.Message, "اخطار");
                }
            }/*
            else
            {
                NameReshte = comboBoxEx1.Text;

                for (int i = 0; i< dataGridViewX1.RowCount; i++)
                {
                    CodeMeli = dataGridViewX1.Rows[i].Cells[0].Value.ToString();
                    sqlGovahi = "SELECT person.codeMeli[meli], person.name + ' ' + person.family[name], person.father, person.shsh, person.tarikht[tavalod], person.sadere, [P&R].nomreA, " +
                                   "[P&R].nomreN, [P&R].dateSodoor[sodoor], [P&R].shGovahi[govahi], reshte.onvan, reshte.date[azmon], reshte.modat, reshte.code[codeStandard], person.mtavalod[shahr] " +
                                   "FROM person INNER JOIN [P&R] ON person.codeMeli = [P&R].PID INNER JOIN reshte ON [P&R].RID = reshte.code " +
                                   "WHERE (person.codeMeli = N'" + CodeMeli + "') AND (reshte.name = N'" + NameReshte + "')";

                    DataManagement.DT = DataManagement.Search(sqlGovahi);
                    try
                    {

                        DataManagement.DT.Columns.Add("mahal");
                        DataManagement.DT.Columns.Add("onvan");
                        DataManagement.DT.Columns.Add("azmon");
                        DataManagement.DT.Columns.Add("modat");
                        DataManagement.DT.Rows[0]["mahal"] = txtNameMarkaz.Text;
                        DataManagement.DT.Rows[0]["onvan"] = txtOnvan.Text;
                        DataManagement.DT.Rows[0]["azmon"] = txtAzmoon.Text;
                        DataManagement.DT.Rows[0]["modat"] = txtModat.Text;

                        stikol.RegData("variable", DataManagement.DT);
                        stikol.Dictionary.DataSources.Items[0].DataTable = DataManagement.DT;
                        
                        stikol.Print();
                    }
                    catch(Exception ex)
                    {
                        FMessegeBox.FarsiMessegeBox.Show(ex.Message, "اخطار");
                        break;
                    }
                }
            }*/
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            string type = "all";
            printGovahi(type);
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboreshte1.Text == "") { comboonvan1.Text = ""; comboreshte1.Focus(); }
            DataManagement.DT = DataManagement.Search("SELECT code, name, modat, date, onvan, dateShoroo FROM reshte where (name = N'" + comboreshte1.SelectedItem.ToString() + "')AND(onvan = N'"+comboonvan1.Text+"')");
            if (DataManagement.DT.Rows.Count == 0) return;
            txtCode.Text = DataManagement.DT.Rows[0]["code"].ToString();
            txtAzmoon.Text = DataManagement.DT.Rows[0]["date"].ToString();
            txtNameMaharat.Text = DataManagement.DT.Rows[0]["name"].ToString();
            txtModat.Text = DataManagement.DT.Rows[0]["modat"].ToString();
            txtNameMarkaz.Text = "آموزشگاه آزاد هنری معرق روشن";
            txtNum.Text = DataManagement.DT.Rows.Count.ToString();
            txtOnvan.Text = DataManagement.DT.Rows[0]["onvan"].ToString();
            txtshoroo.Text = DataManagement.DT.Rows[0]["dateShoroo"].ToString();
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll + " WHERE ([P&R].Rname = N'" + comboreshte1.SelectedItem.ToString() + "')AND([P&R].Ronvan = N'" + comboonvan1.Text + "') ORDER BY [نام و نام خانوادگی]");
            combosal1.Text = combomah1.Text = "";
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            string sql = "";
            if (textBoxX1.Text == "" && textBoxX2.Text == "")
                dataGridViewX1.DataSource = DataManagement.Search(sqlAll);
            else
            {
                sql = sqlAll + " WHERE (codeMeli LIKE N'" + textBoxX1.Text + "%') AND (person.family + ' ' +  person.name LIKE N'" + textBoxX2.Text + "%') ";
                dataGridViewX1.DataSource = DataManagement.Search(sql);
            }
        }

        private void buttonX5_Click_1(object sender, EventArgs e)
        {
            new reshte().ShowDialog(this);
            comboreshte1.Text = comboonvan1.Text = textBoxX1.Text = textBoxX2.Text = "";
            gozareshList_Load(null, null);
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            new persons().ShowDialog(this);
            comboreshte1.Text = comboonvan1.Text = textBoxX1.Text = textBoxX2.Text = "";
            gozareshList_Load(null, null);
        }

        private void dataGridViewX1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string sqls = "SELECT modat, date, onvan, name FROM reshte WHERE (onvan = N'" + dataGridViewX1.Rows[e.RowIndex].Cells[11].Value.ToString() + "') AND (name = N'" +
                    dataGridViewX1.Rows[e.RowIndex].Cells[12].Value.ToString() + "')";

                DataTable dts = DataManagement.Search(sqls);
                if (dts.Rows.Count > 0)
                {
                    txtAzmoon.Text = dts.Rows[0]["date"].ToString();
                    txtModat.Text = dts.Rows[0]["modat"].ToString();
                    txtOnvan.Text = dts.Rows[0]["onvan"].ToString();
                }
            }
            catch (Exception eX)
            {
                FMessegeBox.FarsiMessegeBox.Show("عنوان رشته یا نام رشته برای این شخص ثبت نشده است\nبعد از بروزرسانی اطلاعات مجددا سعی کنید");
            }
        }

        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            new grooh().ShowDialog(this);
            comboreshte1.Text = comboonvan1.Text = textBoxX1.Text = textBoxX2.Text = "";
            gozareshList_Load(null, null);
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            new frm_rptsabtenam().ShowDialog(this);
        }

        private void comboBoxEx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboonvan1.Text == "") { combosal1.Text = ""; comboonvan1.Focus(); }
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll + " WHERE ([P&R].Rname = N'" + comboreshte1.SelectedItem.ToString() + "')AND([P&R].Ronvan = N'" + comboonvan1.Text + "')AND(person.sal=N'"+combosal1.Text+"') ORDER BY [نام و نام خانوادگی]");
            combomah1.Text = "";
        }

        private void comboBoxEx4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combosal1.Text == "") { combomah1.Text = ""; combosal1.Focus(); }
            dataGridViewX1.DataSource = DataManagement.Search(sqlAll + " WHERE ([P&R].Rname = N'" + comboreshte1.SelectedItem.ToString() + "')AND([P&R].Ronvan = N'" + comboonvan1.Text + "')AND(person.sal=N'" + combosal1.Text + "')AND(person.mah=N'" + combomah1.Text + "') ORDER BY [نام و نام خانوادگی]");
            
        }

    }
}
