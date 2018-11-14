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
    public class JPNotificationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: JPNotifications
        public ActionResult Update()
        {
            var notifications = from n in db.JPNotifications
                           select n;
            return View(notifications);
        }
    }
}