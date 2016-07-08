using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SurveyApp.Controllers
{
    [Authorize]
    public class SurveyProctorController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: SurveyProctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectSurvey()
        {
            ViewBag.AllSurvey = db.TSurvey.Select(n => new SelectListItem { Text = n.Title, Value = n.SurveyId.ToString() }).ToList();
            return View();
        }
        public ActionResult TakeSurvey(long id)
        {
            ViewBag.id = id;
            return View();
        }
        public JsonResult GetSurveyDetails(long? id)
        {
            var clientId = (from c in db.TSurveyClient
                            join u in db.Users on c.UserId equals u.Id
                            where u.Email == User.Identity.Name
                            select c.SurveyClientId).ToList().FirstOrDefault();

            var surveyDetails = (from sd in db.TSurveyDetail
                                 join s in db.TSurvey on sd.SurveyId equals s.SurveyId
                                 join j in (from r in db.TSurveyResult
                                            join a in db.TSurveyAnswer on r.SurveyResultId equals a.SurveyResultId
                                            where r.SurveyClientId == clientId && r.SurveyId == id
                                            select a) on sd.SurveyDetailId equals j.SurveyDetailId into tl
                                 from subl in tl.DefaultIfEmpty()
                                 where s.SurveyId == id
                                 orderby sd.SortingIndex
                                 select new { sd.SurveyDetailId, sd.Type, sd.Prompt, sd.DelimitedItemList, ans = subl == null ? "" : subl.SurveyDetailAnswer, SurveyAnswerId = subl == null ? 0 : subl.SurveyAnswerId }).ToList();

            return Json(new { surveyAnswerId = 0, surveyId = id, surveyDetails }, JsonRequestBehavior.AllowGet);
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

        public JsonResult SaveSurveyResult(List<SurveyAnswer> list, long surveyId)
        {

            var clientId = (from c in db.TSurveyClient
                            join u in db.Users on c.UserId equals u.Id
                            where u.Email == User.Identity.Name
                            select c.SurveyClientId).ToList().FirstOrDefault();

            if (db.TSurveyResult.Any(a => a.SurveyClientId == clientId && a.SurveyId == surveyId))
            {
                var oldAnswers = from a in db.TSurveyAnswer
                                 join r in db.TSurveyResult on a.SurveyResultId equals r.SurveyResultId
                                 where r.SurveyClientId == clientId && r.SurveyId == surveyId
                                 select a;

                foreach (var ans in oldAnswers)
                {
                    var newAns = list.Where(l => l.SurveyAnswerId == ans.SurveyAnswerId).FirstOrDefault();
                    if (newAns != null)
                    {
                        ans.SurveyDetailAnswer = newAns.SurveyDetailAnswer;
                        db.Entry(ans).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
            }
            else
            {
                var sr = new SurveyResult()
                {
                    DateCreated = DateTime.Now,
                    EndTime = DateTime.Now,
                    StartTime = DateTime.Now,
                    SurveyId = surveyId,
                    SurveyClientId = clientId
                };

                db.TSurveyResult.Add(sr);
                db.SaveChanges();
                list.ForEach((e) => { e.SurveyResultId = sr.SurveyResultId; });
                db.TSurveyAnswer.AddRange(list);
                db.SaveChanges();
            }
            return Json("Survey Answers saved", JsonRequestBehavior.AllowGet);
        }
    }
}