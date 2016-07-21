using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class AdminController : BaseController
    {
        ApplicationDbContext context;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        public AdminController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult ManageAnalyst()
        {

            return View();
        }
        public ActionResult ManageClient()
        {
            return View();
        }
        public JsonResult GetAllAnalyst()
        {
            var analysts = context.Users.ToList().Where(u => UserManager.GetRoles(u.Id).Contains("Analyst")).Select(s => new { s.Id, s.UserName }).ToList();
            return Json(analysts, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAnalyst(string id, List<SurveyClient> clients)
        {
            if (id == null)
            {
                return Json(new { Status = 1, Message = "Invalid user!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var user = UserManager.FindById(id);
                var logins = user.Logins;
                var rolesForUser = UserManager.GetRoles(id);

                // changing the client's analyst
                foreach (var client in clients)
                {
                    var _client = context.TSurveyClient.Where(w => w.UserId == client.UserId).FirstOrDefault();
                    if (_client != null)
                    {
                        if (client.AnalystId == null)
                        {
                            var _analystList = context.Users.ToList().Where(u => UserManager.GetRoles(u.Id).Contains("Analyst") && u.Id != id).Select(s => s.UserName).ToList();
                            client.AnalystId = _analystList.Count > 0 ? _analystList[new Random().Next(_analystList.Count)] : "";
                        }
                        _client.AnalystId = client.AnalystId;
                        context.Entry(_client).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            var result = UserManager.RemoveFromRole(user.Id, item);
                        }
                    }

                    UserManager.Delete(user);
                    transaction.Commit();
                }
                return Json(new { Status = 0, Message = "Analyst deleted.", Analysts = context.Users.ToList().Where(u => UserManager.GetRoles(u.Id).Contains("Analyst")).Select(s => new { s.Id, s.UserName }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = 1, Message = e.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetAllClient()
        {
            var clients = (from sc in context.TSurveyClient
                           join u in context.Users on sc.UserId equals u.Id
                           select new { u.Id,u.UserName, sc.AnalystId }).ToList();

            var analysts = context.Users.ToList().Where(u => UserManager.GetRoles(u.Id).Contains("Analyst")).Select(s => new { s.Id, s.UserName }).ToList();
            return Json(new { clients, analysts }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteClient(string id)
        {
            if (id == null)
            {
                return Json(new { Status = 1, Message = "Invalid user!" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var user = UserManager.FindById(id);
                var logins = user.Logins;
                var rolesForUser = UserManager.GetRoles(id);

                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var login in logins.ToList())
                    {
                        UserManager.RemoveLogin(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = UserManager.RemoveFromRole(user.Id, item);
                        }
                    }

                    UserManager.Delete(user);
                    transaction.Commit();
                }
                var clients = (from sc in context.TSurveyClient
                               join u in context.Users on sc.UserId equals u.Id
                               select new { u.Id, u.UserName, sc.AnalystId }).ToList();
                return Json(new { Status = 0, Message = "Client deleted.", clients = clients }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = 1, Message = e.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetClientForAnalyst(string id)
        {
            var clients = (from sc in context.TSurveyClient
                           join u in context.Users on sc.UserId equals u.Id
                           join ua in context.Users on sc.AnalystId equals ua.Email
                           where ua.Id == id
                           select new { u.UserName, u.Email, UserId = u.Id }).ToList();
            return Json(clients , JsonRequestBehavior.AllowGet);
        }


        public JsonResult AnalystChanged(string UserId, string AnalystId)
        {
            var _client = context.TSurveyClient.Where(w => w.UserId == UserId).FirstOrDefault();
            if (_client != null)
            {
                if (AnalystId == null)
                {
                    var _analystList = context.Users.ToList().Where(u => UserManager.GetRoles(u.Id).Contains("Analyst")).Select(s => s.UserName).ToList();
                    AnalystId = _analystList.Count > 0 ? _analystList[new Random().Next(_analystList.Count)] : "";
                }
                _client.AnalystId = AnalystId;
                context.Entry(_client).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            return Json(new { Message="Analyst updated."}, JsonRequestBehavior.AllowGet);
        }
        
    }
}