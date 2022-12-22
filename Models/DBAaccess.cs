using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace FIVERR_PROJECT.Models
{
    public class DBAaccess
    {

        static string constr = @"Data Source=DESKTOP-7M608D4\SQLEXPRESS; initial catalog=FIVERR PROJECT; integrated security=true;";
        public SqlConnection con = new SqlConnection(constr);
        public SqlCommand cmd = null;
        public void OpenCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void CloseCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        public void InsertUpdateDelete(string query)
        {
            OpenCon();
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            CloseCon();
        }
        public int count(string query)
        {
            OpenCon();
            int result = 0;
            cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                result = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
            CloseCon();
            return result;
        }
        public int getdata(string query)
        {
            OpenCon();
            int result = 0;
            cmd=new SqlCommand(query, con);
            SqlDataReader sdr=cmd.ExecuteReader();
            if (sdr.Read())
            {
                result = int.Parse(sdr[0].ToString());
            }
            sdr.Close();
            CloseCon();
            return result; 

        }
    }
}