using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using JobPlacementDashboard.Models;
using Microsoft.AspNet.Identity;

namespace JobPlacementDashboard.Controllers
{
    public class JPHiresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        public ViewResult Index(string sortOrder, string searchString)
        {

            ViewBag.CompanySortParm = String.IsNullOrEmpty(sortOrder) ? "company_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.JobTitleSortParm = sortOrder == "JobTitle" ? "jobTitle_desc" : "JobTitle";
            ViewBag.JobCategorySortParm = sortOrder == "JobCategory" ? "jobCategory_desc" : "JobCategory";
            ViewBag.SalarySortParm = sortOrder == "Salary" ? "salary_desc" : "Salary";
            ViewBag.CompanyCitySortParm = sortOrder == "CompanyCity" ? "companyCity_desc" : "CompanyCity";
            ViewBag.CompanyStateSortParm = sortOrder == "CompanyState" ? "companyState_desc" : "CompanyState";
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "HireDate" : "";

            var students = from s in db.JPStudents
                           select s;
            var hires = from s in db.JPHires.Include(j => j.ApplicationUser)
                        select s;


            if (!String.IsNullOrEmpty(searchString))
            {
                hires = hires.Where(s => s.ApplicationUser.FirstName.Contains(searchString) || s.ApplicationUser.FirstName.Contains(searchString) || s.JPCompanyName.Contains(searchString) || s.JPJobCategory.ToString().Contains(searchString)
                                       || s.JPCompanyCity.Contains(searchString) || s.JPCompanyState.ToString().Contains(searchString) || s.JPSalary.ToString().Contains(searchString) || s.JPHireDate.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.JPName);
                    break;
                default: //Name ascending
                    students = students.OrderBy(s => s.JPName);
                    break;

            }
            switch (sortOrder)
            {

                case "company_desc":
                    hires = hires.OrderByDescending(s => s.JPCompanyName);
                    break;

                //case name_desc needs to be included again even though referenced in above switch (sortOrder)

                case "name_desc":
                    hires = hires.OrderByDescending(s => s.ApplicationUser.FirstName);
                    break;
                case "JobTitle":
                    hires = hires.OrderBy(s => s.JPJobTitle);
                    break;
                case "jobTitle_desc":
                    hires = hires.OrderByDescending(s => s.JPJobTitle);
                    break;
                case "JobCategory":
                    hires = hires.OrderBy(s => s.JPJobCategory);
                    break;
                case "jobCategory_desc":
                    hires = hires.OrderByDescending(s => s.JPJobCategory);
                    break;
                case "Salary":
                    hires = hires.OrderBy(s => s.JPSalary);
                    break;
                case "salary_desc":
                    hires = hires.OrderByDescending(s => s.JPSalary);
                    break;
                case "CompanyCity":
                    hires = hires.OrderBy(s => s.JPCompanyCity);
                    break;
                case "companyCity_desc":
                    hires = hires.OrderByDescending(s => s.JPCompanyCity);
                    break;
                case "CompanyState":
                    hires = hires.OrderBy(s => s.JPCompanyState.GetDisplayName());
                    break;
                case "companyState_desc":
                    hires = hires.OrderByDescending(s => s.JPCompanyState.GetDisplayName());
                    break;
                case "companyCareersPage_desc":
                    hires = hires.OrderByDescending(s => s.JPCareersPage);
                    break;
                case "HireDate":
                    hires = hires.OrderBy(s => s.JPHireDate);
                    break;

                

                default: //Company ascending

                    hires = hires.OrderBy(s => s.JPCompanyName);
                    break;


            }

            //JPCompanyState model = new JPCompanyState { db.JPCompanyState = EnumDisplayName.DisplayName};


            return View(hires.ToList());

        }

        // GET: JPHires/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPHire jPHire = db.JPHires.Find(id);
            if (jPHire == null)
            {
                return HttpNotFound();
            }
            return View(jPHire);
        }

        // GET: JPHires/Create
        public ActionResult Create()
        {
            ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName");
            return View();
        }

        // POST: JPHires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPHireId,JPStudentId,JPCompanyName,JPJobTitle,JPJobCategory,JPSalary,JPCompanyCity,JPCompanyState,JPSecondJob,JPCareersPage,JPHireDate")] JPHire jPHire)
        {
            if (ModelState.IsValid)
            {
                // Grabs the active users ID and uses it to identify the users row in JPStudents table to edit JPGraduated and JPHired from false to true.
                string userID = User.Identity.GetUserId();
                JPStudent jpStudent = db.JPStudents.Where(x => x.ApplicationUserId == userID).FirstOrDefault();
                jpStudent.JPGraduated = true;
                jpStudent.JPHired = true;
                
                
                //Auto-populating JPHireId, ApplicationUserId, and JPHireDate during user creation.
                jPHire.JPHireId = Guid.NewGuid();
                jPHire.JPHireDate = DateTime.Now;
                jPHire.ApplicationUserId = userID;
                
                db.Entry(jpStudent).State = EntityState.Modified;
                db.JPHires.Add(jPHire);
                

                //Create JPNotification record 

                JPNotification jPNotification = new JPNotification();
                jPNotification.JPStudent = jpStudent;
                jPNotification.Hire = true;
                jPNotification.NotificationDate = DateTime.Now;


                db.JPNotifications.Add(jPNotification);
                db.SaveChanges();
              
                
                //Build notification email and assign sender/recipient
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress("PortlandJobPlacement@learncodinganywhere.com"));
                message.From = new MailAddress("PLACEHOLDER@live.com");//REPLACE WITH VALID VALUE
                message.Subject = "Automated Hiring Alert";
                message.Body = jpStudent.JPName + " has submit a Hiring form. This is an automated notification.";
                message.IsBodyHtml = false;

                //Send notification email to portland jobs director via async task
                HostingEnvironment.QueueBackgroundWorkItem(_ => SendEmail(message));

                return RedirectToAction("Index");
            }

            //ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPHire.JPStudentId);
            return View(jPHire);
        }

        // GET: JPHires/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPHire jPHire = db.JPHires.Find(id);
            if (jPHire == null)
            {
                return HttpNotFound();
            }
            //ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPHire.JPStudentId);
            return View(jPHire);
        }

        // POST: JPHires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPHireId,JPStudentId,JPCompanyName,JPJobTitle,JPJobCategory,JPSalary,JPCompanyCity,JPCompanyState,JPSecondJob,JPCareersPage,JPHireDate")] JPHire jPHire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPHire).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.JPStudentId = new SelectList(db.JPStudents, "JPStudentId", "JPName", jPHire.JPStudentId);
            return View(jPHire);
        }

        // GET: JPHires/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPHire jPHire = db.JPHires.Find(id);
            if (jPHire == null)
            {
                return HttpNotFound();
            }
            return View(jPHire);
        }

        // POST: JPHires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            JPHire jPHire = db.JPHires.Find(id);
            db.JPHires.Remove(jPHire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(MailMessage message)
        {
            string networkUser = "PLACEHOLDER@live.com";//REPLACE WITH VALID VALUE 
            string networkPass = "Pass1234";//REPLACE WITH VALID VALUE 
            if (ModelState.IsValid)
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = networkUser,
                        Password = networkPass
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
            }
            return View();
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
