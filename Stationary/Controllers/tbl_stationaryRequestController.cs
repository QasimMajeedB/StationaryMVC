using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stationary.Models;
using System.Web.Helpers;

namespace Stationary.Controllers
{
    public class tbl_stationaryRequestController : Controller
    {
        private web_stationaryEntities db = new web_stationaryEntities();

        // GET: tbl_stationaryRequest
        public ActionResult Index()
        {
            var tbl_stationaryRequest = db.tbl_stock.ToList();

            return View(tbl_stationaryRequest);
        }
        public ActionResult Stationary_Request(String name,double price,double qty,double total,int sid) {

            ViewBag.name = name;
            ViewBag.price = price;
            ViewBag.qty = qty;
            ViewBag.total =total;
            ViewBag.sid = sid;
            ViewBag.uid = Session["userid"];
            return View();
        }

        [HttpPost]

        public ActionResult Stationary_Request(String name, double price, double qty, double total, int sid,int uid)
        {
            String from_email = Session["useremail"].ToString();
            var data=db.tbl_stock.Where(obj=>obj.s_id==sid).FirstOrDefault();
            var requestcheck = db.tbl_stationaryRequest.Where(obj1 => obj1.user_id == uid && obj1.req_status == "Pending").FirstOrDefault();
            if (requestcheck != null)
            {
                ViewBag.error = "Request Already Send to Superior";
            }
            else
            {
                if (qty<=data.s_qty)
                {
                    if (data.s_qty == 0)
                    {
                        ViewBag.error = "Out of Stock Quantity";
                    }
                    else {
                        db.tbl_stationaryRequest.Add(new tbl_stationaryRequest() { item_name = name, req_qty = qty, req_totalprice = total, s_id = sid, user_id = uid, request_date = System.DateTime.Now, req_status = "Pending" });
                        int mess = db.SaveChanges();
                        if (mess > 0)
                        {
                            ViewBag.error = "Request Send Successfully to Admin";
                            db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Stationary Request Send To Admin" });
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

                                ViewBag.e = "Email send";

                                //Send email  
                                WebMail.Send(to: "Khantest786786@gmail.com", subject: "Request Send to admin Successfully", body: "Request Send to admin Successfully", isBodyHtml: true);

                            }
                            catch (Exception)
                            {
                                ViewBag.Status = "Problem while sending email, Please check details.";

                            }


                        }
                        else
                        {
                            ViewBag.error = "Request not  Send Successfully to Superior";
                        }
                    }

                   
                }
                else
                {
                    ViewBag.error = "Out of Stock Quantity";
                }
            }
            return View();
        }




        // GET: tbl_stationaryRequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_stationaryRequest tbl_stationaryRequest = db.tbl_stationaryRequest.Find(id);
            if (tbl_stationaryRequest == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stationaryRequest);
        }

        // GET: tbl_stationaryRequest/Create
        public ActionResult Create()
        {
            ViewBag.r_id = new SelectList(db.tbl_role, "r_id", "r_type");
            ViewBag.s_id = new SelectList(db.tbl_stock, "s_id", "s_name");
            return View();
        }

        // POST: tbl_stationaryRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "st_reqid,s_id,r_id,req_qty,req_totalprice,request_date,req_status")] tbl_stationaryRequest tbl_stationaryRequest)
        {
            if (ModelState.IsValid)
            {
                db.tbl_stationaryRequest.Add(tbl_stationaryRequest);
                int mess = db.SaveChanges();
                if (mess > 0)
                {
                    db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Stationary Request Created at this Time" });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.r_id = new SelectList(db.tbl_role, "u_id", "u_name", tbl_stationaryRequest.user_id);
            ViewBag.s_id = new SelectList(db.tbl_stock, "s_id", "s_name", tbl_stationaryRequest.s_id);
            return View(tbl_stationaryRequest);
        }

        // GET: tbl_stationaryRequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_stationaryRequest tbl_stationaryRequest = db.tbl_stationaryRequest.Find(id);
            if (tbl_stationaryRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.r_id = new SelectList(db.tbl_role, "user_id", "r_type", tbl_stationaryRequest.user_id);
            ViewBag.s_id = new SelectList(db.tbl_stock, "s_id", "s_name", tbl_stationaryRequest.s_id);
            return View(tbl_stationaryRequest);
        }

        // POST: tbl_stationaryRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "st_reqid,s_id,r_id,req_qty,req_totalprice,request_date,req_status")] tbl_stationaryRequest tbl_stationaryRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_stationaryRequest).State = EntityState.Modified;
                int mess = db.SaveChanges();
                if (mess > 0)
                {
                    db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Stationary Request modified at this Time" });
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.r_id = new SelectList(db.tbl_role, "r_id", "r_type", tbl_stationaryRequest.user_id);
            ViewBag.s_id = new SelectList(db.tbl_stock, "s_id", "s_name", tbl_stationaryRequest.s_id);
            return View(tbl_stationaryRequest);
        }

        // GET: tbl_stationaryRequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_stationaryRequest tbl_stationaryRequest = db.tbl_stationaryRequest.Find(id);
            if (tbl_stationaryRequest == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stationaryRequest);
        }

        // POST: tbl_stationaryRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_stationaryRequest tbl_stationaryRequest = db.tbl_stationaryRequest.Find(id);
            db.tbl_stationaryRequest.Remove(tbl_stationaryRequest);
            int mess = db.SaveChanges();
            if (mess > 0)
            {
                db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Stationary Request Deleted at this Time" });
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
