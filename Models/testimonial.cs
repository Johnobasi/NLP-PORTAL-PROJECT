using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace FIVERR_PROJECT.Models
{
    public class testimonial
    {
        DBAaccess obj = new DBAaccess();

        [Required]
  
        public String Name { get; set; }

    
        [Required]
  
        public String Testimonial_Category { get; set; }
        [Required]
      
        public String Testimony { get; set; }

        public int likes { get; set; }
        public int comm { get; set; }
        public String comments { get; set; }

        public String reason { get; set; }
         public String path { get; set; }

        [Required]
        [Display(Name = "UPLOAD File")]
        public HttpPostedFileBase File { get; set; }
        public List<testimonial> TestiList()
        {
            List<testimonial> tlist = new List<testimonial>();
            obj.OpenCon();
            string q = "Select name,category, testimony,filepath from testi_check";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                testimonial tes = new testimonial();
                tes.Name = sdr["name"].ToString();

              

                tes.Testimonial_Category = sdr["category"].ToString();
                tes.Testimony = sdr["testimony"].ToString();
              
                tlist.Add(tes);
            }
            sdr.Close();
            obj.CloseCon();

            return tlist;
        }

        public testimonial decline(string d)
        {
            obj.OpenCon();
            string q = "Select testimony from testi_check  where testimony='" + d + "'";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            sdr.Read();

            testimonial tes = new testimonial();

            tes.Testimony = sdr["testimony"].ToString();




            sdr.Close();
            obj.CloseCon();

            return tes;
        }
        public List<testimonial> APPtestiList()
        {
            List<testimonial> tlist = new List<testimonial>();
            obj.OpenCon();
            string q = "Select * from testimony";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                testimonial tes = new testimonial();

                tes.Name = sdr["name"].ToString();
                tes.Testimonial_Category = sdr["category"].ToString();
                tes.Testimony = sdr["testimony"].ToString();
                if (sdr["likes"].ToString() != "")
                {
                   
                    tes.likes = int.Parse(sdr["likes"].ToString());
                }
                else
                {
                    tes.likes = 0;
                }
                if (sdr["tcom"].ToString() != "")
                {
                  
                    tes.comm = int.Parse(sdr["tcom"].ToString());
                }
                else
                {
                    tes.comm = 0;
                }
              
                tlist.Add(tes);
            }
            sdr.Close();
            obj.CloseCon();

            return tlist;
        }
        public testimonial tesibase( string e)
        {
            
            obj.OpenCon();
            string q = "Select testimony from testimony where testimony='" + e+"'";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            sdr.Read();
            
                testimonial tes = new testimonial();

                tes.Testimony = sdr["testimony"].ToString();

            


            sdr.Close();
            obj.CloseCon();

            return tes;
        }
        public List<testimonial> CategoryList(String categ)
        {
            List<testimonial> clist = new List<testimonial>();
            obj.OpenCon();
            string q = "Select name, category, testimony,filepath from testimony where category='"+categ+"'";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            while (sdr.Read())
            {
                testimonial tes = new testimonial();
                tes.Name = sdr["name"].ToString();
             
                tes.Testimony = sdr["testimony"].ToString();
                
                clist.Add(tes);
            }
            sdr.Close();
            obj.CloseCon();

            return clist;
        }
    }
}