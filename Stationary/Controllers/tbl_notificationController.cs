using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stationary.Models;

namespace Stationary.Controllers
{
    public class tbl_notificationController : Controller
    {
        private web_stationaryEntities db = new web_stationaryEntities();

        // GET: tbl_notification
        public ActionResult Index()
        {
            return View(db.tbl_notification.ToList());
        }

        // GET: tbl_notification/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_notification tbl_notification = db.tbl_notification.Find(id);
            if (tbl_notification == null)
            {
                return HttpNotFound();
            }
            return View(tbl_notification);
        }

        // GET: tbl_notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_notification/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "not_id,not_action,not_date")] tbl_notification tbl_notification)
        {
            if (ModelState.IsValid)
            {
                db.tbl_notification.Add(tbl_notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_notification);
        }

        // GET: tbl_notification/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_notification tbl_notification = db.tbl_notification.Find(id);
            if (tbl_notification == null)
            {
                return HttpNotFound();
            }
            return View(tbl_notification);
        }

        // POST: tbl_notification/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "not_id,not_action,not_date")] tbl_notification tbl_notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_notification);
        }

        // GET: tbl_notification/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_notification tbl_notification = db.tbl_notification.Find(id);
            if (tbl_notification == null)
            {
                return HttpNotFound();
            }
            return View(tbl_notification);
        }

        // POST: tbl_notification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_notification tbl_notification = db.tbl_notification.Find(id);
            db.tbl_notification.Remove(tbl_notification);
            db.SaveChanges();
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
