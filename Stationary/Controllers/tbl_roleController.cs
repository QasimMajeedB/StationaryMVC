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
    public class tbl_roleController : Controller
    {
        private web_stationaryEntities db = new web_stationaryEntities();

        // GET: tbl_role
        public ActionResult Index()
        {
            return View(db.tbl_role.ToList());
        }

        // GET: tbl_role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_role tbl_role = db.tbl_role.Find(id);
            if (tbl_role == null)
            {
                return HttpNotFound();
            }
            return View(tbl_role);
        }

        // GET: tbl_role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "r_id,r_type")] tbl_role tbl_role)
        {
            if (ModelState.IsValid)
            {
                db.tbl_role.Add(tbl_role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_role);
        }

        // GET: tbl_role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_role tbl_role = db.tbl_role.Find(id);
            if (tbl_role == null)
            {
                return HttpNotFound();
            }
            return View(tbl_role);
        }

        // POST: tbl_role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "r_id,r_type")] tbl_role tbl_role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_role);
        }

        // GET: tbl_role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_role tbl_role = db.tbl_role.Find(id);
            if (tbl_role == null)
            {
                return HttpNotFound();
            }
            return View(tbl_role);
        }

        // POST: tbl_role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_role tbl_role = db.tbl_role.Find(id);
            db.tbl_role.Remove(tbl_role);
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
