using JobPlacementDashboard.Models;
using JobPlacementDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPlacementDashboard.Controllers
{
    public class AnalyticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var analyticsList = new List<AnalyticsViewModel>();
            foreach (var student in db.JPStudents.Where(a => a.JPHired == true))
            {
                int id = student.JPStudentId;
                var hire = db.JPHires.Where(a => a.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
                int applicationCount = db.JPApplications.Where(a => a.ApplicationUserId == student.ApplicationUserId).Count();
                var analytics = new AnalyticsViewModel(student, applicationCount, hire);

                analyticsList.Add(analytics);
            }
            return View(analyticsList);
        }

        public ActionResult Analytics()
        {

            decimal averageSalary = (from sal in db.JPHires          //Calculate overall average Salary for hired employees
                                     select sal.JPSalary).Average();

            var averageAppsList = new List<AnalyticsViewModel>();
            var avgDaysList = new List<int>();
            foreach (var student in db.JPStudents.Where(a => a.JPHired == true))
            {
                int id = student.JPStudentId;
                var hire = db.JPHires.Where(a => a.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
                int applicationCount = db.JPApplications.Where(a => a.ApplicationUserId == student.ApplicationUserId).Count();
                TimeSpan? days = hire.JPHireDate - student.JPStartDate;
                int daysTillHired = days.Value.Days;
                var analytics = new AnalyticsViewModel(student, applicationCount, hire);
                averageAppsList.Add(analytics);
                avgDaysList.Add(daysTillHired);
            }

            int avgDaysTillHired = Convert.ToInt32(avgDaysList.Average());

            double avgApps = averageAppsList.Select(x => x.totalApplications).Average();// Calculate overall average # of applications for hired employees

            string commonCompany = averageAppsList.GroupBy(x => x.JPCompanyName) //Calculate overall most common company name for hired employees
                .OrderByDescending(x => x.Count())
                .First().Key;

            JPJobCategory commonJob = averageAppsList.GroupBy(x => x.JPJobCategory) //Calculate overall most common Job Category of hired employees
                .OrderByDescending(x => x.Count())
                .First().Key;

            string commonAppliedCompany = averageAppsList.GroupBy(x => x.JPCompanyName)
                .OrderByDescending(x => x.Count()).First().Key;

            JPJobCategory commonAppCategory = averageAppsList.GroupBy(x => x.JPJobCategory)
                .OrderByDescending(x => x.Count()).First().Key;

            var commonJobsList = new List<AnalyticsViewModel>();


            var averageHire = new AnalyticsViewModel(averageSalary, avgApps, commonJob, commonCompany, avgDaysTillHired, commonAppliedCompany, commonAppCategory);

            return View(averageHire);
        }

        public ActionResult Analyticaa()
        {
            int studentSum = db.JPStudents.Where(a => a.JPHired == true).Count();
            int applicationCount = 0;
            foreach (var student in db.JPStudents.Where(a => a.JPHired == true))
            {
                int theApplicationCount = db.JPApplications.Where(a => a.ApplicationUserId == student.ApplicationUserId).Count();
                applicationCount += theApplicationCount;
            }
            decimal salary = db.JPHires.Sum(i => i.JPSalary);
            double averageApps = applicationCount / studentSum;
            decimal AverageSalary = salary / studentSum;
            var commonJobCategory = db.JPHires.GroupBy(x => x.JPJobCategory)
                          .OrderByDescending(x => x.Count())
                          .First().Key;
            AnalyticsViewModel analytics = new AnalyticsViewModel(AverageSalary, averageApps, commonJobCategory);
            return View(analytics);
        }

        // GET: Analytics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Analytics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Analytics/Create
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

        // GET: Analytics/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Analytics/Edit/5
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

        // GET: Analytics/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Analytics/Delete/5
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


        [ChildActionOnly]
        public PartialViewResult _Carousel()
        {
            List<AnalyticsViewModel> carouselList = new List<AnalyticsViewModel>();
           
            foreach (var student in db.JPStudents.Where(a => a.JPHired == true && a.JPContact == true))
                {
                    string fname = student.JPName;
                    var hire = db.JPHires.Where(a => a.ApplicationUserId == student.ApplicationUserId).FirstOrDefault();
                    var company = hire.JPCompanyName;
                    var careerPage = hire.JPCareersPage;
    
                    var hiredStudent = new AnalyticsViewModel(fname, company, careerPage);
                    carouselList.Add(hiredStudent); 
            }
            return PartialView("_Carousel", carouselList);
        }
    }
}
