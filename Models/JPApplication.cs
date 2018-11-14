using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using JobPlacementDashboard.DAL;

namespace JobPlacementDashboard.Models
{
    public class JPApplication
    {
        [Key]
        [DisplayName("Application ID")]
        public Guid JPAppId { get; set; }
        [DisplayName("Company Name")]
        public string JPCompanyName { get; set; }
        [DisplayName("Job Title")]
        public string JPJobTitle { get; set; }
        [DisplayName("Job Category")]
        public JPJobCategory JPJobCategory { get; set; }
        [DisplayName("Work Location City")]
        public string JPCompanyCity { get; set; }
        [DisplayName("Work Location State")]
        public JPCompanyState JPCompanyState { get; set; }
        [DisplayName("Application Date")]
        public DateTime JPApplicationDate { get; set; }
        public string ApplicationUserId { get; set; }
        [DisplayName("Student Location")]
        public JPStudentLocation JPStudentLocation { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public bool? IsAppliedDateWithinOneWeekOfCurrentDate => DateCalculateHelper.CalculateIsObjectCreatedWithinOneWeekOfCurrentDate(this.JPApplicationDate);
        public bool? IsAppliedDateWithinOneMonthOfCurrentDate => DateCalculateHelper.CalculateIsObjectCreatedWithinOneMonthOfCurrentDate(this.JPApplicationDate);
        public bool? IsAppliedDateWithinOneYearOfCurrentDate => DateCalculateHelper.CalculateIsObjectCreatedWithinOneYearOfCurrentDate(this.JPApplicationDate);
    }
}