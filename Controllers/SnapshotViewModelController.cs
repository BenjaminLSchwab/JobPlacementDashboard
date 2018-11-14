using JobPlacementDashboard.Models;
using JobPlacementDashboard.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JobPlacementDashboard.Controllers
{
    public class SnapshotViewModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SnapshotViewModel
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Snapshot()
        {
            var weeklyAppsList = new List<JPApplication>();
            var weeklyHiresList = new List<JPHire>();

            var monthlyAppsList = new List<JPApplication>();
            var monthlyHiresList = new List<JPHire>();

            var yearlyAppsList = new List<JPApplication>();
            var yearlyHiresList = new List<JPHire>();



            var newJpStudentsList = new List<JPStudent>();

            var jpStudentCount = 0;
            var jpStudentCount_Portland = 0;
            var jpStudentCount_Denver = 0;
            var jpStudentCount_Seattle = 0;
            var jpStudentCount_Remote = 0;

            int unhiredGradCount = 0;
            int unhiredGradCount_Portland = 0;
            int unhiredGradCount_Denver = 0;
            int unhiredGradCount_Seattle = 0;
            int unhiredGradCount_Remote = 0;

            int totalDaysInJP = 0, portlandDaysInJP = 0, denverDaysInJP = 0, seattleDaysInJP = 0, remoteDaysInJP = 0;  
            int totalStudents = 0, portlandStudents = 0, denverStudents = 0, seattleStudents = 0, remoteStudents = 0;
            int avgDaysInJP = 0, portlandAvgDaysInJP = 0, denverAvgDaysInJP = 0, seattleAvgDaysInJP = 0, remoteAvgDaysInJP = 0;

            foreach (var student in db.JPStudents)
            {
                int id = student.JPStudentId;

                if (student.JPHired == true)
                {
                    DateTime hireDate = db.JPHires.Where(x => x.ApplicationUserId == student.ApplicationUserId).FirstOrDefault().JPHireDate;

                    if ((student.JPStudentLocation == JPStudentLocation.PortlandLocal) || (student.JPStudentLocation == JPStudentLocation.PortlandRemote))
                    {
                        portlandDaysInJP += (int)(hireDate - student.JPStartDate).TotalDays;
                        portlandStudents++;
                    }
                    if ((student.JPStudentLocation == JPStudentLocation.DenverLocal) || (student.JPStudentLocation == JPStudentLocation.DenverRemote))
                    {
                        denverDaysInJP += (int)(hireDate - student.JPStartDate).TotalDays;
                        denverStudents++;
                    }
                    if ((student.JPStudentLocation == JPStudentLocation.SeattleLocal) || (student.JPStudentLocation == JPStudentLocation.SeattleRemote))
                    {
                        seattleDaysInJP += (int)(hireDate - student.JPStartDate).TotalDays;
                        seattleStudents++;
                    }
                    if ((student.JPStudentLocation == JPStudentLocation.Remote))
                    {
                        remoteDaysInJP += (int)(hireDate - student.JPStartDate).TotalDays;
                        remoteStudents++;
                    }
                    totalDaysInJP += (int)(hireDate - student.JPStartDate).TotalDays;
                    totalStudents++;
                }

                var apps = db.JPApplications.Where(a => a.ApplicationUserId == student.ApplicationUserId).ToList();
                if (student.JPHired == true)
                {
                    var hire = db.JPHires.Where(a => a.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
                    if (hire.IsHiredWithinOneWeekOfCurrentDate == true) weeklyHiresList.Add(hire); //it goes 8 times and dups // 
                    if (hire.IsHiredWithinOneMonthOfCurrentDate == true) monthlyHiresList.Add(hire);
                    if (hire.IsHiredWithinOneYearOfCurrentDate == true) yearlyHiresList.Add(hire);

                }

                else if (student.JPHired == false && student.JPGraduated == false)
                {
                    if ((student.JPStudentLocation == JPStudentLocation.PortlandLocal) || (student.JPStudentLocation == JPStudentLocation.PortlandRemote)) jpStudentCount_Portland++;
                    if ((student.JPStudentLocation == JPStudentLocation.DenverLocal) || (student.JPStudentLocation == JPStudentLocation.DenverRemote)) jpStudentCount_Denver++;
                    if ((student.JPStudentLocation == JPStudentLocation.SeattleLocal) || (student.JPStudentLocation == JPStudentLocation.SeattleRemote)) jpStudentCount_Seattle++;
                    if ((student.JPStudentLocation == JPStudentLocation.Remote) || (student.JPStudentLocation == JPStudentLocation.PortlandLocal)) jpStudentCount_Remote++;
                    jpStudentCount++;
                    if (student.IsStartDateWithinOneWeekOfCurrentDate == true)
                    {
                        newJpStudentsList.Add(student);
                    }
                }

                else if (student.JPHired == false && student.JPGraduated == true)
                {
                    if ((student.JPStudentLocation == JPStudentLocation.PortlandLocal) || (student.JPStudentLocation == JPStudentLocation.PortlandRemote)) unhiredGradCount_Portland++;
                    if ((student.JPStudentLocation == JPStudentLocation.DenverLocal) || (student.JPStudentLocation == JPStudentLocation.DenverRemote)) unhiredGradCount_Denver++;
                    if ((student.JPStudentLocation == JPStudentLocation.SeattleLocal) || (student.JPStudentLocation == JPStudentLocation.SeattleRemote)) unhiredGradCount_Seattle++;
                    if (student.JPStudentLocation == JPStudentLocation.Remote) unhiredGradCount_Remote++;
                    unhiredGradCount++;
                }

                foreach (var app in apps) if (app.IsAppliedDateWithinOneWeekOfCurrentDate == true) weeklyAppsList.Add(app);
            }

            var newJpStudentsList_Portland = newJpStudentsList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).ToList();
            var newJpStudentsList_Denver = newJpStudentsList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).ToList();
            var newJpStudentsList_Seattle = newJpStudentsList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).ToList();
            var newJpStudentsList_Remote = newJpStudentsList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).ToList();

            var weeklyHiresList_Portland = weeklyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).ToList();
            var weeklyHiresList_Denver = weeklyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).ToList();
            var weeklyHiresList_Seattle = weeklyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).ToList();
            var weeklyHiresList_Remote = weeklyHiresList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).ToList();

            var monthlyHiresList_Portland = monthlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).ToList();
            var monthlyHiresList_Denver = monthlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).ToList();
            var monthlyHiresList_Seattle = monthlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).ToList();
            var monthlyHiresList_Remote = monthlyHiresList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).ToList();

            var yearlyHiresList_Portland = yearlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).ToList();
            var yearlyHiresList_Denver = yearlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).ToList();
            var yearlyHiresList_Seattle = yearlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).ToList();
            var yearlyHiresList_Remote = yearlyHiresList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).ToList();

            int totalWeeklyHires = weeklyHiresList.Count();
            int totalWeeklyHires_Portland = weeklyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandLocal)).Count();
            int totalWeeklyHires_Denver = weeklyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).Count();
            int totalWeeklyHires_Seattle = weeklyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).Count();
            int totalWeeklyHires_Remote = weeklyHiresList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).Count();

            int totalMonthlyHires = monthlyHiresList.Count();
            int totalMonthlyHires_Portland = monthlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandLocal)).Count();
            int totalMonthlyHires_Denver = monthlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).Count();
            int totalMonthlyHires_Seattle = monthlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).Count();
            int totalMonthlyHires_Remote = monthlyHiresList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).Count();

            int totalYearlyHires = yearlyHiresList.Count();
            int totalYearlyHires_Portland = yearlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandLocal)).Count();
            int totalYearlyHires_Denver = yearlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).Count();
            int totalYearlyHires_Seattle = yearlyHiresList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).Count();
            int totalYearlyHires_Remote = yearlyHiresList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).Count();    

            int totalWeeklyApps = weeklyAppsList.Count();
            int totalWeeklyApps_Portland = weeklyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).Count();
            int totalWeeklyApps_Denver = weeklyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).Count();
            int totalWeeklyApps_Seattle = weeklyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).Count();
            int totalWeeklyApps_Remote = weeklyAppsList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).Count();

            int totalMonthlyApps = monthlyAppsList.Count();
            int totalMonthlyApps_Portland = monthlyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).Count();
            int totalMonthlyApps_Denver = monthlyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).Count();
            int totalMonthlyApps_Seattle = monthlyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).Count();
            int totalMonthlyApps_Remote = monthlyAppsList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).Count();

            int totalYearlyApps = yearlyAppsList.Count();
            int totalYearlyApps_Portland = yearlyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.PortlandLocal) || (x.JPStudentLocation == JPStudentLocation.PortlandRemote)).Count();
            int totalYearlyApps_Denver = yearlyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.DenverLocal) || (x.JPStudentLocation == JPStudentLocation.DenverRemote)).Count();
            int totalYearlyApps_Seattle = yearlyAppsList.Where(x => (x.JPStudentLocation == JPStudentLocation.SeattleLocal) || (x.JPStudentLocation == JPStudentLocation.SeattleRemote)).Count();
            int totalYearlyApps_Remote = yearlyAppsList.Where(x => x.JPStudentLocation == JPStudentLocation.Remote).Count();


            if (portlandStudents > 0) { portlandAvgDaysInJP = (portlandDaysInJP / portlandStudents); }
            if (denverStudents > 0) { denverAvgDaysInJP = (denverDaysInJP / denverStudents); }
            if (seattleStudents > 0) { seattleAvgDaysInJP = (seattleDaysInJP / seattleStudents); }
            if (remoteStudents > 0) { remoteAvgDaysInJP = (remoteDaysInJP / remoteStudents); }
            avgDaysInJP = (totalDaysInJP / totalStudents);


            var snapshotStats = new SnapshotViewModel(
                newJpStudentsList, 
                weeklyHiresList, monthlyHiresList, yearlyHiresList, 
                totalWeeklyApps, totalMonthlyApps, totalYearlyApps,
                totalWeeklyHires, totalMonthlyHires, totalYearlyHires,
                jpStudentCount, unhiredGradCount,
                newJpStudentsList_Portland, 
                weeklyHiresList_Portland, monthlyHiresList_Portland, yearlyHiresList_Portland,
                totalWeeklyApps_Portland, totalMonthlyApps_Portland, totalYearlyApps_Portland,
                totalWeeklyHires_Portland, totalMonthlyHires_Portland, totalYearlyHires_Portland,
                jpStudentCount_Portland, unhiredGradCount_Portland, 
                newJpStudentsList_Denver, 
                weeklyHiresList_Denver, monthlyHiresList_Denver, yearlyHiresList_Denver,
                totalWeeklyApps_Denver, totalMonthlyApps_Denver, totalYearlyApps_Denver,
                totalWeeklyHires_Denver, totalMonthlyHires_Denver, totalYearlyHires_Denver,
                jpStudentCount_Denver, unhiredGradCount_Denver, 
                newJpStudentsList_Seattle,
                weeklyHiresList_Seattle, monthlyHiresList_Seattle, yearlyHiresList_Seattle,
                totalWeeklyApps_Seattle, totalMonthlyApps_Seattle, totalYearlyApps_Seattle,
                totalWeeklyHires_Seattle, totalMonthlyHires_Seattle, totalYearlyHires_Seattle,
                jpStudentCount_Seattle, unhiredGradCount_Seattle,
                newJpStudentsList_Remote, 
                weeklyHiresList_Remote, monthlyHiresList_Remote, yearlyHiresList_Remote,
                totalWeeklyApps_Remote, totalMonthlyApps_Remote, totalYearlyApps_Remote,
                totalWeeklyHires_Remote, totalMonthlyHires_Remote, totalYearlyHires_Remote, 
                jpStudentCount_Remote, unhiredGradCount_Remote, 
                avgDaysInJP,portlandAvgDaysInJP, denverAvgDaysInJP, seattleAvgDaysInJP, remoteAvgDaysInJP, portlandDaysInJP, denverDaysInJP, seattleDaysInJP, remoteDaysInJP
                ); 
            //removed totalDaysInJP
            //weeklyApps is 3x what it should be (it was 54 today (8/30), but it should be 18) because there are 3 copies of each seed student. Without duplicate seed data this should work fine.)
            return View(snapshotStats);

        }


        // GET: SnapshotViewModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SnapshotViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SnapshotViewModel/Create
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

        // GET: SnapshotViewModel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SnapshotViewModel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SnapshotViewModel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SnapshotViewModel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // CSV Export Option
        public ActionResult ExportCSV(bool? forHires)
        {
            var beginDate = DateTime.Now.AddDays(-7);
            string FileName = "";
            StringBuilder sb = new StringBuilder();
            var emailList = Enumerable.Empty<Object>().AsQueryable().Select(r => new { JPEmail = "", ApplicationUserId = "" });
            if (forHires == true)
            {
                var weeklyHires = from student in db.JPStudents
                                  join hire in db.JPHires
                                  on student.ApplicationUserId
                                  equals hire.ApplicationUserId
                                  where (hire.JPHireDate >= beginDate && hire.JPHireDate <= DateTime.Now)
                                  select new { student.JPEmail, student.ApplicationUserId};
                emailList = weeklyHires;

                

                //creating CSV
                sb.Append("Weekly Hired Student Report");
                
                FileName = "WeeklyHiredEmailList.csv";
                
            }
            else
            {
                sb.Append("New Student Report");
                
                FileName = "NewStudentEmailList.csv";
                
                var newStudentEmails = from student in db.JPStudents
                                       where (student.JPStartDate >= beginDate && student.JPStartDate <= DateTime.Now
                                       && student.JPHired == false && student.JPGraduated == false)
                                       select new { student.JPEmail, student.ApplicationUserId };
                emailList = newStudentEmails;

                

            }
            foreach (var email in emailList)
            {
                sb.AppendLine();
                sb.Append(email.JPEmail.ToString());
                sb.Append(", ");
                Console.WriteLine(sb);
            }
            string FilePath = Server.MapPath("/App_Data/");
            System.IO.File.WriteAllText(FilePath + FileName, sb.ToString());
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/csv";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(FilePath + FileName);
            response.Flush();

            // Deletes the file on server
            System.IO.File.Delete(FilePath + FileName);

            response.End();

            var latestContacts = from contact in db.JPLatestContacts
                                 select new { contact.JPLatestContactId, contact.ApplicationUserId, contact.JPLatestContactDate };

            //updating the JPLatestContact Table
            foreach (var student in emailList)
            {
                var matchingContact = latestContacts.Where(x => x.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
                if (matchingContact == null)
                {
                    var newContact = new JPLatestContact();
                    newContact.ApplicationUserId = student.ApplicationUserId;
                    newContact.JPLatestContactDate = DateTime.Now;
                    newContact.JPLatestContactId = Guid.NewGuid();
                    db.JPLatestContacts.Add(newContact);
                }
                else
                {
                    var updatedContact = db.JPLatestContacts.Find(matchingContact.JPLatestContactId);
                    updatedContact.JPLatestContactDate = DateTime.Now;
                    db.JPLatestContacts.Attach(updatedContact);
                    db.Entry(updatedContact).Property(x => x.JPLatestContactDate).IsModified = true;
                }
            }
            db.SaveChanges();

            return RedirectToAction("Snapshot");
        }
        
    }
}
