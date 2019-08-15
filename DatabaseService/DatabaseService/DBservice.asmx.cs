using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data.Sql;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.OleDb;

namespace DatabaseService
{
    /// <summary>
    /// Summary description for DBservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DBservice : WebService
    {
        int j;
        [WebMethod]
        public int InsertData(String username, String email, Int64 phonenumber, String password ,String brand, String model, int mileage, String variant)
        {
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            int i;
            con.Open();
            model = model.ToUpper();
            brand = brand.ToUpper();
            variant = variant.ToUpper();
            int mile = 12;
            //SqlCommand cmd = new SqlCommand("Insert into mileagedeails Values ('" + brand + "','" + model + "','" + mileage + "','" + variant + "')", con);
            SqlCommand cmd1 = new SqlCommand("Insert into registerdetails Values ('" + username + "','" + phonenumber + "','" + email + "','" + password + "','" + model + "','" + brand + "','" + mileage + "','" + variant + "')", con);
            SqlCommand cmd2 = new SqlCommand("Select * from registerdetails where mobilenumber = '" + phonenumber  + "'  or email = '" + email +"' ", con);
            SqlCommand cmd3 = con.CreateCommand();
            cmd3.CommandText = "Select mileage from mileagedeails where brand = '" + brand + "' and model = '" + model + "'  and variant = '" + variant + "' " ;
            mile = (int)cmd3.ExecuteScalar();
            //SqlCommand cmd11 = new SqlCommand("Insert into mileagedeails Values ('" + brand + "','" + model + "','" + int.Parse(mile) + "','" + variant + "')", con);
            SqlCommand cmd12 = new SqlCommand("Insert into registerdetails Values ('" + username + "','" + phonenumber + "','" + email + "','" + password + "','" + model + "','" + brand + "','" + mile + "','" + variant + "')", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            if (dt.Rows.Count == 0)
                i = 1;
            else
                i = 0;
            if (i == 1 )
            {
                if (Math.Abs(mile - mileage) <=3)
                {
              //      cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                //    cmd11.ExecuteNonQuery();
                    cmd12.ExecuteNonQuery();
                }
                con.Close();
                return 0;
            }
            else
            {
                con.Close();
                return 1;
            }
        }


        [WebMethod]
        public int logincheck(String email, String password)
        {
            int i;
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from registerdetails where email = '" + email + "' and password = '" + password + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.Close();
                return 1;
            }

            else
            {
                con.Close();
                return 0;
            }
        }

        [WebMethod]
        public void setride(String startingplace, String destination, Decimal startingplacelat, Decimal startingplacelong, Decimal destlat, Decimal destlong, int hrs, int min, int day, int month, int year, String email, String mode,int cap)
        {
            int i;
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into ridedetail Values ('" + startingplace + "','" + destination + "','" + startingplacelat + "','" + startingplacelong + "','" + destlat + "','" + destlong + "','" + hrs + "','" + min + "','" + day + "','" + month + "','" + year + "','" + email + "','" + mode + "','" + cap + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        [WebMethod]
        public void searchride(String startingplace, String destination, Decimal startingplacelat, Decimal startingplacelong, Decimal destlat, Decimal destlong, int hrs, int min, int day, int month, int year, String email, String mode, int cap)
        {
            int i;
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into ridedetail Values ('" + startingplace + "','" + destination + "','" + startingplacelat + "','" + startingplacelong + "','" + destlat + "','" + destlong + "','" + hrs + "','" + min + "','" + day + "','" + month + "','" + year + "','" + email + "','" + mode + "','" + cap + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        [WebMethod]
        public void arraything(int currdate,int curryear,int currmonth)
        {
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            SqlCommand  cmd = new SqlCommand("Delete from ridedetail where (year < '" + curryear + "') or (year = '" + curryear + "' and month < '" + currmonth + "') or (year = '" + curryear + "' and month = '" + currmonth + "' and day < '" + currdate + "')",con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        [WebMethod]
        public int arrlength(int currdate)
        {
            String connetionString = null;
            String[] s1 = new String[200];
            SqlConnection cnn;
            SqlCommand cmd;
            String sql = null;
            SqlDataReader reader;
            int i = 0;
            connetionString = "Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;";
            sql = "Select day from ridedetail where day < '" + currdate + "' ";

            cnn = new SqlConnection(connetionString);


            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                s1[i] = reader.GetValue(0).ToString();
                i++;
            }
            reader.Close();
            cmd.Dispose();
            cnn.Close();
            return i;
        }
        [WebMethod]
        public String[] aftersearchinfo(int currdate)
        {
            String mode = "set";
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            String locmail;
            SqlCommand cmd33 = con.CreateCommand();
            cmd33.CommandText = "Select email from ridedetail where mode = '" + mode + "' ";
            locmail = (String)cmd33.ExecuteScalar();


            String connetionString = null;
            String[] s1 = new String[200];
            SqlConnection cnn;
            SqlCommand cmd = null, cmd1 = null, cmd2 = null, cmd3 = null, cmd4 = null, cmd5 = null, cmd6 = null, cmd7 = null, cmd8 = null;
            String sql = null, sql1 = null, sql2 = null, sql3 = null, sql4 = null, sql5 = null, sql6 = null, sql7 = null, sql8 = null;
            SqlDataReader reader, reader1, reader2, reader3, reader4, reader5, reader6, reader7, reader8;
            int i = 0;
            connetionString = "Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;";
            sql = "Select day from ridedetail where day < '" + currdate + "' ";
            sql1 = "Select name from registerdetails where email = '" + locmail + "' ";
            sql2 = "Select brand from registerdetails where email = '" + locmail + "' ";
            sql3 = "Select model from registerdetails where email = '" + locmail + "' ";
            sql4 = "Select startingplace from ridedetail where email = '" + locmail + "' and mode = '" + mode + "' ";
            sql5 = "Select destination from ridedetail where email = '" + locmail + "' and mode = '" + mode + "' ";
            sql6 = "Select hrs from ridedetail where email = '" + locmail + "' and mode = '" + mode + "' ";
            sql7 = "Select min from ridedetail where email = '" + locmail + "' and mode = '" + mode + "' ";
            sql8 = "Select capacity from ridedetail where email = '" + locmail + "' and mode = '" + mode + "' ";

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            try
            {
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    s1[i] = reader.GetValue(0).ToString();
                    i++;
                }
                reader.Close();

                cmd1 = new SqlCommand(sql1, cnn);
                reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {

                    s1[i] = reader1.GetValue(0).ToString();
                    i++;
                }
                reader1.Close();

                cmd2 = new SqlCommand(sql2, cnn);
                reader2 = cmd2.ExecuteReader();
                if (reader2.Read())
                {

                    s1[i] = reader2.GetValue(0).ToString();
                    i++;
                }
                reader2.Close();

                cmd3 = new SqlCommand(sql3, cnn);
                reader3 = cmd3.ExecuteReader();
                if (reader3.Read())
                {

                    s1[i] = reader3.GetValue(0).ToString();
                    i++;
                }
                reader3.Close();

                cmd4 = new SqlCommand(sql4, cnn);
                reader4 = cmd4.ExecuteReader();
                if (reader4.Read())
                {

                    s1[i] = reader4.GetValue(0).ToString();
                    i++;
                }
                reader4.Close();

                cmd5 = new SqlCommand(sql5, cnn);
                reader5 = cmd5.ExecuteReader();
                if (reader5.Read())
                {

                    s1[i] = reader5.GetValue(0).ToString();
                    i++;
                }
                reader5.Close();

                cmd6 = new SqlCommand(sql6, cnn);
                reader6 = cmd6.ExecuteReader();
                if (reader6.Read())
                {

                    s1[i] = reader6.GetValue(0).ToString();
                    i++;
                }
                reader6.Close();

                cmd7 = new SqlCommand(sql7, cnn);
                reader7 = cmd7.ExecuteReader();
                if (reader7.Read())
                {

                    s1[i] = reader7.GetValue(0).ToString();
                    i++;
                }
                reader7.Close();

                cmd8 = new SqlCommand(sql8, cnn);
                reader8 = cmd8.ExecuteReader();
                if (reader8.Read())
                {

                    s1[i] = reader8.GetValue(0).ToString();
                    i++;
                }
                reader8.Close();
            }
            catch(Exception ex)
            {
                s1[1] = "errorerrorerror";
            }
            cmd.Dispose();
            cmd1.Dispose();
            cmd2.Dispose();
            cmd3.Dispose();
            cmd4.Dispose();
            cmd5.Dispose();
            cmd6.Dispose();
            cmd7.Dispose();
            cmd8.Dispose();
            
            cnn.Close();
            return s1;
        }
        [WebMethod]
        public String[] aftersearchinfoyes(int currdate, String brand)
        {
            String mode = "set";
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            brand = brand.ToUpper();
            String connetionString = null;
            String[] s1 = new String[200];
            String[] s2 = new String[200];
            String[] s3 = new String[200];
            SqlConnection cnn;
            String eem = null;
            SqlCommand cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9,cmd10;
            String sql = null, sql1 = null, sql2 = null, sql3 = null, sql4 = null, sql5 = null, sql6 = null, sql7 = null, sql8 = null, sql9 = null,sql10 = null;
            SqlDataReader reader, reader1, reader2, reader3, reader4, reader5, reader6, reader7, reader8, reader9,reader10;
            int i = 0, t=0,k = 0;
            connetionString = "Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;";
            sql = "Select day from ridedetail where day < '" + currdate + "' ";
            sql9 = "Select email from registerdetails where brand = '" + brand + "'   ";
            sql10 = "Select email from ridedetail where  mode ='" + mode + "'";

            cnn = new SqlConnection(connetionString);
            cnn.Open();

            try
            {
                cmd9 = new SqlCommand(sql9, cnn);
                reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {

                    s2[k] = reader9.GetValue(0).ToString();

                    k++;
                }
                reader9.Close();
            }
            catch(Exception ex)
            {
                s1[1] = "nosuchbrand";
                return s1;
            }
            try
            {
                cmd10 = new SqlCommand(sql10, cnn);
                reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {

                    s3[t] = reader10.GetValue(0).ToString();
                    t++;
                }
                reader10.Close();
            }
            catch
            {
                s1[2] = "errorerrorerror";
                return s1;
            }
            try
            {
                for (int ii = 0; ii < s2.LongLength; ii++)
                {
                    for (int jj = 0; jj < s3.LongLength; jj++)
                    {
                        if (s2[ii].Equals(s3[jj]))
                        {
                            eem = s2[ii];
                            ii = 222;
                            break;
                        }
                    }

                }
            }
            catch(Exception)
            {
                s1[1] = "nosuchcar";
                return s1;
            }
            sql1 = "Select name from registerdetails where email = '" + eem + "' and brand = '" + brand + "'";
            sql2 = "Select brand from registerdetails where email = '" + eem + "' and brand = '" + brand + "'";
            sql3 = "Select model from registerdetails where email = '" + eem + "' and brand = '" + brand + "'";

            sql4 = "Select startingplace from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql5 = "Select destination from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql6 = "Select hrs from ridedetail where email = '" + eem + "' and mode = '" + mode + "'";
            sql7 = "Select min from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql8 = "Select capacity from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";



            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                s1[i] = reader.GetValue(0).ToString();
                i++;
            }
            reader.Close();

            cmd1 = new SqlCommand(sql1, cnn);
            reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {

                s1[i] = reader1.GetValue(0).ToString();
                i++;
            }
            reader1.Close();

            cmd2 = new SqlCommand(sql2, cnn);
            reader2 = cmd2.ExecuteReader();
            if (reader2.Read())
            {

                s1[i] = reader2.GetValue(0).ToString();
                i++;
            }
            reader2.Close();

            cmd3 = new SqlCommand(sql3, cnn);
            reader3 = cmd3.ExecuteReader();
            if (reader3.Read())
            {

                s1[i] = reader3.GetValue(0).ToString();
                i++;
            }
            reader3.Close();

            cmd4 = new SqlCommand(sql4, cnn);
            reader4 = cmd4.ExecuteReader();
            if (reader4.Read())
            {

                s1[i] = reader4.GetValue(0).ToString();
                i++;
            }
            reader4.Close();

            cmd5 = new SqlCommand(sql5, cnn);
            reader5 = cmd5.ExecuteReader();
            if (reader5.Read())
            {

                s1[i] = reader5.GetValue(0).ToString();
                i++;
            }
            reader5.Close();

            cmd6 = new SqlCommand(sql6, cnn);
            reader6 = cmd6.ExecuteReader();
            if (reader6.Read())
            {

                s1[i] = reader6.GetValue(0).ToString();
                i++;
            }
            reader6.Close();

            cmd7 = new SqlCommand(sql7, cnn);
            reader7 = cmd7.ExecuteReader();
            if (reader7.Read())
            {

                s1[i] = reader7.GetValue(0).ToString();
                i++;
            }
            reader7.Close();

            cmd8 = new SqlCommand(sql8, cnn);
            reader8 = cmd8.ExecuteReader();
            if (reader8.Read())
            {

                s1[i] = reader8.GetValue(0).ToString();
                i++;
            }
            reader8.Close();

            cmd.Dispose();
            cmd1.Dispose();
            cmd2.Dispose();
            cmd3.Dispose();
            cmd4.Dispose();
            cmd5.Dispose();
            cmd6.Dispose();
            cmd7.Dispose();
            cmd8.Dispose();
            cnn.Close();
            return s1;

        }
        [WebMethod]
        public String[] carsort(int currdate, String brand, String model)
        {
            String mode = "set";
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();
            brand = brand.ToUpper();
            model = model.ToUpper();
            String connetionString = null;
            String[] s1 = new String[200];
            String[] s2 = new String[200];
            String[] s3 = new String[200];
            SqlConnection cnn;
            String eem = null;
            SqlCommand cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10;
            String sql = null, sql1 = null, sql2 = null, sql3 = null, sql4 = null, sql5 = null, sql6 = null, sql7 = null, sql8 = null, sql9 = null, sql10 = null;
            SqlDataReader reader, reader1, reader2, reader3, reader4, reader5, reader6, reader7, reader8, reader9, reader10;
            int i = 0, t = 0, k = 0;
            connetionString = "Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;";
            sql = "Select day from ridedetail where day < '" + currdate + "' ";
            sql9 = "Select email from registerdetails where brand = '" + brand + "' and model = '"+model+"'  ";
            sql10 = "Select email from ridedetail where  mode ='" + mode + "'";

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            try
            {
                cmd9 = new SqlCommand(sql9, cnn);
                reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {

                    s2[k] = reader9.GetValue(0).ToString();

                    k++;
                }
                reader9.Close();
            }
            catch(Exception ex)
            {
                s1[1] = "nosuchcar";
                return s1;
            }
            try
            {
                cmd10 = new SqlCommand(sql10, cnn);
                reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {

                    s3[t] = reader10.GetValue(0).ToString();
                    t++;
                }
                reader10.Close();
            }
            catch(Exception ex)
            {
                s1[2] = "errorerrorerror";
                return s1;
            }
            try
            {
                for (int ii = 0; ii < s2.LongLength; ii++)
                {
                    for (int jj = 0; jj < s3.LongLength; jj++)
                    {
                        if (s2[ii].Equals(s3[jj], StringComparison.Ordinal))
                        {
                            eem = s2[ii];
                            ii = 222;
                            break;
                        }
                    }

                }
            }
            catch(Exception)
            {
                s1[1] = "nosuchcar";
                return s1;
            }
            sql1 = "Select name from registerdetails where email = '" + eem + "' and brand = '" + brand + "' and model = '" + model + "'";
            sql2 = "Select brand from registerdetails where email = '" + eem + "' and brand = '" + brand + "' and model = '" + model + "'";
            sql3 = "Select model from registerdetails where email = '" + eem + "' and brand = '" + brand + "' and model = '" + model + "'";

            sql4 = "Select startingplace from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql5 = "Select destination from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql6 = "Select hrs from ridedetail where email = '" + eem + "' and mode = '" + mode + "'";
            sql7 = "Select min from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql8 = "Select capacity from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";



            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                s1[i] = reader.GetValue(0).ToString();
                i++;
            }
            reader.Close();

            cmd1 = new SqlCommand(sql1, cnn);
            reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {

                s1[i] = reader1.GetValue(0).ToString();
                i++;
            }
            reader1.Close();

            cmd2 = new SqlCommand(sql2, cnn);
            reader2 = cmd2.ExecuteReader();
            if (reader2.Read())
            {

                s1[i] = reader2.GetValue(0).ToString();
                i++;
            }
            reader2.Close();

            cmd3 = new SqlCommand(sql3, cnn);
            reader3 = cmd3.ExecuteReader();
            if (reader3.Read())
            {

                s1[i] = reader3.GetValue(0).ToString();
                i++;
            }
            reader3.Close();

            cmd4 = new SqlCommand(sql4, cnn);
            reader4 = cmd4.ExecuteReader();
            if (reader4.Read())
            {

                s1[i] = reader4.GetValue(0).ToString();
                i++;
            }
            reader4.Close();

            cmd5 = new SqlCommand(sql5, cnn);
            reader5 = cmd5.ExecuteReader();
            if (reader5.Read())
            {

                s1[i] = reader5.GetValue(0).ToString();
                i++;
            }
            reader5.Close();

            cmd6 = new SqlCommand(sql6, cnn);
            reader6 = cmd6.ExecuteReader();
            if (reader6.Read())
            {

                s1[i] = reader6.GetValue(0).ToString();
                i++;
            }
            reader6.Close();

            cmd7 = new SqlCommand(sql7, cnn);
            reader7 = cmd7.ExecuteReader();
            if (reader7.Read())
            {

                s1[i] = reader7.GetValue(0).ToString();
                i++;
            }
            reader7.Close();

            cmd8 = new SqlCommand(sql8, cnn);
            reader8 = cmd8.ExecuteReader();
            if (reader8.Read())
            {

                s1[i] = reader8.GetValue(0).ToString();
                i++;
            }
            reader8.Close();

            cmd.Dispose();
            cmd1.Dispose();
            cmd2.Dispose();
            cmd3.Dispose();
            cmd4.Dispose();
            cmd5.Dispose();
            cmd6.Dispose();
            cmd7.Dispose();
            cmd8.Dispose();
            cnn.Close();
            return s1;

        }
        [WebMethod]
        public String[] aftersearchinfono(int currdate,String brand)
        {
            String mode = "set";
            SqlConnection con = new SqlConnection("Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;");
            con.Open();

            brand = brand.ToUpper();

            String connetionString = null;
            String[] s1 = new String[200];
            String[] s2 = new String[200];
            String[] s3 = new String[200];
            SqlConnection cnn;
            String eem = null;
            SqlCommand cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10;
            String sql = null, sql1 = null, sql2 = null, sql3 = null, sql4 = null, sql5 = null, sql6 = null, sql7 = null, sql8 = null, sql9 = null, sql10 = null;
            SqlDataReader reader, reader1, reader2, reader3, reader4, reader5, reader6, reader7, reader8, reader9, reader10;
            int i = 0, t = 0, k = 0;
            connetionString = "Data Source=teammfap.database.windows.net;Initial Catalog=database1;Persist Security Info=True;User ID=teammfap;Password=tteeaammmmffaapp123!@#;";
            sql = "Select day from ridedetail where day < '" + currdate + "' ";
            sql9 = "Select email from registerdetails where brand != '" + brand + "'   ";
            sql10 = "Select email from ridedetail where  mode ='" + mode + "'";

            cnn = new SqlConnection(connetionString);
            cnn.Open();

            try
            {
                cmd9 = new SqlCommand(sql9, cnn);
                reader9 = cmd9.ExecuteReader();
                while (reader9.Read())
                {

                    s2[k] = reader9.GetValue(0).ToString();

                    k++;
                }
                reader9.Close();
            }
            catch(Exception ex)
            {
                s1[1] = "nosuchcar";
                return s1;
            }
            try
            {
                cmd10 = new SqlCommand(sql10, cnn);
                reader10 = cmd10.ExecuteReader();
                while (reader10.Read())
                {

                    s3[t] = reader10.GetValue(0).ToString();
                    t++;
                }
                reader10.Close();
            }
            catch(Exception ex)
            {
                s1[2] = "errorerrorerror";
                return s1;
            }
            try
            {
                for (int ii = 0; ii < s2.LongLength; ii++)
                {
                    for (int jj = 0; jj < s3.LongLength; jj++)
                    {
                        if (s2[ii].Equals(s3[jj], StringComparison.Ordinal))
                        {
                            eem = s2[ii];
                            ii = 222;
                            break;
                        }
                    }

                }
            }
            catch(Exception)
            {
                s1[1] = "no such car";
                return s1;
            }
            sql1 = "Select name from registerdetails where email = '" + eem + "' and brand != '" + brand + "'";
            sql2 = "Select brand from registerdetails where email = '" + eem + "' and brand != '" + brand + "'";
            sql3 = "Select model from registerdetails where email = '" + eem + "' and brand != '" + brand + "'";

            sql4 = "Select startingplace from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql5 = "Select destination from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql6 = "Select hrs from ridedetail where email = '" + eem + "' and mode = '" + mode + "'";
            sql7 = "Select min from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";
            sql8 = "Select capacity from ridedetail where email = '" + eem + "' and mode = '" + mode + "' ";



            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {

                s1[i] = reader.GetValue(0).ToString();
                i++;
            }
            reader.Close();

            cmd1 = new SqlCommand(sql1, cnn);
            reader1 = cmd1.ExecuteReader();
            if (reader1.Read())
            {

                s1[i] = reader1.GetValue(0).ToString();
                i++;
            }
            reader1.Close();

            cmd2 = new SqlCommand(sql2, cnn);
            reader2 = cmd2.ExecuteReader();
            if (reader2.Read())
            {

                s1[i] = reader2.GetValue(0).ToString();
                i++;
            }
            reader2.Close();

            cmd3 = new SqlCommand(sql3, cnn);
            reader3 = cmd3.ExecuteReader();
            if (reader3.Read())
            {

                s1[i] = reader3.GetValue(0).ToString();
                i++;
            }
            reader3.Close();

            cmd4 = new SqlCommand(sql4, cnn);
            reader4 = cmd4.ExecuteReader();
            if (reader4.Read())
            {

                s1[i] = reader4.GetValue(0).ToString();
                i++;
            }
            reader4.Close();

            cmd5 = new SqlCommand(sql5, cnn);
            reader5 = cmd5.ExecuteReader();
            if (reader5.Read())
            {

                s1[i] = reader5.GetValue(0).ToString();
                i++;
            }
            reader5.Close();

            cmd6 = new SqlCommand(sql6, cnn);
            reader6 = cmd6.ExecuteReader();
            if (reader6.Read())
            {

                s1[i] = reader6.GetValue(0).ToString();
                i++;
            }
            reader6.Close();

            cmd7 = new SqlCommand(sql7, cnn);
            reader7 = cmd7.ExecuteReader();
            if (reader7.Read())
            {

                s1[i] = reader7.GetValue(0).ToString();
                i++;
            }
            reader7.Close();

            cmd8 = new SqlCommand(sql8, cnn);
            reader8 = cmd8.ExecuteReader();
            if (reader8.Read())
            {

                s1[i] = reader8.GetValue(0).ToString();
                i++;
            }
            reader8.Close();

            cmd.Dispose();
            cmd1.Dispose();
            cmd2.Dispose();
            cmd3.Dispose();
            cmd4.Dispose();
            cmd5.Dispose();
            cmd6.Dispose();
            cmd7.Dispose();
            cmd8.Dispose();
            cnn.Close();
            return s1;
        }
    }
}
