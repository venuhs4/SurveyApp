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
    public class SurveyGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SurveyGroups
        public ActionResult Index()
        {
            return View(db.TSurveyGroup.ToList());
        }

        public ActionResult CreateAll()
        {
            return View();
        }

        // GET: SurveyGroups/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyGroup surveyGroup = db.TSurveyGroup.Find(id);
            if (surveyGroup == null)
            {
                return HttpNotFound();
            }
            return View(surveyGroup);
        }

        // GET: SurveyGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SurveyGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyGroupId,SurveyGroupName")] SurveyGroup surveyGroup)
        {
            if (ModelState.IsValid)
            {
                db.TSurveyGroup.Add(surveyGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(surveyGroup);
        }

        // GET: SurveyGroups/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyGroup surveyGroup = db.TSurveyGroup.Find(id);
            if (surveyGroup == null)
            {
                return HttpNotFound();
            }
            return View(surveyGroup);
        }

        // POST: SurveyGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyGroupId,SurveyGroupName")] SurveyGroup surveyGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveyGroup);
        }

        // GET: SurveyGroups/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyGroup surveyGroup = db.TSurveyGroup.Find(id);
            if (surveyGroup == null)
            {
                return HttpNotFound();
            }
            return View(surveyGroup);
        }

        // POST: SurveyGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SurveyGroup surveyGroup = db.TSurveyGroup.Find(id);
            db.TSurveyGroup.Remove(surveyGroup);
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
