using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;


namespace amoozeshgah
{
    public partial class sms : Form
    {
        int numberEnter = 0;

        public sms()
        {
            
            InitializeComponent();
        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                numberEnter++;
        }

        private void txtSms_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Languge_Keybord.Persian();
            buttonX3_Click_1(null, null);

            DataTable dt = DataManagement.Search("select usernamesms,passwordsms from setup");

            txtUserName.Text = dt.Rows[0][0].ToString();
            txtPass.Text = dt.Rows[0][1].ToString();

            dataGridView2.DataSource = DataManagement.Search("select mah'ماه',sal'سال' from person");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text == "" || txtPass.Text == "")
                {
                    FMessegeBox.FarsiMessegeBox.Show("نام کاربری و یا کلمه عبور را وارد کنید");
                    return;
                }

                if (txtShomare.Text == "")
                {
                    FMessegeBox.FarsiMessegeBox.Show("شماره اختصاصی وارد نشده است");
                    return;
                }
                if (txtSms.Text == "")
                {
                    FMessegeBox.FarsiMessegeBox.Show("متن پیامک وارد نشده است");
                    return;
                }

                string[] Mobile = new string[_txtMobile.RowCount - 1];
                for (int i = 0; i < _txtMobile.RowCount - 1; i++)
                    Mobile.SetValue(_txtMobile.Rows[i].Cells[0].Value.ToString(), i);

