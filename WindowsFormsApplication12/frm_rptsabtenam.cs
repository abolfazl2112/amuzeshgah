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
    public partial class frm_rptsabtenam : Form
    {
        string sqlAllPerson = "SELECT codeMeli AS [کد ملی], name AS نام, family AS [نام خاوادگی], father AS [نام پدر], shsh AS [شماره شناسنامه], mobile AS موبایل, tarikht AS [تاریخ تولد], sadere AS صادره,  "+
                              "mtavalod AS [محل تولد], sal AS [سال ورود], mah AS [ماه ورود] FROM person";

        string sqlperson = "SELECT person.family + ' ' +  person.name[نام و نام خانوادگی], person.father'نام پدر', person.tarikht[تاریخ تولد], " +
                " person.codeMeli[کد ملی], person.shsh'شماره شناسنامه', person.sadere'صادره',[P&R].Rname'نام رشته',[P&R].Ronvan'عنوان استاندارد', [P&R].nomreN[نمره نظری],[P&R].nomreA[نمره عملی], [P&R].shGovahi[شماره گواهینامه] " +
                " FROM person INNER JOIN [P&R] ON person.codeMeli = [P&R].PID ";

        DataTable datatable;
        public frm_rptsabtenam()
        {
            InitializeComponent();
        }

        private void btnfaragir_Click(object sender, EventArgs e)
        {

            string sqlselectperson = "SELECT codeMeli, name, family, father, shsh, tarikht, sadere, mtavalod, sal, mah, mobile " +
                                     " FROM person WHERE(codeMeli = N'" + dataGridViewX2.CurrentRow.Cells[3].Value.ToString() + "')";

            string sqlselectreshte = "SELECT name, onvan, vamali, vnazari FROM reshte WHERE (name = N'" + comboreshte2.Text + "')";


            string sqlRP = "SELECT [P&R].PID, reshte.name, reshte.onvan, reshte.vamali, reshte.vnazari "+
                           " FROM [P&R] INNER JOIN reshte ON [P&R].RID = reshte.code "+
                           " WHERE([P&R].PID = N'" + dataGridViewX2.CurrentRow.Cells[3].Value.ToString() + "') AND (reshte.name = N'"+comboreshte2.Text+"')";




            
            try
            {
                string str = Application.StartupPath + "\\rep\\Sabtnam.mrt";
                Stimulsoft.Report.StiReport stikol = new Stimulsoft.Report.StiReport();
                Stimulsoft.Report.StiReport stitmp = new Stimulsoft.Report.StiReport();
                stitmp.Load(str);
                //DataSet ds = new DataSet();
                DataTable dt1 = DataManagement.Search(sqlselectperson);
                //ds.Tables.Add(dt1);
                DataTable dt2 = DataManagement.Search(sqlRP);
                //ds.Tables.Add(dt2);
                //ds.Tables[0].TableName = "khate1";
                //ds.Tables[1].TableName = "variable1";
                stitmp.RegData("khate1", dt1);
                stitmp.RegData("variable1", dt2);

                //stikol.Report = stitmp;
                
                
                stikol.Show(this);

                //stikol.Dictionary.DataSources.Items[0].Dictionary.DataSources = ds;
                //stikol.RegData("variable1", dtPerson);
                //stikol.Dictionary.DataSources.Items[1].DataTable = dtPerson;
            }
            catch (Exception ex) { FMessegeBox.FarsiMessegeBox.Show(ex.Message); }
        }


        private void printPerson()
        {
            if (comboreshte2.Text == "" || dataGridViewX2.RowCount == 0) return;
            string sqlperson = "SELECT person.family + ' ' +  person.name[name], person.father, person.tarikht[tavalod], " +
                " person.codeMeli[meli], person.shsh, person.sadere, [P&R].nomreN[nazari],[P&R].nomreA[amali], [P&R].shGovahi[govahiname] " +
                " FROM person INNER JOIN [P&R] ON person.codeMeli = [P&R].PID WHERE ([P&R].Rname = N'" + comboreshte2.Text + "')AND(person.codeMeli = N'" + textBoxX1.Text + "') ORDER BY name";

            string sqlVariable = "SELECT reshte.code[codeEstandard], reshte.name[nameReshte], reshte.date[DateAzmoon], reshte.onvan[onvanReshte], reshte.dateShoroo[DateShoroo], [P&R].dateSodoor[DateSodoor] " +
                                 "FROM reshte INNER JOIN [P&R] ON reshte.code = [P&R].RID WHERE (reshte.name = N'" + comboreshte2.Text + "')";

            string str = Application.StartupPath + "\\rep\\perrep.mrt";
            DataManagement.DT = DataManagement.Search(sqlperson);
            try
            {
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
                //DataManagement.DT.Rows[0]["number"] = dataGridViewX1.RowCount.ToString();
                //DataManagement.DT.Rows[0]["nameMarkaz"] = txtNameMarkaz.Text;
                //DataManagement.DT.Rows[0]["nahiye"] = txtNahiye.Text;
                //DataManagement.DT.Rows[0]["sal"] = txtSal.Text;
                //DataManagement.DT.Rows[0]["nobat"] = txtNobat.Text;

                stikol.Load(str);
                stikol.RegData("variable", DataManagement.DT);
                stikol.Dictionary.DataSources.Items[0].DataTable = DataManagement.DT;
                stikol.Show();
            }
            catch
            {
                FMessegeBox.FarsiMessegeBox.Show("مشکل در چاپ اطلاعات", "اخطار");
            }

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_rptsabtenam_Load(object sender, EventArgs e)
        {
            dataGridViewX2.DataSource =datatable= DataManagement.Search(sqlperson);
            DataTable dd=DataManagement.Search("select name from reshte");
            for (int i = 0; i < dd.Rows.Count; i++)
                comboreshte2.Items.Add(dd.Rows[i][0].ToString());
        }

        private void groupPanel4_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (comboreshte2.Text == "" || dataGridViewX2.RowCount == 0) return;

            dataGridViewX2.DataSource = datatable = DataManagement.Search(sqlperson + " WHERE ([P&R].Rname = N'" + comboreshte2.Text + "')AND(person.codeMeli = N'" + textBoxX1.Text + "') ORDER BY name");
        }

        private void comboreshte2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewX2.DataSource = datatable = DataManagement.Search(sqlperson + " where([P&R].Rname = N'"+comboreshte2.Text+"') ");
            textBoxX1.Text = "";
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (comboreshte2.Text == "") return;
            dataGridViewX2.DataSource = datatable = DataManagement.Search(sqlperson + " where([P&R].Rname = N'" + comboreshte2.Text + "') AND (person.codeMeli = N'" + textBoxX1.Text + "') ");
        }
    }
}
