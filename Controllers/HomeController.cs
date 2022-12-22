using FIVERR_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebGrease.Css.Extensions;

namespace Fiverrproject.Controllers
{
    public class HomeController : Controller
    {
        DBAaccess obj = new DBAaccess();
        public ActionResult Index()
        {
            List<testimonial> tlist = new List<testimonial>();
            testimonial tes = new testimonial();
            obj.OpenCon();
            string q = "Select name, category, testimony, filepath, likes, tcom from testimony";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();


            while (sdr.Read())
            {

                tes.Name = sdr["name"].ToString();
               
                tes.Testimonial_Category = sdr["category"].ToString();
                tes.Testimony = sdr["testimony"].ToString();
                tes.path = sdr["filepath"].ToString();
                if (sdr["likes"].ToString() == "")
                {
                    tes.likes = 0;
                }
                else
                {
                    tes.likes = int.Parse(sdr["likes"].ToString());
                }
                if (sdr["tcom"].ToString() == "")
                {
                    tes.comm = 0;
                }
                else
                {
                    tes.comm = int.Parse(sdr["tcom"].ToString());
                }


                tlist.Add(tes);
            }
            sdr.Close();
            obj.CloseCon();

            return View(tlist);
        }

        public ActionResult Giving()
        {


            return View();
        }
    

        public ActionResult likes(String d)
        {

            testimonial tes = new testimonial();
            DBAaccess db = new DBAaccess();

            db.OpenCon();
            string q = "Select * from testimony where testimony='" + d + "'";
            db.cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = db.cmd.ExecuteReader();
            sdr.Read();
            if (sdr["likes"].ToString() == "")
            {
                tes.likes = 0;
            }
            else
            {
                tes.likes = int.Parse(sdr["likes"].ToString());
            }


            sdr.Close();
            db.CloseCon();


            if (tes.likes == 0)
            {
                string q1 = "update testimony set likes = 0 where testimony='" + d + "'";
                db.InsertUpdateDelete(q1);
                string q2 = "update testimony set likes = likes + 1 where testimony='" + d + "'";
                db.InsertUpdateDelete(q2);

            }
            else
            {
                string q2 = "update testimony set likes = likes + 1 where testimony='" + d + "'";
                db.InsertUpdateDelete(q2);

            }


            return RedirectToAction("Index");
        }
        public ActionResult comme(string e)
        {
            testimonial tes = new testimonial();
           
            return View(tes.tesibase(e));
        }
        [HttpPost]
        public ActionResult comme(testimonial tes)
        {
            string quer = "insert into comments  values('"+tes.Testimony+"','"+tes.comments+"')";
            obj.InsertUpdateDelete(quer);
            string que = "select count(comment) from comments where testimony='" + tes.Testimony + "' ";
            int count=obj.count(que);
            string query = "update testimony set tcom ="+count+ " where  testimony='"+tes.Testimony+"'";
            obj.InsertUpdateDelete(query);
            Response.Write("<script type = 'text/javascript'>alert('Success Fully Uploaded Your Comment');</script>");
            return RedirectToAction("Index");
        }
       
        public ActionResult Contact()
        {
            return View();
        }      
    }
}