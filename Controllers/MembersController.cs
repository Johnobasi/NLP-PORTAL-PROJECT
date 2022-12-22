using FIVERR_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace FIVERR_PROJECT.Controllers
{
    public class MembersController : Controller
    {
       
        DBAaccess obj = new DBAaccess();
        public ActionResult Dashboard()
        {
            string que = "select count(*) from testimony";
            int ttesti = obj.getdata(que);
            ViewData["ttest"] = ttesti;
            string qu = "select count(*) from signup";
            int member = obj.getdata(qu);
            ViewBag.member=member;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Signup s)
        {

           

            Signup sign = new Signup();
           bool result = sign.LoginCheck(s.UserName, s.Password);

            if (result)
            {
                Response.Write("<script type = 'text/javascript'>alert('Success Fully Login');</script>");
                return RedirectToAction("Testimonial", "Home");

            }
                 

            bool member = sign.memberCheck(s.UserName, s.Password);
            if (member)
            {
                Response.Write("<script type = 'text/javascript'>alert('Wellcome To Dashboard');</script>");
                return RedirectToAction("Dashboard");
            }
            else
            {

                Response.Write("<script>alert('UserName Or Password Is Invalid! ');</script>");

                return RedirectToAction("Login");
            }
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Signup s)
        {
            
                string q = "insert into signup values('" + s.UserName + "','" + s.Password + "','" + s.Email + "')";

                obj.InsertUpdateDelete(q);
            Response.Write("<script>alert('SignUp Successfully  ');</script>");
            return RedirectToAction("Login");
           
           
        }
       


        public ActionResult MEMBER()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MEMBER(Member m)
        {
          
                string q = "insert into member values('" + m.UserName + "','" + m.Password + "','" + m.Email + "')";

                obj.InsertUpdateDelete(q);

            Response.Write("<script>alert('New Memmber Add Successfully  ');</script>");
            return RedirectToAction("Dashboard");
           
           
        }
        public ActionResult Deletemem(String d)
        {

            string qe = "delete from member where username='"+d+"'";

                obj.InsertUpdateDelete(qe);
            Response.Write("<script>alert('Member is Deleted  ');</script>");

            return RedirectToAction("MemberDisplay");
           
           
        }
        public ActionResult Editmem(String ed)
        {

            Member obj = new Member();


            return View(obj.memberDetails(ed));
        }
        [HttpPost]
        public ActionResult Editmem(Member mem)
        {
                String qe = "update member set lpassword='" + mem.Password + "', email='" + mem.Email + "' where username='" + mem.UserName + " ' ";

                obj.InsertUpdateDelete(qe);
            Response.Write("<script>alert('Edit in Member Details Successfully Done ');</script>");
            return RedirectToAction("MemberDisplay");
            
        }
        public ActionResult MemberDisplay()
        {
           
                Member memobj = new Member();
                List<Member> memb = memobj.memberList();
                return View(memb);
           
            
        }
    }
}