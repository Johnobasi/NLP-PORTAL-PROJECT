using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FIVERR_PROJECT.Models
{
    public class Signup
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        DBAaccess obj = new DBAaccess();
        public List<Signup> SignList()
        {
            List<Signup> slist = new List<Signup>();
            obj.OpenCon();
            string q = "Select username,spassword,email from signup";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                Signup s = new Signup();
                s.UserName = sdr["username"].ToString();
                s.Email = sdr["email"].ToString();
                s.Password = sdr["spassword"].ToString();


                slist.Add(s);
            }
            sdr.Close();
            obj.CloseCon();

            return slist;
        }
        public bool LoginCheck(String uname, String pass)
        {

            bool res = false;
            obj.OpenCon();
            string q = "Select username,spassword,email from signup where username='" + uname + "' and spassword='" + pass + "'";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();

            if (sdr.Read())
            {


                if (sdr["username"].ToString() == uname && sdr["spassword"].ToString() == pass)
                {
                    res = true;
                    return res;


                }

            }

            sdr.Close();
            obj.CloseCon();
            return res;
        }
        public bool memberCheck(String uname, String pass)
        {

            bool res = false;
            obj.OpenCon();
            string q = "Select username,lpassword,email from member where username='" + uname + "' and lpassword='" + pass + "'";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();

            if (sdr.Read())
            {


                if (sdr["username"].ToString() == uname && sdr["lpassword"].ToString() == pass)
                {
                    res = true;
                    return res;


                }
            }
            sdr.Close();
            obj.CloseCon();
            return res;
        }
    }
}