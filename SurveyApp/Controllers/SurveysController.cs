using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyApp.Models;
using Newtonsoft.Json;

namespace SurveyApp.Controllers
{
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            return View(db.TSurvey.ToList());
        }

        public ActionResult AllDetails()
        {
            ViewBag.ServeyTypes = SurveyDetail.GetServeyTypes();
            return View();
        }

        public JsonResult GetServeyTypes()
        {
            return Json(SurveyDetail.GetServeyTypes().Select(s=> new { Id = s.Key, Name = s.Value }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSurveyDetails(long id)
        {
            var list = (from d in db.TSurveyDetail
                       where d.SurveyId == id
                       select new {
                           d.SurveyId,
                           d.SurveyDetailId,
                           d.Type,
                           d.DependentItemID,
                           d.Dependancy,
                           d.Dependent,
                           d.DelimitedItemList,
                           d.Prompt
                       }).ToList();
            return Json(list , JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSurveyDetail(SurveyDetail detail)
        {
            db.TSurveyDetail.Add(detail);
            db.SaveChanges();
            return Json("Added", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllSurveyGroups()
        {
            var groups = (from g in db.TSurveyGroup
                          select new { Name = g.SurveyGroupName, Id = g.SurveyGroupId }).ToList();
            return Json(groups, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSurveyByGroup(long id)
        {
            if (id == 0)
            {
                var groups = (from s in db.TSurvey
                              select new { Name = s.Title, Id = s.SurveyId }).ToList();
                return Json(groups, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var groups = (from s in db.TSurvey
                              join m in db.TSurveyGroupMap on s.SurveyId equals m.SurveyId
                              where m.SurveyGroupId == id
                              select new { Name = s.Title, Id = s.SurveyId }).ToList();
                return Json(groups, JsonRequestBehavior.AllowGet);
            }
        }



        // GET: Surveys/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.TSurvey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyId,Duty,Title,Description,LinkId")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.TSurvey.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.TSurvey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyId,Duty,Title,Description,LinkId")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.TSurvey.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Survey survey = db.TSurvey.Find(id);
            db.TSurvey.Remove(survey);
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
