using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPlacementDashboard.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;

namespace JobPlacementDashboard.Controllers
{
    public class JPStudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JPStudents
        
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "location_desc" : "Location";

            var students = from s in db.JPStudents
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                string searchStringNoSpaces = Regex.Replace(searchString, @"\s+", "");
                students = students.Where(s => s.JPStudentLocation.ToString().Contains(searchString) || s.JPStudentLocation.ToString().Contains(searchStringNoSpaces) || s.JPName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.JPName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.JPStartDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.JPStartDate);
                    break;
                case "Location":
                    students = students.OrderBy(s => s.JPStudentLocation);
                    break;
                case "location_desc":
                    students = students.OrderByDescending(s => s.JPStudentLocation);
                    break;
                default: //Name ascending
                    students = students.OrderBy(s => s.JPName);
                    break;
            }
            return View(students.ToList());
        }

        // GET: JPStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPStudent jPStudent = db.JPStudents.Find(id);
            if (jPStudent == null)
            {
                return HttpNotFound();
            }
            return View(jPStudent);
        }

        // GET: JPStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JPStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = 
            "JPStudentId,JPName,JPEmail,JPStudentLocation,JPStartDate," +
            "JPLinkedIn,JPPortfolio,JPGithubLink,JPContact,JPGraduated,JPHired")] JPStudent jPStudent)
        {
            //if (ModelState.IsValid)
            //{
            //    db.JPStudents.Add(jPStudent);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(jPStudent);

            if (ModelState.IsValid)
            {
                // Test to see if URL provided has http or https in it. 
                // If not, then save as is. If it does, strip the protocol.
                string linkedInUrl = jPStudent.JPLinkedIn;
                Regex regexLI = new Regex(@"^http(s)?://");
                Match matchLI = regexLI.Match(linkedInUrl);
                if (matchLI.Success)
                {
                    Uri linkedInNewURL = new Uri(linkedInUrl);
                    string linkedInOutput = linkedInNewURL.Host + linkedInNewURL.PathAndQuery;
                    jPStudent.JPLinkedIn = linkedInOutput;
                }
                else
                {
                    jPStudent.JPLinkedIn = linkedInUrl;
                }

                // Test to see if URL provided has http or https in it. If not, 
                // then save as is. If it does, strip the protocol.
                string portfolioUrl = jPStudent.JPPortfolio;
                Regex regexP = new Regex(@"^http(s)?://");
                Match matchP = regexP.Match(portfolioUrl);
                if (matchP.Success)
                {
                    Uri portfolioNewURL = new Uri(portfolioUrl);
                    string portfolioOutput = portfolioNewURL.Host + portfolioNewURL.PathAndQuery;
                    jPStudent.JPPortfolio = portfolioOutput;
                }
                else
                {
                    jPStudent.JPPortfolio = portfolioUrl;
                }

                JPLatestContact jPLatestContact = new JPLatestContact
                {
                    JPLatestContactDate = DateTime.Now,
                    ApplicationUserId = User.Identity.GetUserId(),
                    JPLatestContactId = Guid.NewGuid()
                };

                db.JPLatestContacts.Add(jPLatestContact);

             
                db.SaveChanges();

                jPStudent.JPStartDate = DateTime.Now;
                jPStudent.JPGraduated = false;
                jPStudent.JPHired = false;
               
                jPStudent.ApplicationUserId = User.Identity.GetUserId();
                db.JPStudents.Add(jPStudent);
                db.SaveChanges();

                var checklist = new JPChecklist();
                checklist.ApplicationUserid = User.Identity.GetUserId();
                checklist.JPBusinessCards = checklist.JPCleanGitHub = checklist.JPMeetups = checklist.JpRoundTables = checklist.JPUpdatedLinkedIn = checklist.JPUpdatedPortfolioSite = false;
                db.JPChecklists.Add(checklist);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(jPStudent);
        }

        // GET: JPStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPStudent jPStudent = db.JPStudents.Find(id);
            if (jPStudent == null)
            {
                return HttpNotFound();
            }
            return View(jPStudent);
        }

        // POST: JPStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = 
            "JPStudentId,JPName,JPEmail,JPStudentLocation," +
            "JPStartDate,JPLinkedIn,JPPortfolio,JPGithubLink,JPContact,JPGraduated,JPHired")]
        JPStudent jPStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jPStudent);
        }

        // GET: JPStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPStudent jPStudent = db.JPStudents.Find(id);
            if (jPStudent == null)
            {
                return HttpNotFound();
            }
            return View(jPStudent);
        }

        // POST: JPStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPStudent jPStudent = db.JPStudents.Find(id);
            db.JPStudents.Remove(jPStudent);
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
