using Microsoft.AspNet.Identity.Owin;
using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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
        public JsonResult GetAllAnalyst()
        {
            //var list = context.Users.ToList().Where(x => User.is
            //UserManager.get
            //var users = context.Users.ToList();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}