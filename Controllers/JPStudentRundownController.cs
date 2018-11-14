using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPlacementDashboard.ViewModels;
using JobPlacementDashboard.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using System.Reflection;

namespace JobPlacementDashboard.Controllers
{
    public class JPStudentRundownController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ViewResult Index(string sortOrder, string searchString, string latestContact)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.LocationSortParm = sortOrder == "Location" ? "location_desc" : "Location";
            ViewBag.GraduateSortParm = sortOrder == "Graduate" ? "graduate_desc" : "Graduate";
            ViewBag.TotalSortParm = sortOrder == "Total" ? "total_desc" : "Total";
            ViewBag.WeeklySortParm = sortOrder == "Weekly" ? "weekly_desc" : "Weekly";
            ViewBag.NoActivity = sortOrder == "No Activity" ? "No_Activity" : "No_Activity";
            ViewBag.LatestContactSortParm = sortOrder == "LatestContact" ? "latestcontact_desc" : "LatestContact";

            List<JPStudentRundown> jPStudentRundownList = new List<JPStudentRundown>();


            var students = from s in db.JPStudents
                           where s.JPHired == false //setting the logic here so we can skip the whole passing of the object into the list, and then not have to look at it in the view at all. Before it would still have to hit all the information below
                           select s;                        //where as now it will skip the following and not even be there in the view
                           

            if (!String.IsNullOrEmpty(searchString))
            {
                string searchStringNoSpaces = Regex.Replace(searchString, @"\s+", "");
                students = students.Where(s => s.JPStudentLocation.ToString().Contains(searchString) || s.JPStudentLocation.ToString().Contains(searchStringNoSpaces) || s.JPName.Contains(searchString));
            }

            // For when student email link is selected
            if (!String.IsNullOrEmpty(latestContact))
            {
                int jpid = Convert.ToInt32(latestContact);
                string userid = db.JPStudents.Where(s => s.JPStudentId == jpid).FirstOrDefault().ApplicationUserId;
                UpdateLatestContact(userid);
            }

