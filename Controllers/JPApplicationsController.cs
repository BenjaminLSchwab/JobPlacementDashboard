using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using JobPlacementDashboard.Models;
using Microsoft.AspNet.Identity;

namespace JobPlacementDashboard.Controllers
{
    public class JPApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles ="Admin")]
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.CompanySortParm = sortOrder == "Company" ? "company_desc" : "Company";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.JobTitleSortParm = sortOrder == "Job Title" ? "jobTitle_desc" : "Job Title";
            ViewBag.JobCategorySortParm = sortOrder == "Job Category" ? "jobCategory_desc" : "Job Category";
            ViewBag.CompanyCitySortParm = sortOrder == "Company City" ? "companyCity_desc" : "Company City";
            ViewBag.CompanyStateSortParm = sortOrder == "Company State" ? "companyState_desc" : "Company State";
            ViewBag.ApplicationDateSortParm = sortOrder == "Application Date" ?  "applicationDate_desc" : "applicationDate";

            
            var students = from s in db.JPStudents
                           select s;

            var applications = from s in db.JPApplications.Include(j => j.ApplicationUser)
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                applications = applications.Where(s => s.ApplicationUser.FirstName.Contains(searchString) || s.ApplicationUser.LastName.Contains(searchString)
                || s.JPCompanyName.Contains(searchString) || s.JPJobCategory.ToString().Contains(searchString)
                || s.JPCompanyCity.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Company":
                    applications = applications.OrderBy(s => s.JPCompanyName);
                    break;
                case "company_desc":
                    applications = applications.OrderByDescending(s => s.JPCompanyName);
                    break;
                case "name_desc":
                    applications = applications.OrderByDescending(s => s.ApplicationUser.FirstName);
                    break;
                case "Name":
                    applications = applications.OrderBy(s => s.ApplicationUser.FirstName);
                    break;
                case "Job Title":
                    applications = applications.OrderBy(s => s.JPJobTitle);
                    break;
                case "jobTitle_desc":
                    applications = applications.OrderByDescending(s => s.JPJobTitle);
                    break;
                case "JobCategory":
                    applications = applications.OrderBy(s => s.JPJobCategory);
                    break;
                case "jobCategory_desc":
                    applications = applications.OrderByDescending(s => s.JPJobCategory);
                    break;
                case "Company City":
                    applications = applications.OrderBy(s => s.JPCompanyCity);
                    break;
                case "companyCity_desc":
                    applications = applications.OrderByDescending(s => s.JPCompanyCity);
                    break;
                case "Company State":
                    applications = applications.OrderBy(s => s.JPCompanyState);
                    break;
                case "companyState_desc":
                    applications = applications.OrderByDescending(s => s.JPCompanyState);
                    break;


                default: //Company ascending

                    applications = applications.OrderBy(s => s.JPCompanyName);
                    break;


            }

            return View(applications.ToList());


        }

        public ActionResult StudentIndex(string sortOrder, string searchString)
        {
            //Displaying the active users applications data from JPApplications

            ViewBag.CompanySort = sortOrder == "Company" ? "company_desc" : "Company";
            ViewBag.DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.TitleSort = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.CitySort = sortOrder == "City" ? "city_desc" : "City";
            ViewBag.searchString = searchString;

            string userId = User.Identity.GetUserId();
            var applications = from s in db.JPApplications.Where(x => x.ApplicationUserId == userId)
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                string searchStringNoSpaces = Regex.Replace(searchString, @"\s+", "");
                applications = applications.Where(s => s.JPCompanyName.Contains(searchString) || s.JPCompanyName.Contains(searchStringNoSpaces));
            }

            ViewBag.TotalApplications = applications.Count();
            ViewBag.AppGoal = (applications.Count() < 35) ? (35 - applications.Count()) : 0;

            switch (sortOrder)
            {
                case "Company":
                    applications = applications.OrderBy(s => s.JPCompanyName);
                    break;
                case "company_desc":
                    applications = applications.OrderByDescending(s => s.JPCompanyName);
                    break;
                case "Date":
                    applications = applications.OrderBy(s => s.JPApplicationDate);
                    break;
                case "date_desc":
                    applications = applications.OrderByDescending(s => s.JPApplicationDate);
                    break;
                case "Title":
                    applications = applications.OrderBy(s => s.JPJobTitle);
                    break;
                case "title_desc":
                    applications = applications.OrderByDescending(s => s.JPJobTitle);
                    break;
                case "City":
                    applications = applications.OrderBy(s => s.JPCompanyCity);
                    break;
                case "city_desc":
                    applications = applications.OrderByDescending(s => s.JPCompanyCity);
                    break;
                default:
                    applications = applications.OrderByDescending(s => s.JPApplicationDate);
                    break;
            }

            return View(applications);
        }

        // GET: JPApplications/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPApplication jPApplication = db.JPApplications.Find(id);
            if (jPApplication == null)
            {
                return HttpNotFound();
            }
            return View(jPApplication);
        }

        // GET: JPApplications/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName");
                return View();
            }

            var currentUrl = Url.Action("Create", "JPApplications");
            ViewBag.Message = "Please sign-in to access the Create page in Job Placement";
            return RedirectToAction("Login", "Account", new { returnUrl = currentUrl });
        }

        // POST: JPApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPAppId,JPStudentId,JPCompanyName,JPJobTitle,JPJobCategory,JPCompanyCity,JPCompanyState,JPStudentLocation")] JPApplication jPApplication)
        {
            if (ModelState.IsValid)
            {
                //Auto-populating JPAppId, ApplicationUserId, and JPApplicationDate during user creation.
                jPApplication.JPAppId = Guid.NewGuid();
                jPApplication.ApplicationUserId = User.Identity.GetUserId();
                jPApplication.JPApplicationDate = DateTime.Now;
                string userID = User.Identity.GetUserId();
                JPStudent jpStudent = db.JPStudents.Where(x => x.ApplicationUserId == userID).FirstOrDefault();
                
                jPApplication.JPStudentLocation = jpStudent.JPStudentLocation;
                db.JPApplications.Add(jPApplication);
                
                //Create JPNotifcation with Graduate = true if total user applications =35

                
                var applications = from s in db.JPApplications.Where(x => x.ApplicationUserId == userID)
                                   select s;
                int appCount = applications.Count();
                if (appCount==34)
                {
                    JPNotification jPNotification = new JPNotification();
                    jPNotification.Graduate = true;
                    jPNotification.NotificationDate = DateTime.Now;
                    jPNotification.JPStudent = jpStudent;
                    db.JPNotifications.Add(jPNotification);
                    
                }
                db.SaveChanges();
                return RedirectToAction("StudentIndex");

                
                
            }
            
            
            //ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPApplication.JPStudentId);
            return View(jPApplication);
        }

        // GET: JPApplications/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPApplication jPApplication = db.JPApplications.Find(id);
            if (jPApplication == null)
            {
                return HttpNotFound();
            }
            //ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPApplication.JPStudentId);
            return View(jPApplication);
        }

        // POST: JPApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPAppId,JPStudentId,JPCompanyName,JPJobTitle,JPJobCategory,JPCompanyCity,JPCompanyState,JPApplicationDate")] JPApplication jPApplication)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPApplication).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPApplication.JPStudentId);
            return View(jPApplication);
        }

        // GET: JPApplications/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPApplication jPApplication = db.JPApplications.Find(id);
            if (jPApplication == null)
            {
                return HttpNotFound();
            }
            return View(jPApplication);
        }

        // POST: JPApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            JPApplication jPApplication = db.JPApplications.Find(id);
            db.JPApplications.Remove(jPApplication);
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
