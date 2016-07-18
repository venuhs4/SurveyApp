using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
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
            var clients = (from c in db.TSurveyClient
                           join u in db.Users on c.UserId equals u.Id
                           where c.AnalystId == User.Identity.Name
                           select new { c.SurveyClientId, u.Email }).ToList();

            Dictionary<long, string> clientsDict = new Dictionary<long, string>();
            clients.ForEach((e) =>
            {
                clientsDict.Add(e.SurveyClientId, e.Email);
            });

            ViewBag.clients = clientsDict;
            return View();
        }

        [Authorize(Roles = "Analyst,Admin")]
        public ActionResult ClientModuleResponse(long client)
        {
            return View(db.TSurveyClient.Where(t=> t.SurveyClientId == client).FirstOrDefault());
        }

        public JsonResult GetClientModule(long client)
        {
            var output = (from ast in db.TAnalystSurveyType
                          join d in (from aas in db.TAnalystSurvey
                                     join sc in db.TSurveyClient on aas.ClientId equals sc.SurveyClientId
                                     where sc.SurveyClientId == client
                                     select aas) on ast.AnalystSurveyTypeId equals d.AnalystSurveyTypeId into allJoin
                          from a in allJoin.DefaultIfEmpty()
                          select new
                          {
                              ast.AnalystSurveyTypeId,
                              ast.AnalystTypeName,
                              ast.Description,
                              ClientId = client,
                              Response = a == null ? "" : a.Response,
                              InternalNotes = a == null ? "" : a.InternalNotes,
                              AnalystSurveyId = a == null ? 0 : a.AnalystSurveyId
                          }).ToList();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAnalystSurvey(List<AnalystSurvey> surveyModules)
        {
            surveyModules.ForEach((e)=> {
                if(e.AnalystSurveyId == 0 )
                {
                    e.Created = DateTime.Now;
                    db.TAnalystSurvey.Add(e);
                    db.SaveChanges();
                }
                else
                {
                    var item = db.TAnalystSurvey.Where(w => w.AnalystSurveyId == e.AnalystSurveyId).FirstOrDefault();
                    item.InternalNotes = e.InternalNotes;
                    item.Response = e.Response;
                    db.Entry(item);
                    db.SaveChanges();
                }
            });
            return Json(surveyModules, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Client,Admin")]
        public ActionResult AnalysisSurvey()
        {
            ViewBag.client = (from u in db.Users
                              join c in db.TSurveyClient on u.Id equals c.UserId
                              where u.UserName == User.Identity.Name
                              select c.SurveyClientId).FirstOrDefault();
            return View();
        }

        //public JsonResult GetAllAnalystSurveyTypes(string username)
        //{
        //    var roles = UserManager.GetRoles(db.Users.Where(w => w.UserName == (username == null ? User.Identity.Name:username)).FirstOrDefault().Id);

        //    if (roles.Contains("Client"))
        //    {
        //        var output = (from ast in db.TAnalystSurveyType
        //                   join d in (from aas in db.TAnalystSurvey
        //                              join sc in db.TSurveyClient on aas.ClientId equals sc.SurveyClientId
        //                              join u in db.Users on sc.AnalystId equals u.Id
        //                              where u.Email == User.Identity.Name
        //                              select aas) on ast.AnalystSurveyTypeId equals d.AnalystSurveyTypeId into allJoin
        //                   from a in allJoin.DefaultIfEmpty()
        //                   select new
        //                   {
        //                       ast.AnalystSurveyTypeId,
        //                       ast.AnalystTypeName,
        //                       ast.Description,
        //                       Response = a == null? "" :a.Response,
        //                       InternalNotes = a == null ? "" : a.InternalNotes,
        //                       AnalystSurveyId = a == null ? 0 : a.AnalystSurveyId
        //                   }).ToList();
        //        return Json(output, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        var output = (from ast in db.TAnalystSurveyType
        //                      join d in (from aas in db.TAnalystSurvey
        //                                 join sc in db.TSurveyClient on aas.ClientId equals sc.SurveyClientId
        //                                 join u in db.Users on sc.UserId equals u.Id
        //                                 where u.Email == User.Identity.Name
        //                                 select aas) on ast.AnalystSurveyTypeId equals d.AnalystSurveyTypeId into allJoin
        //                      from a in allJoin.DefaultIfEmpty()
        //                      select new
        //                      {
        //                          ast.AnalystSurveyTypeId,
        //                          ast.AnalystTypeName,
        //                          ast.Description,
        //                          Response = a == null ? "" : a.Response,
        //                          InternalNotes = a == null ? "" : a.InternalNotes,
        //                          AnalystSurveyId = a == null ? 0 : a.AnalystSurveyId
        //                      }).ToList();
        //        return Json(output, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public JsonResult SaveAnalystSurvey(List<AnalystSurvey> data, string client)
        //{

        //    foreach (var item in data)
        //    {
        //        if(item.AnalystSurveyId == 0 )
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }

        //    return Json("Saved", JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllClients()
        //{
        //    List<string> clients = new List<string>();
        //    foreach (var item in db.Users.Select(s => new { s.Email, s.Id }).ToList())
        //    {
        //        var roles = UserManager.GetRoles(item.Id);
        //        if (roles.Contains("Client"))
        //        {
        //            clients.Add(item.Email);
        //        }
        //    }
        //    return Json(clients, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetAllAnalysts()
        //{
        //    return Json(db.Users.Select(s => new { s.Email, s.Id }).ToList().Where(t => UserManager.GetRoles(t.Id).Contains("Analyst")).Select(s => s.Email).ToList(), JsonRequestBehavior.AllowGet);
        //}

        //public class ChangeData
        //{

        //}
    }
}