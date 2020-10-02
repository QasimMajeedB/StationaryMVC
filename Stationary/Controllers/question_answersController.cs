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
    public class question_answersController : Controller
    {
        private web_stationaryEntities db = new web_stationaryEntities();

        // GET: question_answers
        public ActionResult Index()
        {
            return View(db.question_answers.ToList());
        }

        // GET: question_answers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question_answers question_answers = db.question_answers.Find(id);
            if (question_answers == null)
            {
                return HttpNotFound();
            }
            return View(question_answers);
        }

        // GET: question_answers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: question_answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,messages,froms,tos,dates")] question_answers question_answers)
        {
            if (ModelState.IsValid)
            {
                db.question_answers.Add(question_answers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question_answers);
        }

        // GET: question_answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question_answers question_answers = db.question_answers.Find(id);
            if (question_answers == null)
            {
                return HttpNotFound();
            }
            return View(question_answers);
        }

        // POST: question_answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,messages,froms,tos,dates")] question_answers question_answers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question_answers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question_answers);
        }

        // GET: question_answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            question_answers question_answers = db.question_answers.Find(id);
            if (question_answers == null)
            {
                return HttpNotFound();
            }
            return View(question_answers);
        }

        // POST: question_answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            question_answers question_answers = db.question_answers.Find(id);
            db.question_answers.Remove(question_answers);
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
