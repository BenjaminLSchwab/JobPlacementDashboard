using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPlacementDashboard.Models;
using Microsoft.AspNet.Identity;

namespace JobPlacementDashboard.Controllers
{
    public class JPCurrentJobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JPCurrentJobs
        public ActionResult Index()
        {
            return View(db.JPCurrentJobs.ToList());
        }

        // GET: JPCurrentJobs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPCurrentJob jPCurrentJob = db.JPCurrentJobs.Find(id);
            if (jPCurrentJob == null)
            {
                return HttpNotFound();
            }
            return View(jPCurrentJob);
        }

        // GET: JPCurrentJobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JPCurrentJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPCurrentId,JPCompanyName,JPJobTitle,JPJobCategory,JPSalary,JPCompanyCity,JPCompanyState,JPCareersPage")] JPCurrentJob jPCurrentJob)
        {
            if (ModelState.IsValid)
            {
                // Grabs the active users ID and uses it to identify the users row in JPHires table to edit JPSecondJob from false to true.
                string userID = User.Identity.GetUserId();
                JPHire jpHire = db.JPHires.Where(x => x.ApplicationUserId == userID).FirstOrDefault();
                jpHire.JPSecondJob = true;
                db.Entry(jpHire).State = EntityState.Modified;

                //Assigns an ID and ApplicationUserId to JPCurrentJobs.
                jPCurrentJob.JPCurrentId = Guid.NewGuid();
                jPCurrentJob.ApplicationUserId = userID;
                db.JPCurrentJobs.Add(jPCurrentJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jPCurrentJob);
        }

        // GET: JPCurrentJobs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPCurrentJob jPCurrentJob = db.JPCurrentJobs.Find(id);
            if (jPCurrentJob == null)
            {
                return HttpNotFound();
            }
            return View(jPCurrentJob);
        }

        // POST: JPCurrentJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPCurrentId,JPCompanyName,JPJobTitle,JPJobCategory,JPSalary,JPCompanyCity,JPCompanyState,JPCareersPage")] JPCurrentJob jPCurrentJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPCurrentJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jPCurrentJob);
        }

        // GET: JPCurrentJobs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPCurrentJob jPCurrentJob = db.JPCurrentJobs.Find(id);
            if (jPCurrentJob == null)
            {
                return HttpNotFound();
            }
            return View(jPCurrentJob);
        }

        // POST: JPCurrentJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            JPCurrentJob jPCurrentJob = db.JPCurrentJobs.Find(id);
            db.JPCurrentJobs.Remove(jPCurrentJob);
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
