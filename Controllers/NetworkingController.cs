using JobPlacementDashboard.ViewModels;
using JobPlacementDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPlacementDashboard.Controllers
{
    public class NetworkingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Networking(string sortOrder, string filterBy = "", string keyword = "")
        {
            var NetworkingList = new List<NetworkingViewModel>();

            foreach (var hire in db.JPHires.Where(x => x.JPSecondJob == false))
            {
                string id = hire.ApplicationUserId;
                var student = db.JPStudents.Where(a => a.ApplicationUserId == hire.ApplicationUserId).FirstOrDefault();
                var networking = new NetworkingViewModel(student, hire);

                NetworkingList.Add(networking);
            }
            foreach (var hire in db.JPHires.Where(x => x.JPSecondJob == true))
            {
                string id = hire.ApplicationUserId;
                var student = db.JPStudents.Where(a => a.ApplicationUserId == hire.ApplicationUserId).FirstOrDefault();
                var currentJob = db.JPCurrentJobs.Where(a => a.ApplicationUserId == hire.ApplicationUserId).FirstOrDefault();
                var networking = new NetworkingViewModel(student, currentJob);

                NetworkingList.Add(networking);
            }

            //the first time the page is loaded, sortOrder will be null, so each viewbag item will be set to the ascending order version            
            ViewBag.StudentNameSortParm = sortOrder == "studentName" ? "studentName_desc" : "studentName";
            ViewBag.CompanyNameSortParm = sortOrder == "companyName" ? "companyName_desc" : "companyName";
            ViewBag.JobTitleSortParm = sortOrder == "jobTitle" ? "jobTitle_desc" : "jobTitle";
            ViewBag.CompanyLocationSortParm = sortOrder == "companyLocation" ? "companyLocation_desc" : "companyLocation";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";


            switch (sortOrder)
            {
                default:                    
                case "studentName":
                    NetworkingList = NetworkingList.OrderBy(x => x.JPName).ToList();
                    break;
                case "studentName_desc":
                    NetworkingList = NetworkingList.OrderByDescending(x => x.JPName).ToList();
                    break;
                case "companyName":
                    NetworkingList = NetworkingList.OrderBy(x => x.JPCompanyName).ToList();
                    break;
                case "companyName_desc":
                    NetworkingList = NetworkingList.OrderByDescending(x => x.JPCompanyName).ToList();
                    break;
                case "jobTitle":
                    NetworkingList = NetworkingList.OrderBy(x => x.JPJobTitle).ToList();
                    break;
                case "jobTitle_desc":
                    NetworkingList = NetworkingList.OrderByDescending(x => x.JPJobTitle).ToList();
                    break;
                case "companyLocation":
                    NetworkingList = NetworkingList.OrderBy(x => x.JPCompanyCity).ToList();
                    break;
                case "companyLocation_desc":
                    NetworkingList = NetworkingList.OrderByDescending(x => x.JPCompanyCity).ToList();
                    break;
                case "date":
                    NetworkingList = NetworkingList.OrderBy(x => x.JPHireDate).ToList();
                    break;
                case "date_desc":
                    NetworkingList = NetworkingList.OrderByDescending(x => x.JPHireDate).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                var filterText = keyword;

                switch (filterBy)
                {
                    case "JPName":
                        var INetworkingList = from record in NetworkingList.OrderBy(n => n.JPName)
                                              where record.JPName.ToLower().Contains(filterText.ToLower())
                                              select record;
                        NetworkingList = INetworkingList.ToList();
                        break;
                    case "JPCompanyName":
                        INetworkingList = from record in NetworkingList.OrderBy(n => n.JPName)
                                          where record.JPCompanyName.ToLower().Contains(filterText.ToLower())
                                          select record;
                        NetworkingList = INetworkingList.ToList();
                        break;
                    case "JPJobLocation":
                        INetworkingList = from record in NetworkingList.OrderBy(n => n.JPName)
                                          where record.JPCompanyCity.ToLower().Contains(filterText.ToLower())
                                          select record;
                        NetworkingList = INetworkingList.ToList();
                        break;
                    case "JPJobTile":
                        INetworkingList = from record in NetworkingList.OrderBy(n => n.JPName)
                                          where record.JPJobTitle.ToLower().Contains(filterText.ToLower())
                                          select record;
                        NetworkingList = INetworkingList.ToList();
                        break;
                    default:
                        INetworkingList = from record in NetworkingList.OrderBy(n => n.JPName)
                                          where record.JPJobTitle.ToLower().Contains(filterText.ToLower())
                                          || record.JPCompanyCity.ToLower().Contains(filterText.ToLower())
                                          || record.JPCompanyName.ToLower().Contains(filterText.ToLower())
                                          || record.JPName.ToLower().Contains(filterText.ToLower())
                                          select record;
                        NetworkingList = INetworkingList.ToList();
                        break;

                }
            }
            //to show when result are not found rather than an empty screen
            if (NetworkingList.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Your search returned no results, please try again.");
            }

            return View(NetworkingList);          
        }

        // GET: Networking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Networking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Networking/Create
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

        // GET: Networking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Networking/Edit/5
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

        // GET: Networking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Networking/Delete/5
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
    }
}
