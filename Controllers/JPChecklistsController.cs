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
    public class JPChecklistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JPChecklists
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            var jPChecklists = db.JPChecklists.Include(j => j.ApplicationUser);
            return View(jPChecklists.ToList());
        }
    
        // GET: JPChecklists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPChecklist jPChecklist = db.JPChecklists.Find(id);
            if (jPChecklist == null)
            {
                return HttpNotFound();
            }
            return View(jPChecklist);
        }

        // GET: JPChecklists/Create
        public ActionResult Create()
        {
            //ViewBag.ApplicationUserid = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            return View();
        }

        // POST: JPChecklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JPChecklistid,ApplicationUserid,JPBusinessCards,JPMeetups,JPUpdatedLinkedIn,JPUpdatedPortfolioSite,JPCleanGitHub,JpRoundTables")] JPChecklist jPChecklist)
        {
            if (ModelState.IsValid)
            
            {
               
                db.JPChecklists.Add(jPChecklist);
                db.SaveChanges();
                return RedirectToAction("StudentIndex","JPApplications");
            }

            //ViewBag.ApplicationUserid = new SelectList(db.ApplicationUsers, "Id", "FirstName", jPChecklist.ApplicationUserid);
            return View(jPChecklist);
        }

        // GET: JPChecklists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPChecklist jPChecklist = db.JPChecklists.Find(id);
            if (jPChecklist == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ApplicationUserid = new SelectList(db.ApplicationUsers, "Id", "FirstName", jPChecklist.ApplicationUserid);
            return View(jPChecklist);
        }

        // POST: JPChecklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JPChecklistid,ApplicationUserid,JPBusinessCards,JPMeetups,JPUpdatedLinkedIn,JPUpdatedPortfolioSite,JPCleanGitHub,JpRoundTables")] JPChecklist jPChecklist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jPChecklist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StudentIndex", "JPApplications");
            }
            //ViewBag.ApplicationUserid = new SelectList(db.ApplicationUsers, "Id", "FirstName", jPChecklist.ApplicationUserid);
            return View(jPChecklist);
        }

        // GET: JPChecklists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JPChecklist jPChecklist = db.JPChecklists.Find(id);
            if (jPChecklist == null)
            {
                return HttpNotFound();
            }
            return View(jPChecklist);
        }

        // POST: JPChecklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JPChecklist jPChecklist = db.JPChecklists.Find(id);
            db.JPChecklists.Remove(jPChecklist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult _MyChecklist()
        {
            string currentUserId = User.Identity.GetUserId();
            JPChecklist currentUser = db.JPChecklists.Where(a => a.ApplicationUserid == currentUserId).FirstOrDefault();
            return PartialView(currentUser);
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
