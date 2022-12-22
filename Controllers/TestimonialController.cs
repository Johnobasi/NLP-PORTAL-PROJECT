using FIVERR_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using System.Data.SqlClient;


namespace FIVERR_PROJECT.Controllers
{
    public class TestimonialController : Controller
    {

        DBAaccess obj = new DBAaccess();
        public ActionResult approvedtesti()
        {
           
                testimonial obj = new testimonial();
                List<testimonial> sl = obj.APPtestiList();
                return View(sl);
            
           
        }


        public ActionResult Testimonial()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Testimonial(testimonial tes)
        {
            var allowedExtensions = new[] { ".bmp", ".png", ".jpg", ".gif", ".jpeg", ".mp4",".mp3", ".txt", ".doc", ".pdf", ".docx" };
            var nameoffile = tes.File.FileName;
            var ext = Path.GetExtension(nameoffile);
            string dbp = string.Empty;
            //getting  the  extension(ex-.jpg)
            if (allowedExtensions.Contains(ext))  //check  what  type of  extension
            {

                var path = Path.Combine(Server.MapPath("~/UserImages"), tes.Name.Trim()  + ext);

                tes.File.SaveAs(path);

                dbp = "/UserImages/" + tes.Name.Trim() + ext;

            }
            else
            {
                ViewBag.message = "Please  choose  .bmp, .png,.jpg, .gif, .jpeg,.mp4,.txt,.doc  file";
                return View("Testimonial");
            }


            string q = "insert into testi_check values('" + tes.Name + "', '" + tes.Testimonial_Category + "', '" + tes.Testimony + "','" + dbp + "')";

            obj.InsertUpdateDelete(q);

            Response.Write("<script type = 'text/javascript'>alert('Success Fully Uploaded Your Testimony');</script>");
            return RedirectToAction("Index","Home");


        }


        public ActionResult TestiDisplay()
        {
           
                testimonial obj = new testimonial();
                List<testimonial> sl = obj.TestiList();
                return View(sl);
           
            
        }
        public ActionResult approve(string e)
        {
            String que = "INSERT INTO testimony (name, category, testimony, filepath) SELECT * FROM testi_check WHERE testimony='" + e+"'";
            obj.InsertUpdateDelete(que);
           
            String dt = "DELETE FROM testi_check WHERE testimony= '" + e + "'";
            obj.InsertUpdateDelete(dt);
            return RedirectToAction("TestiDisplay");
        }
        public ActionResult Detailstes(string dt)
        {

            testimonial tes = new testimonial();


            obj.OpenCon();
            string q = "Select * from testi_check where testimony ='" + dt + "' ";
            obj.cmd = new SqlCommand(q, obj.con);
            SqlDataReader sdr = obj.cmd.ExecuteReader();
            sdr.Read();
            tes.Name = sdr[0].ToString();
          
            tes.Testimonial_Category = sdr[1].ToString();
            tes.Testimony = sdr[2].ToString();
            tes.path = sdr[3].ToString();
            sdr.Close();
            obj.CloseCon();

            return View(tes);
        }
     
        public ActionResult Deletetes(string d)
        {
           testimonial tes=new testimonial();
            
           
            return View(tes.decline(d));
       
        }
     
        [HttpPost]
        public ActionResult Deletetes(testimonial tes)
        {
            String dt = "DELETE FROM testi_check WHERE testimony='" + tes.Testimony + "'";
            obj.InsertUpdateDelete(dt);

            return RedirectToAction("TestiDisplay");
        }
    }
}