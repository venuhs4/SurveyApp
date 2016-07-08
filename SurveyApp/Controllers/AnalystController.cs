using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        // GET: Analyst
        [Authorize(Roles = "Analyst,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Client,Admin")]
        public ActionResult ConnectToAnalyst()
        {
            return View();
        }

        public JsonResult GetAllAnalystSurveyTypes(long id)
        {
            var surveyTypes = db.TAnalystSurveyType.Select(s => new { s.AnalystSurveyTypeId, s.AnalystTypeName, s.Description }).ToList();
            var analystSurveys = (from ast in db.TAnalystSurveyType
                                  join ss in db.TAnalystSurvey on ast.AnalystSurveyTypeId equals ss.AnalystSurveyTypeId
                                  select new
                                  {
                                      ast.AnalystSurveyTypeId,
                                      ss.AnalystSurveyId,
                                      ss.ClientId,
                                      ss.Created,
                                      ss.Description,
                                      ss.InternalNotes,
                                      ss.Response
                                  }).ToList();
            return Json(new { surveyTypes, analystSurveys }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetAllUsers()
        //{
        //    if (UserManager.GetRoles(User.Identity.Name).Contains("Analyst") || UserManager.GetRoles(User.Identity.Name).Contains("Admin"))
        //        return Json(db.Users.Select(s => s.UserName).ToList().Where(t => UserManager.GetRoles(User.Identity.Name) == ), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetAllClients()
        {
            List<string> clients = new List<string>();
            foreach (var item in db.Users.Select(s => new { s.Email, s.Id }).ToList())
            {
                var roles = UserManager.GetRoles(item.Id);
                if (roles.Contains("Client"))
                {
                    clients.Add(item.Email);
                }
            }
            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllAnalysts()
        {
            return Json(db.Users.Select(s =>new { s.Email, s.Id }).ToList().Where(t => UserManager.GetRoles(t.Id).Contains("Analyst")).Select(s=> s.Email).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}