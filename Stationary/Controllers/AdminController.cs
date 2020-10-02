using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stationary.Models;
using System.Data.Entity;
namespace Stationary.Controllers
{
    public class AdminController : Controller
    {
        web_stationaryEntities db = new web_stationaryEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Dashboard()
        {
            var data = db.tbl_stationaryRequest.Where(obj => obj.req_status == "Pending").ToList();
            return View(data);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Admin");
        }

        public ActionResult ChangePassword()
        {

       

            
            return View();
        }
        [HttpPost]

        public ActionResult ChangePassword(Change  obj)
        {

            int id = Convert.ToInt16(Session["userid"]);

            var data = db.tbl_user.Where(obj1 => obj1.u_id == id && obj1.u_pass == obj.old).FirstOrDefault();

            if (data != null)
            {
                if (obj.newpass == obj.confirmpass)
                {

                    data.u_pass = obj.confirmpass;
                   int mess =db.SaveChanges();
                   if (mess > 0)
                   {
                       ViewBag.error = "Your password has been updated successfully";
                   }
                   else {
                       ViewBag.error = "Your password has not  been updated successfully";
                   }
                }
                else
                {
                    ViewBag.error = "Your new Password and confirm password are Not same";
                }

            }
            else {

                ViewBag.error = "Your old password is not exist";
            }


            return View();
        }

        

        [HttpPost]
        public ActionResult Login(tbl_user obj)
        {
            var data=db.tbl_user.Include(obj1 => obj1.tbl_role).Where(obj1 => obj1.u_email == obj.u_email && obj1.u_pass == obj.u_pass).FirstOrDefault();

            if (data != null)
            {

               Session["type"]=data.tbl_role.r_type;
               Session["userid"] = data.u_id;
               Session["useremail"] = data.u_email;

               return RedirectToAction("Dashboard","Admin");

            }
            else {
                ViewBag.error ="Username and password is incorrect";
            }

            
            return View();
        }
    }
}