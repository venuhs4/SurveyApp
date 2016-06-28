using SurveyApp.Helpers;
using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class BaseController : Controller
    {
        public static ApplicationUser GetLoggedInUser()
        {
            return null;
        }

    }
}