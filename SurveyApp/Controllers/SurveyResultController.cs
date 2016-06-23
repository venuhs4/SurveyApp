using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class SurveyResultController : BaseController
    {
        // GET: SurveyResult
        public ActionResult Index()
        {
            return View();
        }
    }
}