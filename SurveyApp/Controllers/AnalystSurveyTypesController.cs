using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyApp.Models;

namespace SurveyApp.Controllers
{
    public class AnalystSurveyTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnalystSurveyTypes
        public ActionResult Index()
        {
            return View(db.TAnalystSurveyType.ToList());
        }

        // GET: AnalystSurveyTypes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalystSurveyType analystSurveyType = db.TAnalystSurveyType.Find(id);
            if (analystSurveyType == null)
            {
                return HttpNotFound();
            }
            return View(analystSurveyType);
        }

        // GET: AnalystSurveyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalystSurveyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnalystSurveyTypeId,AnalystTypeName,Description")] AnalystSurveyType analystSurveyType)
        {
            if (ModelState.IsValid)
            {
                db.TAnalystSurveyType.Add(analystSurveyType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(analystSurveyType);
        }

        // GET: AnalystSurveyTypes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalystSurveyType analystSurveyType = db.TAnalystSurveyType.Find(id);
            if (analystSurveyType == null)
            {
                return HttpNotFound();
            }
            return View(analystSurveyType);
        }

        // POST: AnalystSurveyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnalystSurveyTypeId,AnalystTypeName,Description")] AnalystSurveyType analystSurveyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(analystSurveyType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(analystSurveyType);
        }

        // GET: AnalystSurveyTypes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnalystSurveyType analystSurveyType = db.TAnalystSurveyType.Find(id);
            if (analystSurveyType == null)
            {
                return HttpNotFound();
            }
            return View(analystSurveyType);
        }

        // POST: AnalystSurveyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AnalystSurveyType analystSurveyType = db.TAnalystSurveyType.Find(id);
            db.TAnalystSurveyType.Remove(analystSurveyType);
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
