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
    public class SurveysController : BaseController
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

        public ActionResult InitialSurvey()
        {
            return View();
        }

        public ActionResult TakeInitialSurvey()
        {
            return View("TakeInitialSurvey", "_TempLayout");
        }

        public ActionResult SecondarySurvey()
        {
            return View();
        }

        public ActionResult TakeSecondarySurvey()
        {
            return View();
        }

        public JsonResult GetInitialSurveyDetails()
        {
            var surveyDetails = (from sd in db.TSurveyDetail
                                 join s in db.TSurvey on sd.SurveyId equals s.SurveyId
                                 where s.SurveyId == 1
                                 orderby sd.SortingIndex
                                 select new { sd.SurveyDetailId, sd.Type, sd.Prompt, sd.DelimitedItemList }).ToList();

            db.TSurveyResult.Add(new SurveyResult()
            {
                DateCreated = DateTime.Now,

            });

            return Json(new { surveyAnswerId = 0, surveyId = 1, surveyDetails }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSurveyAnswers()
        {
            var surveyDetails = (from sd in db.TSurveyDetail
                                 join s in db.TSurvey on sd.SurveyId equals s.SurveyId
                                 where s.SurveyId == 1
                                 orderby sd.SortingIndex
                                 select new { sd.SurveyDetailId, sd.Type, sd.Prompt, sd.DelimitedItemList }).ToList();
            return Json(new { surveyId = 1, surveyDetails }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveInitialSurvey(List<SurveyAnswer> list, string name, string email, string phone)
        {
            var sr = new SurveyResult()
            {
                DateCreated = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                SurveyId = 1
            };

            db.TSurveyResult.Add(sr);
            db.SaveChanges();
            list.ForEach((e) => { e.SurveyResultId = sr.SurveyResultId; });
            db.TSurveyAnswer.AddRange(list);
            db.SaveChanges();
            return Json("Survey Answers saved", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSecondarySurveyDetails()
        {
            var surveyDetails = (from sd in db.TSurveyDetail
                                 join s in db.TSurvey on sd.SurveyId equals s.SurveyId
                                 where s.SurveyId == 2
                                 orderby sd.SortingIndex
                                 select new { sd.SurveyDetailId, sd.Type, sd.Prompt, sd.DelimitedItemList }).ToList();

            db.TSurveyResult.Add(new SurveyResult()
            {
                DateCreated = DateTime.Now,

            });

            return Json(new { surveyAnswerId = 0, surveyId = 2, surveyDetails }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveSecondarySurvey(List<SurveyAnswer> list)
        {
            var sr = new SurveyResult()
            {
                DateCreated = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                SurveyId = 2
            };

            db.TSurveyResult.Add(sr);
            db.SaveChanges();
            list.ForEach((e) => { e.SurveyResultId = sr.SurveyResultId; });
            db.TSurveyAnswer.AddRange(list);
            db.SaveChanges();
            return Json("Survey Answers saved", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetServeyTypes()
        {
            return Json(SurveyDetail.GetServeyTypes().Select(s => new { Id = s.Key, Name = s.Value }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSurveyDetails(long id)
        {
            var list = (from d in db.TSurveyDetail
                        where d.SurveyId == id
                        orderby d.SortingIndex ascending
                        select new
                        {
                            d.SurveyId,
                            d.SurveyDetailId,
                            d.SortingIndex,
                            d.Type,
                            d.DependentItemID,
                            d.Dependancy,
                            d.Dependent,
                            d.DelimitedItemList,
                            d.Prompt
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveSurveyDetail(SurveyDetail detail)
        {
            var d = db.TSurveyDetail.Where(f => f.SurveyDetailId == detail.SurveyDetailId).FirstOrDefault();
            if (d != null)
            {
                db.Entry(d).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return Json("deleted", JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrderIndexUpdate(List<SurveyDetail> surveys)
        {
            foreach (var item in surveys)
            {
                var survey = db.TSurveyDetail.Where(s => s.SurveyDetailId == item.SurveyDetailId).FirstOrDefault();
                if (surveys != null)
                {
                    survey.SortingIndex = item.SortingIndex;
                    db.Entry(survey).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json("Order Index saved", JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddSurveyDetail(SurveyDetail detail, long id)
        {
            db.TSurveyDetail.Add(detail);
            db.SaveChanges();

            var list = (from d in db.TSurveyDetail
                        where d.SurveyId == id
                        orderby d.SortingIndex ascending
                        select new
                        {
                            d.SurveyId,
                            d.SurveyDetailId,
                            d.SortingIndex,
                            d.Type,
                            d.DependentItemID,
                            d.Dependancy,
                            d.Dependent,
                            d.DelimitedItemList,
                            d.Prompt
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
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
