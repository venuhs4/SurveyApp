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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using SurveyApp.Helpers;
using Microsoft.AspNet.Identity;

namespace SurveyApp.Controllers
{
    public class SurveysController : BaseController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        [AllowAnonymous]
        public ActionResult TakeInitialSurvey()
        {
            return View("TakeInitialSurvey", "_TempLayout");
        }

        [AllowAnonymous]
        public ActionResult AfterInitialSurvey() {
            return View();
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TakeInitialSurvey(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                MailHelper.SendMail(model.Email,"Survey App Login details", string.Format(@"Survey app login is initiated from this mail. Please use the following login details for this mail ID.
                    
Username:{0}
Password:{1}

follow the link to head to second survey, http://166.62.35.239/surveyapp/surveys/takesecondarysurvey",model.Email,"Survey@123"));
                var result = await UserManager.CreateAsync(user, "Survey@123");
                if (result.Succeeded)
                {
                    model.Name = "Client";
                    //Assign Role to user Here 
                    await this.UserManager.AddToRoleAsync(user.Id, model.Name);
                    //Ends Here

                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    db.TSurveyClient.Add(new SurveyClient()
                    {
                        UserId = user.Id,
                        Company = model.OrgName,
                    });
                    db.SaveChanges();

                    //return RedirectToAction("TakeSecondarySurvey", "Surveys");
                    return RedirectToAction("AfterInitialSurvey", "Surveys");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeSecondarySurvey(SecondSurvey model)
        {
            if (ModelState.IsValid)
            {
                List<string> analysts = new List<string>();
                foreach (var item in db.Users.Select(s => new { s.Email, s.Id }).ToList())
                {
                    var roles = UserManager.GetRoles(item.Id);
                    if (roles.Contains("Analyst"))
                    {
                        analysts.Add(item.Email);
                    }
                }

                var selectedAnalyst = analysts[new Random().Next(analysts.Count)];

                MailHelper.SendMail(User.Identity.Name, "Welcome from Survey App", string.Format(@"Welcome to survey application...

Please start interacting with the Analyst, follow the link to start, http://166.62.35.239/surveyapp/Analyst/ConnectToAnalyst"));

                

                SurveyClient sc = db.TSurveyClient.Where(d => d.UserId ==  db.Users.Where(u=> u.Email == User.Identity.Name).FirstOrDefault().Id).FirstOrDefault();
                if(sc!=null)
                {
                    sc.Address = model.Address;
                    sc.FirstName = model.FirstName;
                    sc.LastName = model.LastName;
                    sc.Pincode = model.Pincode;
                    sc.AnalystId = selectedAnalyst;
                }
                db.Entry(sc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ConnectToAnalyst", "Analyst");
            }
            return View(model);
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