            switch (sortOrder)
            {
                case "Graduate":
                    students = students.Where(s => (db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId).Count() >= 35));
                    break;
                case "graduate_desc":
                    students = students.Where(s => (db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId).Count() >= 35));
                    break;
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
                case "Total":
                    students = students.OrderBy(s => db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId).Count());
                    break;
                case "total_desc":
                    students = students.OrderByDescending(s => db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId).Count());
                    break;
                case "Weekly":
                    var dateCriteria = DateTime.Now.AddDays(-7);
                    students = students.OrderBy(s => db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId && a.JPApplicationDate >= dateCriteria).Count());
                    break;
                case "weekly_desc":
                    dateCriteria = DateTime.Now.AddDays(-7);
                    students = students.OrderByDescending(s => db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId && a.JPApplicationDate >= dateCriteria).Count());
                    break;
                case "No_Activity":
                    dateCriteria = DateTime.Now.AddDays(-7);
                    students = students.Where(s => db.JPApplications.Where(a => a.ApplicationUserId == s.ApplicationUserId && a.JPApplicationDate >= dateCriteria).Count() == 0);
                    break;
                case "LatestContact":
                    students = from c in db.JPLatestContacts
                               join s in db.JPStudents on c.ApplicationUserId equals s.ApplicationUserId
                               where s.JPHired == false
                               orderby c.JPLatestContactDate ascending
                               select s;
                    break;
                case "latestcontact_desc":
                    students = from c in db.JPLatestContacts
                               join s in db.JPStudents on c.ApplicationUserId equals s.ApplicationUserId
                               where s.JPHired == false
                               orderby c.JPLatestContactDate descending
                               select s;
                    break;

                default: //Name ascending
                    students = students.OrderBy(s => s.JPName);
                    break;
            }

            foreach (var student in students)
            {
                jPStudentRundownList.Add(BuildRundownObj(student.JPStudentId));

            }

            return View(jPStudentRundownList);
        }

        /*      Commented out details, create and delete for now
         * 
                // GET: JPStudentRundown/Details/5
                public ActionResult Details(int id)
                {
                    return View();
                }

                // GET: JPStudentRundown/Create
                public ActionResult Create()
                {
                    return View();
                }

                // POST: JPStudentRundown/Create
                [HttpPost]
                public ActionResult Create(FormCollection collection)
                {
                    try
                    {
                        // TODO: Add insert logic here

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }
        */
        // GET: JPStudentRundown/Edit/5
        public ActionResult Edit(int id)
        {
            JPStudent student = db.JPStudents.Find(id);
            return View(student);
        }

        // POST: JPStudentRundown/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPStudentId,JPName,JPEmail,JPStudentLocation,JPStartDate,JPLinkedIn,JPGithubLink,JPPortfolio,JPContact,JPGraduated,JPHired,ApplicationUserId")] JPStudent jPStudent)
        {            
            if (ModelState.IsValid)
            {

                db.Entry(jPStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");



            }
            return RedirectToAction("Index");

        }

        public JPStudentRundown BuildRundownObj(int id)//this is to create a JPStudentRundown object out of a JPStudent keyte
        {
            var student = db.JPStudents.Find(id);
            var LatestContact = db.JPLatestContacts.Where(a => a.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
            var studentApps = db.JPApplications.Where(a => a.ApplicationUserId == student.ApplicationUserId).ToList();
            var applicationCount = studentApps.Count();
            var thisWeekCount = studentApps.Where(a => a.IsAppliedDateWithinOneWeekOfCurrentDate == true).Count();
            var checkListStatus = CheckListStatusCount(id);
            JPChecklist checklist = db.JPChecklists.Where(a => a.ApplicationUserid == student.ApplicationUserId).FirstOrDefault();
            checklist.JPBusinessCards = false;
            checklist.JPMeetups = false;
            checklist.JPUpdatedLinkedIn = false;
            checklist.JPUpdatedPortfolioSite = false;
            checklist.JPCleanGitHub = false;
            checklist.JpRoundTables = false;
            var studentRundown = new JPStudentRundown(student, checklist, applicationCount, thisWeekCount, LatestContact.CalculateLastContactDate, checkListStatus);
            return studentRundown;
        }

        public void UpdateLatestContact(string userid)
        {
            //update latest contact if entry found
            if (db.JPLatestContacts.Where(c => c.ApplicationUserId == userid).ToList().Count() > 0)
            {
                var contact = db.JPLatestContacts.Where(c => c.ApplicationUserId == userid).FirstOrDefault();
                contact.JPLatestContactDate = DateTime.Now;
                db.SaveChanges();
            }
            //otherwise add new latestContactObject
            else
            {
                var contact = new JPLatestContact();
                contact.ApplicationUserId = userid;
                contact.JPLatestContactDate = DateTime.Now;
                db.JPLatestContacts.Add(contact);
                db.SaveChanges();
            }
        }


        public ActionResult ExportCSV(string emailList)
        {
            string FilePath = Server.MapPath("/App_Data/");
            string FileName = "EmailList.csv";

            System.IO.File.WriteAllText(FilePath + FileName, emailList);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/csv";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(FilePath + FileName);
            response.Flush();
            System.IO.File.Delete(FilePath + FileName); // Deletes the file on server

            response.End();
            List<string> listOfEmails = emailList.Split(',').ToList();
            foreach (var emailName in listOfEmails)
            {
                //Takes each email in list and searches for it on the JPStudents table and finds the associated ApplicationUserID.
                //Then calls the UpdateLatestContact method on each ApplicationUserID.
                var userId = db.JPStudents.Where(x => x.JPEmail == emailName).First().ApplicationUserId.ToString();
                UpdateLatestContact(userId);
            }
            return RedirectToAction("Index");
        }

        //Counts the total of completed items in JPChecklists table
        public int CheckListStatusCount(int id)
        {
            var count = 0;
            var studentId = db.JPStudents.Find(id);
            var studentIns = db.JPChecklists.Where(a => a.ApplicationUserid == studentId.ApplicationUserId).FirstOrDefault();
            foreach (var prop in studentIns.GetType().GetProperties())
            {
                var propValue = prop.GetValue(studentIns, null).ToString().ToLower();
                if (propValue == "true") { count += 1; }
            }
            return count;
        }

        public ActionResult _Checklist(string applicationUserId)
        {
            JPChecklist checklist = db.JPChecklists.Where(a => a.ApplicationUserid == applicationUserId).FirstOrDefault();
            return PartialView("_Checklist", checklist);
        }
    }
}