                _SendSmsFromWebServis(txtUserName.Text, txtPass.Text, txtShomare.Text, Mobile, txtSms.Text);
            }
            catch(Exception ex)
            {
                FMessegeBox.FarsiMessegeBox.Show("مشکل در ارسال پیامک" + "\n" + ex.Message);
            }
        }

        private void _SendSmsFromWebServis(string UserName, string Password, string Shomare, string[] Mobile, string SmsText)
        {
            try
            {
                if (!network.CheckConection()) { FMessegeBox.FarsiMessegeBox.Show("مشکل در اتصال به اینترنت"); return; }

                long[] rec = null;
                byte[] status = null;

                int retval = DataManagement.ws.SendSms(UserName, Password, Mobile, Shomare, SmsText, false, "", ref rec, ref status);

                string StringStatus = "";
                switch (retval)
                {
                    case 0:
                        StringStatus = "نام کاربری و یا رمز عبور اشتباه است";
                        break;
                    case 1:
                        StringStatus = "ارسال با موفقیت انجام شده است";
                        break;
                    case 2:
                        StringStatus = "اعتبار کافی نیست";
                        break;
                    case 3:
                        StringStatus = "محدودیت در ارسال روزانه";
                        break;
                    case 4:
                        StringStatus = "محدودیت در حجم ارسال";
                        break;
                    case 5:
                        StringStatus = "شماره فرستنده معتبر نیست";
                        break;
                }

                FMessegeBox.FarsiMessegeBox.Show(StringStatus);
            }
            catch
            {
                FMessegeBox.FarsiMessegeBox.Show("مشکل در ارسال ، لطفا تمامی اطلاعات را کامل کنید");
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtpish_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void lblLan_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblNumSms_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }


        private void txtShomare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                MenuShomare.BringToFront();
                int x = this.Location.X + (this.Size.Width - txtShomare.Size.Width - txtShomare.Location.X + 21);
                int y = this.Location.Y + (txtShomare.Location.Y + this.Location.Y + 21);
                MenuShomare.Show(new Point(x, y));
            }
        }

        private void txtShomare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;


            if (txtShomare.Text.Length >= 14)
                e.Handled = true;
            
        }

        private void txtShomare_TextChanged(object sender, EventArgs e)
        {
            if (txtShomare.Text == "")
                Pb.Enabled = false;
            else
            {
                Pb.Enabled = true;
            }
        }

        private void Pb_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < txtShomare.Items.Count; i++)
                if (txtShomare.Items[i].ToString() == txtShomare.Text)
                    return;

            txtShomare.Items.Add(txtShomare.Text);
            txtTmp.Text += "\n" + txtShomare.Text;
            saveToFile(Application.StartupPath + "\\smsCongig.mni");
        }

        private void saveToFile(string FileName)
        {
            StreamWriter myStreamWriter = null;
            try
            {
                myStreamWriter = File.CreateText(FileName);

                myStreamWriter.Write(txtTmp.Text);
                myStreamWriter.Flush();
            }
            catch (Exception exc)
            {
                MessageBox.Show("مشکل در ذخیره اطلاعات" + Environment.NewLine + "Exception: " + exc.Message);
            }
            finally
            {
                if (myStreamWriter != null)
                {
                    myStreamWriter.Close();
                }
            }
            
        }

        private void LoadFromFile(string FileName)
        {
            
            
            StreamReader myStreamReader = null;
            try
            {
                myStreamReader = File.OpenText(FileName);
                txtTmp.Text = myStreamReader.ReadToEnd();
                txtShomare.Items.AddRange(txtTmp.Lines);
                for (int i = 0; i<txtShomare.Items.Count; i++)
                    if (txtShomare.Items[i].ToString() == "")
                        txtShomare.Items.RemoveAt(i);
            }
            catch (Exception exc)
            {
                MessageBox.Show("مشکل در خواندن" + Environment.NewLine + "Exception: " + exc.Message);
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }
            }
        }

        public static string codegenerate(string t)
        {
            string tc = "";
            for (int j = 0; j < t.Length; j++)
            {
                String oS = t[j].ToString();
                byte[] bos = Encoding.ASCII.GetBytes(oS);
                MemoryStream ms = new MemoryStream();
                BinaryWriter brw = new BinaryWriter(ms);
                brw.Write(bos);
                String strBin = "";
                BitArray ba = new BitArray(ms.ToArray());
                for (int i = 0; i < ba.Length; i++)
                {
                    if (ba[i])
                    {
                        strBin += 1;
                    }
                    else
                    {
                        strBin += 0;
                    }
                }
                tc += strBin;
            }
            return tc;
        }

        private void buttonX2_Click_1(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPass.Text == "")
            {
                FMessegeBox.FarsiMessegeBox.Show("نام کاربری و یا کلمه عبور را وارد کنید"); return;
            }

            try
            {
                lblCredit.Text = DataManagement.ws.GetCredit(txtUserName.Text, txtPass.Text).ToString();
            }
            catch(Exception ex)
            {
                FMessegeBox.FarsiMessegeBox.Show(ex.Message);
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //    DataTable dt = DataManagement.Search("select mobile from person");
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //        if(dt.Rows[i][0].ToString()!="")
            //            _txtMobile.Rows.Add(dt.Rows[i][0].ToString());
            //}
            //catch { }
        }

        private DataGridView _GetMessages(string Location,int Index, int Count, DataGridView DataGrid)
        {
            try
            {
                if (!network.CheckConection()) { FMessegeBox.FarsiMessegeBox.Show("مشکل در اتصال به اینترنت"); return null; }
                servisesms.Send sms = new servisesms.Send();
                int locationInt = 0;
                if (Location == "دریافتی")
                    locationInt = 1;
                else if (Location == "ارسالی")
                    locationInt = 2;
                else
                    locationInt = 3;

                DataGrid.Rows.Clear();
                servisesms.MessagesBL[] messages = sms.GetMessages(txtUserName.Text, txtPass.Text, locationInt, txtShomare.Text, Index, Count);
                foreach (servisesms.MessagesBL message in messages)
                {
                    DataGrid.Rows.Add("", message.Sender, message.Body, PersianDate.GetPersianDate(message.SendDate));
                }
                return DataGrid;
            }
            catch (Exception ex)
            {
                FMessegeBox.FarsiMessegeBox.Show(ex.Message, "خطا");
                return null;
            }
        }


        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            int Index = Convert.ToInt32(GetMessageIndex.Text);
            int Count = Convert.ToInt32(GetMessageCount.Text);
            DataGridView dgrid = _GetMessages(txtlocation.Text, Index, Count, dataGridView1);
            if (dgrid == null) return;
            dataGridView1 = dgrid;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

    }
}
