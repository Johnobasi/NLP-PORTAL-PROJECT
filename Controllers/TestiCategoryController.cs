using FIVERR_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIVERR_PROJECT.Controllers
{
    public class TestiCategoryController : Controller
    {
        DBAaccess db = new DBAaccess();
        testimonial obj=new testimonial();
        List<testimonial> lis;
        public ActionResult Childbirth()
        {
            lis = obj.CategoryList("Childbirth");
            return View(lis);
        }
        public ActionResult Finance()
        {
            lis = obj.CategoryList("Finance");
            return View(lis);
        }
        public ActionResult Investment()
        {
            lis = obj.CategoryList("Investment");
            return View(lis);
        }
        public ActionResult Funding()
        {
            lis = obj.CategoryList("Funding");
            return View(lis);
        }

        public ActionResult Immigration()
        {
            lis = obj.CategoryList("Immigration");
            return View(lis);
        }

        public ActionResult Career() 
        {
            lis = obj.CategoryList("Career");
            return View(lis);
        }
        public ActionResult Business()
        {
            lis = obj.CategoryList("Business");
            return View(lis);
        }
        public ActionResult Schoolfees()
        {
            lis = obj.CategoryList("Schoolfees");
            return View(lis);
        }
        public ActionResult Dtesti(string ne)
        {

            testimonial tes = new testimonial();
           

            db.OpenCon();
            string q = "Select * from testimony where name ='" + ne + "' ";
           db.cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = db.cmd.ExecuteReader();
            sdr.Read();
            tes.Name = sdr[0].ToString();
     
            tes.Testimonial_Category = sdr[1].ToString();

            tes.Testimony = sdr[2].ToString();
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

            sdr.Close();
            db.CloseCon();

            return View(tes);
        }
        public ActionResult like(String d)
        {

            testimonial tes = new testimonial();


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


            if (tes.likes==0)
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
           

            return RedirectToAction("Index", "Home");
        }
        public ActionResult comme(string e)
        {
            testimonial tes = new testimonial();

            return View(tes.tesibase(e));
        }
        [HttpPost]
        public ActionResult comme(testimonial tes)
        {
            string quer = "insert into comments  values('" + tes.Testimony + "','" + tes.comments + "')";
            db.InsertUpdateDelete(quer);
            string que = "select count(comment) from comments where testimony='" + tes.Testimony + "' ";
            int count = db.count(que);
            string query = "update testimony set tcom =" + count + " where  testimony='" + tes.Testimony + "'";
            db.InsertUpdateDelete(query);
            Response.Write("<script type = 'text/javascript'>alert('Success Fully Uploaded Your Comment');</script>");
            return RedirectToAction("Index","Home");
        }
    }
}