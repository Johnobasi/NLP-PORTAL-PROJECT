using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FIVERR_PROJECT.Models
{
    public class Member
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        DBAaccess obj = new DBAaccess();
        public List<Member> memberList()
        {
            List<Member> memlist = new List<Member>();
            obj.OpenCon();
            string q = "Select username,lpassword,email from member";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                Member mem = new Member();
                mem.UserName = sdr["username"].ToString();
                mem.Email = sdr["email"].ToString();
                mem.Password = sdr["lpassword"].ToString();


                memlist.Add(mem);
            }
            sdr.Close();
            obj.CloseCon();

            return memlist;
        }
        public Member memberDetails(string name)
        {
            Member mem = new Member();
            obj.OpenCon();
            string q = "Select username,lpassword,email from member where username='" + name + "'";
            SqlCommand cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.Read())
            {
                mem.UserName = sdr[0].ToString();
                mem.Email = sdr[2].ToString();
                mem.Password = sdr[1].ToString();
            }
          
            sdr.Close();
            obj.CloseCon();

            return mem;
        }

    }
}