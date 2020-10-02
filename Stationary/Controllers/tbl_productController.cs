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
    public class tbl_productController : Controller
    {
        private web_stationaryEntities db = new web_stationaryEntities();

        // GET: tbl_product
        public ActionResult Index()
        {
            return View(db.tbl_product.ToList());
        }

        // GET: tbl_product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // GET: tbl_product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,p_name,p_price,p_qty,p_date,p_des")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.tbl_product.Add(tbl_product);
                db.SaveChanges();
                db.tbl_stock.Add(new tbl_stock() {s_name=tbl_product.p_name,s_price=tbl_product.p_price,s_qty=tbl_product.p_qty,s_date=tbl_product.p_date,s_des=tbl_product.p_des,s_status="Available"});
                db.SaveChanges();
                db.tbl_notification.Add(new tbl_notification() {not_date=System.DateTime.Now,not_action="Product Created at this Time"});
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_product);
        }

        // GET: tbl_product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // POST: tbl_product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,p_name,p_price,p_qty,p_date,p_des")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_product).State = EntityState.Modified;
                int mess=db.SaveChanges();
                if (mess > 0)
                {
                    db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Product modified at this Time" });
                    db.SaveChanges();
                }
              
                return RedirectToAction("Index");
            }
            return View(tbl_product);
        }

        // GET: tbl_product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // POST: tbl_product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_product tbl_product = db.tbl_product.Find(id);
            db.tbl_product.Remove(tbl_product);
            int mess = db.SaveChanges();
            if (mess > 0)
            {
                db.tbl_notification.Add(new tbl_notification() { not_date = System.DateTime.Now, not_action = "Product Deleted at this Time" });
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
