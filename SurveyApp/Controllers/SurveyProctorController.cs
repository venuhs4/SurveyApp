using SurveyApp.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult TakeSurvey()
        {
            return View();
        }
        public JsonResult GetSurveyDetails()
        {
            var surveyDetails = (from sd in db.TSurveyDetail
                                 join s in db.TSurvey on sd.SurveyId equals s.SurveyId
                                 where s.SurveyId == 1
                                 orderby sd.SortingIndex
                                 select new {sd.SurveyDetailId, sd.Type, sd.Prompt, sd.DelimitedItemList }).ToList();

            db.TSurveyResult.Add(new SurveyResult() {
                DateCreated = DateTime.Now,
                
            });

            return Json(new{ surveyAnswerId = 0, surveyId = 1, surveyDetails },JsonRequestBehavior.AllowGet);
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
            var sr = new SurveyResult()
            {
                DateCreated = DateTime.Now,
                EndTime = DateTime.Now,
                StartTime = DateTime.Now,
                SurveyId = surveyId
            };

            db.TSurveyResult.Add(sr);
            db.SaveChanges();
            list.ForEach((e) => { e.SurveyResultId = sr.SurveyResultId; });
            db.TSurveyAnswer.AddRange(list);
            db.SaveChanges();
            return Json("Survey Answers saved", JsonRequestBehavior.AllowGet);
        }
    }
}