using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stationary.Models;
using System.Web.Helpers;
namespace Stationary.Controllers
{
    public class ApprovedCancelRequestController : Controller
    {
        web_stationaryEntities db = new web_stationaryEntities();
        // GET: ApprovedCancelRequest
        public ActionResult Index()
        {

            var data= db.tbl_stationaryRequest.Where(obj => obj.req_status == "Pending").ToList();
            return View(data);
        }

        public ActionResult Approved(int uid,int sid,int qty)
        {
           String from_email=Session["useremail"].ToString();
          var request=db.tbl_stationaryRequest.Where(obj => obj.st_reqid == uid).FirstOrDefault();
          request.approvecancel_date = System.DateTime.Now;
          request.req_status = "Approved";
          db.SaveChanges();
          var stock = db.tbl_stock.Where(obj1=>obj1.s_id == sid).FirstOrDefault();
        
         
              stock.s_qty -= qty;
              db.SaveChanges();
            if(stock.s_qty==0){
                stock.s_status = "NotAvailable";
                db.SaveChanges();

            }

            db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Admin Approved Employee Request  at this Time" });
            db.SaveChanges();
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "Khantest786786@gmail.com";
                WebMail.Password = "B2atm8!2345";

                //Sender email address.  
                WebMail.From = from_email;

                //Send email  
                WebMail.Send(to: "Khantest786786@gmail.com", subject: "Admin Request Approved Successfully", body: "Request Approved Successfully", isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }

          

         
            return RedirectToAction("Index","ApprovedCancelRequest");
        }

        public ActionResult Cancel(int uid, int sid,int qty)
        {
            String from_email = Session["useremail"].ToString();                   
            var request = db.tbl_stationaryRequest.Where(obj => obj.st_reqid == uid).FirstOrDefault();
            request.approvecancel_date = System.DateTime.Now;
            request.req_status = "Cancel";
            db.SaveChanges();
            db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Admin Cancel Employee Request  at this Time" });
            db.SaveChanges();
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = "Khantest786786@gmail.com";
                WebMail.Password = "B2atm8!2345";

                //Sender email address.  
                WebMail.From = from_email;

                //Send email  
                WebMail.Send(to: "Khantest786786@gmail.com", subject: "Admin Cancel Request Successfully", body: "Cancel Request Successfully", isBodyHtml: true);
               
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }
            return RedirectToAction("Index", "ApprovedCancelRequest");
        }

    }
}