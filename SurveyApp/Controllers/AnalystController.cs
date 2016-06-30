using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class AnalystController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Analyst
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllAnalystSurveyTypes(long id)
        {
            var surveyTypes = db.TAnalystSurveyType.Select(s => new { s.AnalystSurveyTypeId, s.AnalystTypeName, s.Description }).ToList();
            var analystSurveys = (from ast in db.TAnalystSurveyType
                                join ss in db.TAnalystSurvey on ast.AnalystSurveyTypeId equals ss.AnalystSurveyTypeId
                                select new { ast.AnalystSurveyTypeId,
                                    ss.AnalystSurveyId, ss.ClientId, ss.Created, ss.Description, ss.InternalNotes, ss.Response
                                    }).ToList();
            return Json(new { surveyTypes, analystSurveys }, JsonRequestBehavior.AllowGet);
        }
    }
}