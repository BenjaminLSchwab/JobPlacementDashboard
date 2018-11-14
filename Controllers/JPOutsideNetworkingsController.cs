using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPlacementDashboard.Models;

namespace JobPlacementDashboard.Controllers
{
    public class JPOutsideNetworkingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JPOutsideNetworkings
        public ActionResult Index()
        {
            return View(db.JPOutsideNetworkings.ToList());
        }

        

        // GET: JPOutsideNetworkings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPOutsideNetworking jPOutsideNetworking = db.JPOutsideNetworkings.Find(id);
            if (jPOutsideNetworking == null)
            {
                return HttpNotFound();
            }
            return View(jPOutsideNetworking);
        }

        // GET: JPOutsideNetworkings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JPOutsideNetworkings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OutsideNetworkingID,Name,Position,Company,CompanyURL,LinkedIn,Location,Contact,Stack")] JPOutsideNetworking jPOutsideNetworking)
        {
            if (ModelState.IsValid)
            {
                db.JPOutsideNetworkings.Add(jPOutsideNetworking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jPOutsideNetworking);
        }

        // GET: JPOutsideNetworkings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPOutsideNetworking jPOutsideNetworking = db.JPOutsideNetworkings.Find(id);
            if (jPOutsideNetworking == null)
            {
                return HttpNotFound();
            }
            return View(jPOutsideNetworking);
        }

        // POST: JPOutsideNetworkings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OutsideNetworkingID,Name,Position,Company,CompanyURL,LinkedIn,Location,Contact,Stack")] JPOutsideNetworking jPOutsideNetworking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPOutsideNetworking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jPOutsideNetworking);
        }

        // GET: JPOutsideNetworkings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPOutsideNetworking jPOutsideNetworking = db.JPOutsideNetworkings.Find(id);
            if (jPOutsideNetworking == null)
            {
                return HttpNotFound();
            }
            return View(jPOutsideNetworking);
        }

        // POST: JPOutsideNetworkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPOutsideNetworking jPOutsideNetworking = db.JPOutsideNetworkings.Find(id);
            db.JPOutsideNetworkings.Remove(jPOutsideNetworking);
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

        
        public ActionResult _OutsideNetworking(string sortOrder)
        {
            List<JPOutsideNetworking> partialViewList = new List<JPOutsideNetworking>();
            partialViewList = db.JPOutsideNetworkings.ToList();

            ViewBag.NameSortParm = sortOrder == "studentName" ? "studentName_desc" : "studentName";
            ViewBag.PositionSortParm = sortOrder == "position" ? "position_desc" : "position";
            ViewBag.CompanySortParm = sortOrder == "company" ? "company_desc" : "company";
            ViewBag.LocationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.StackSortParm = sortOrder == "stack" ? "stack_desc" : "stack";

            switch (sortOrder)
            {
                default:
                case "studentName":
                    partialViewList = partialViewList.OrderBy(x => x.Name).ToList();
                    break;
                case "studentName_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Name).ToList();
                    break;
                case "position":
                    partialViewList = partialViewList.OrderBy(x => x.Position).ToList();
                    break;
                case "position_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Position).ToList();
                    break;
                case "company":
                    partialViewList = partialViewList.OrderBy(x => x.Company).ToList();
                    break;
                case "company_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Company).ToList();
                    break;
                case "location":
                    partialViewList = partialViewList.OrderBy(x => x.Location).ToList();
                    break;
                case "location_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Location).ToList();
                    break;
                case "stack":
                    partialViewList = partialViewList.OrderBy(x => x.Stack).ToList();
                    break;
                case "stack_desc":
                    partialViewList = partialViewList.OrderByDescending(x => x.Stack).ToList();
                    break;
            }

            return PartialView("_OutsideNetworking", partialViewList);
        }
    }
}
