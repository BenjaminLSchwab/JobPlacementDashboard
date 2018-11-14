using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public class JPCurrentJob
    {

        [Key]
        public Guid JPCurrentId { get; set; }
        public string JPCompanyName { get; set; }
        public string JPJobTitle { get; set; }
        public JPJobCategory JPJobCategory { get; set; }
        public decimal JPSalary { get; set; }
        public string JPCompanyCity { get; set; }
        public JPCompanyState JPCompanyState { get; set; }
        public string JPCareersPage { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}