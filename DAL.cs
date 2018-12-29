using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace IGNco.Helper
{
    public class DAL
    {
        public static string logedin { get; set; }
        public static string dataSource { get; set; }
        public static string googlelogedinImg { get; set; }
        public static string ConnectionStr = "Data Source="+ dataSource + ";Initial Catalog=SPProject;Integrated Security=True";
        public int runCommand(string spName, List<DatabaseModel> Spcmd)
        {
            string pro = ConnectionStr;
            int nat = (int)_EnumManager.DbState.success;

            SqlConnection thisconnection = new SqlConnection();
            thisconnection.ConnectionString = pro;
            try
            {
                SqlCommand cmd = new SqlCommand(spName, thisconnection);
                //cmd.CommandType = CommandType.StoredProcedure;

                thisconnection.Open();
                foreach (var param in Spcmd)
                {
                    cmd.Parameters.AddWithValue(param.ParametrName, param.Value);
                }

                cmd.ExecuteNonQuery();
            }
            catch
            {
                nat = (int)_EnumManager.DbState.error;
            }
            finally
            {
                thisconnection.Close();
            }

            return nat;
        }

        public int hasRows(string spName, List<DatabaseModel> Spcmd)
        {
            int nat = (int)_EnumManager.DbState.success;
            string pro = ConnectionStr;
            SqlConnection thisconnection = new SqlConnection();
            thisconnection.ConnectionString = pro;
            try
            {
                SqlCommand cmd = new SqlCommand(spName, thisconnection);
                //cmd.CommandType = CommandType.StoredProcedure;
                thisconnection.Open();

                foreach (var param in Spcmd)
                {
                    cmd.Parameters.AddWithValue(param.ParametrName, param.Value);
                }

                SqlDataReader rdr0 = cmd.ExecuteReader();
                if (rdr0.HasRows == true)
                {
                    nat = (int)_EnumManager.DbState.hasrows;
                }
                else
                {
                    nat = (int)_EnumManager.DbState.empty;
                }
            }
            catch { nat = (int)_EnumManager.DbState.error; }
            finally
            {
                thisconnection.Close();
            }
            return nat;
        }

        public DataTable selectTable(string Ctxt, out int result)
        {
            DataTable dt = new DataTable();
            string pro = ConnectionStr;

            SqlConnection thisconnection = new SqlConnection();
            thisconnection.ConnectionString = pro;
            try
            {
                thisconnection.Open();
                SqlDataAdapter rdr = new SqlDataAdapter(Ctxt, thisconnection);
                try
                {
                    rdr.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        result = (int)_EnumManager.DbState.empty;
                    }
                    else
                    {
                        result = (int)_EnumManager.DbState.success;
                    }
                }
                catch { result = (int)_EnumManager.DbState.error; }
            }
            catch
            {
                result = (int)_EnumManager.DbState.error;
            }
            finally
            {
                thisconnection.Close();
            }
            return dt;
        }

        public DataTable selectTable(string Ctxt, List<DatabaseModel> Spcmd, out int result)
        {
            DataTable dt = new DataTable();
            string pro = ConnectionStr;

            SqlConnection thisconnection = new SqlConnection();
            thisconnection.ConnectionString = pro;
            try
            {
                thisconnection.Open();
                SqlCommand cmd = new SqlCommand(Ctxt, thisconnection);
                //cmd.CommandType = CommandType.StoredProcedure;
                foreach (var param in Spcmd)
                {
                    cmd.Parameters.AddWithValue(param.ParametrName, param.Value);
                }
                SqlDataAdapter rdr = new SqlDataAdapter(cmd);

                try
                {
                    rdr.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        result = (int)_EnumManager.DbState.empty;
                    }
                    else
                    {
                        result = (int)_EnumManager.DbState.success;
                    }
                }
                catch { result = (int)_EnumManager.DbState.error; }
            }
            catch
            {
                result = (int)_EnumManager.DbState.error;
            }
            finally
            {
                thisconnection.Close();
            }
            return dt;
        }

        public static DataTable noRow()
        {
            DataTable dtsb = new DataTable();
            DataColumn c1 = new DataColumn("name", typeof(System.String));
            if (!dtsb.Columns.Contains("name"))
            {
                dtsb.Columns.Add(c1);
            }

            DataRow dr = dtsb.NewRow();
            dr["name"] = "false";
            dtsb.Rows.Add(dr);

            return dtsb;
        }

        public static DataTable error()
        {
            DataTable dtsb = new DataTable();
            DataColumn c1 = new DataColumn("name", typeof(System.String));
            if (!dtsb.Columns.Contains("name"))
            {
                dtsb.Columns.Add(c1);
            }

            DataRow dr = dtsb.NewRow();
            dr["name"] = "er";
            dtsb.Rows.Add(dr);

            return dtsb;
        }

        public string persianCDate()
        {
            System.Globalization.PersianCalendar oPersianC = new System.Globalization.PersianCalendar();
            string Day, Month, Year, Date = "";
            //date
            Year = oPersianC.GetYear(System.DateTime.Now).ToString();

            Date = Year;
            return Date;
        }
        public string persianDate()
        {
            System.Globalization.PersianCalendar oPersianC = new System.Globalization.PersianCalendar();
            string Day, Month, Year, Date = "";
            //date
            Year = oPersianC.GetYear(System.DateTime.Now).ToString();
            Month = oPersianC.GetMonth(System.DateTime.Now).ToString();
            Day = oPersianC.GetDayOfMonth(System.DateTime.Now).ToString();
            if ((int.Parse(Month) > 0) && (int.Parse(Month) < 10))
            {
                Month = "0" + Month;
            }

            if ((int.Parse(Day) > 0) && (int.Parse(Day) < 10))
            {
                Day = "0" + Day;
            }
            Date = Year + "/" + Month + "/" + Day;
            return Date;
        }
        public string persianTime()
        {
            System.Globalization.PersianCalendar oPersianC = new System.Globalization.PersianCalendar();
            string testc = "";
            //date
            testc = oPersianC.GetHour(System.DateTime.Now).ToString();
            testc += ":" + oPersianC.GetMinute(System.DateTime.Now).ToString();
            return testc;
        }
        public string persianTimeAndSec()
        {
            System.Globalization.PersianCalendar oPersianC = new System.Globalization.PersianCalendar();
            string testc = "";
            //date
            testc = oPersianC.GetHour(System.DateTime.Now).ToString();
            testc += ":" + oPersianC.GetMinute(System.DateTime.Now).ToString();
            testc += ":" + oPersianC.GetSecond(System.DateTime.Now).ToString();
            return testc;
        }

        public void Basket(ref DataTable basketDt, string id, string price, string count)
        {
            //DataTable dtsb = new DataTable();
            if (basketDt.Rows.Count == 0)
            {
                DataColumn c0 = new DataColumn("ProId", typeof(System.String));
                DataColumn c1 = new DataColumn("ProPrice", typeof(System.String));
                DataColumn c2 = new DataColumn("ProCount", typeof(System.String));
                if (!basketDt.Columns.Contains("ProId"))
                {
                    basketDt.Columns.Add(c0);
                    basketDt.Columns.Add(c1);
                    basketDt.Columns.Add(c2);
                }
            }

            bool insert = false;
            for (int i = 0; i < basketDt.Rows.Count; i++)
            {
                if (id == basketDt.Rows[i][0].ToString())
                {
                    float number = float.Parse(count) + float.Parse(basketDt.Rows[i][2].ToString());
                    basketDt.Rows[i][2] = number.ToString();
                    insert = true;
                    break;
                }
            }

            if (!insert)
            {
                DataRow dr = basketDt.NewRow();
                dr["ProId"] = id;
                dr["ProPrice"] = price;
                dr["ProCount"] = count;
                basketDt.Rows.Add(dr);
            }

        }

        //public void sendSms(string text, string mobile, ref int SmsResult)
        //{
        //    string txt = text;
        //    string smsusername = ConfigurationManager.AppSettings["smsUsername"].ToString();
        //    string smspass = ConfigurationManager.AppSettings["smsPassword"].ToString();
        //    byte[] state = null;
        //    long[] rid = null;
        //    string[] mobiles = { mobile };

        //    //net.postgah.Send sms = new net.postgah.Send();
        //    //SmsResult = sms.SendSms(smsusername, smspass, "30004015999133", mobiles, txt, false, ref state, ref rid);
        //}

        public string getTextDoc(string path)
        {
            string text = "";
            using (StreamReader stremreader = new StreamReader(path, true))
            {
                text = stremreader.ReadToEnd() + "\n\r#";
            }
            return text;
        }

        public bool SetTextDoc(string path, string text)
        {
            bool result = true;
            using (StreamWriter streamwriter = new StreamWriter(path, true))
            {
                streamwriter.WriteLine(text);
            }
            return result;
        }

        public string checkID(string DbName)
        {
            string ID = Guid.NewGuid().ToString();

            DAL dal = new DAL();
            string spCom = string.Empty;
            List<DatabaseModel> dbList = new List<DatabaseModel>();
            DatabaseModel dbm;

            spCom = "SELECT * from " + DbName + " where [ID]=@ID";
            dbm = new DatabaseModel();
            dbm.ParametrName = "@ID";
            dbm.Value = ID;
            dbList.Add(dbm);

            int result = dal.hasRows(spCom, dbList);
            if (result != (int)_EnumManager.DbState.hasrows)
            {
                return ID;
            }

            return checkID(DbName);
        }

        public List<ListModel> MakeList(DataTable dt)
        {
            List<ListModel> list = new List<ListModel>();
            ListModel lm;
            lm = new ListModel();
            lm.Member = "-- انتخاب --";
            lm.Value = "0";
            list.Add(lm);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lm = new ListModel();
                lm.Member = dt.Rows[i][1].ToString();
                lm.Value = dt.Rows[i][0].ToString();
                list.Add(lm);
            }
            return list;
        }
        public static string setpino(string get)
        {
            string tb = get;
            string lasto = "";
            tb = tb.Replace(",", "");
            if (tb.Length > 3)
            {
                if (tb.Length > 6)
                {
                    if (tb.Length > 9)
                    {
                        if (tb.Length > 12)
                        {
                            int u = tb.Length - 12;
                            string l = tb;
                            l = l.Replace(",", "");
                            string a = l.Substring(0, u);
                            string b = l.Substring(u, 3);
                            string c = l.Substring(u + 3, 3);
                            string d = l.Substring(u + 3 + 3, 3);
                            string e = l.Substring(u + 3 + 3 + 3, 3);
                            a = a.Replace(",", "");
                            b = b.Replace(",", "");
                            c = c.Replace(",", "");
                            d = d.Replace(",", "");
                            e = e.Replace(",", "");
                            lasto = a + "," + b + "," + c + "," + d + "," + e;

                        }
                        else
                        {
                            int u = tb.Length - 9;
                            string l = tb;
                            l = l.Replace(",", "");
                            string a = l.Substring(0, u);
                            string b = l.Substring(u, 3);
                            string c = l.Substring(u + 3, 3);
                            string d = l.Substring(u + 3 + 3, 3);
                            a = a.Replace(",", "");
                            b = b.Replace(",", "");
                            c = c.Replace(",", "");
                            d = d.Replace(",", "");
                            lasto = a + "," + b + "," + c + "," + d;
                        }
                    }
                    else
                    {
                        int u = tb.Length - 6;
                        string l = tb;
                        l = l.Replace(",", "");
                        string a = l.Substring(0, u);
                        string b = l.Substring(u, 3);
                        string c = l.Substring(u + 3, 3);
                        a = a.Replace(",", "");
                        b = b.Replace(",", "");
                        c = c.Replace(",", "");
                        lasto = a + "," + b + "," + c;
                    }
                }
                else
                {
                    int u = tb.Length - 3;
                    string l = tb;
                    string a = l.Substring(0, u);
                    string b = l.Substring(u, 3);
                    a = a.Replace(",", "");
                    b = b.Replace(",", "");
                    string f = a + "," + b;
                    lasto = f;

                }
                get = lasto;
            }

            return get;
        }

        public string getData(string fileName)
        {
            string v = "";
            //string dir = path;

            if (File.Exists(fileName + ".txt"))
            {
                StreamReader st = new StreamReader(fileName + ".txt");
                string p1 = "";
                do
                {
                    p1 = st.ReadLine();

                } while (st.Peek() != -1);
                st.Close();
                v = p1;
            }
            else
            {
                v = "";
            }
            return v;
        }
        public string SetData(string fileName, string txt)
        {
            string v = "";
            //string dir = path;

            StreamWriter stw = null;
            stw = File.CreateText(fileName + ".txt");
            stw.WriteLine(txt);
            stw.Close();
            v = "";

            return v;
        }

    }

    public class ListModel
    {
        public string Member { get; set; }
        public object Value { get; set; }
    }
    public class DatabaseModel
    {
        public string ParametrName { get; set; }
        public object Value { get; set; }
    }

    public class UserModel
    {
        public string Name { get; set; }
        public object Role { get; set; }
        public object BaseId { get; set; }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 170,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = 400 };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 75, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}