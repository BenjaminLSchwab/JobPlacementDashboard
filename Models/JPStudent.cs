using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public class JPStudent
    {
        [DisplayName("Student ID")]
        public int JPStudentId { get; set; }
        [DisplayName("Student Name")]
        public string JPName { get; set; }
        [DisplayName("Email")]
        public string JPEmail { get; set; }
        [DisplayName("Location")]
        public JPStudentLocation JPStudentLocation { get; set; }
        [DisplayName("Start Date")]
        public DateTime JPStartDate { get; set; }
        [DisplayName("LinkedIn Profile")]
        public string JPLinkedIn { get; set; }
        [DisplayName("Portfolio Website")]
        public string JPPortfolio { get; set; }
        [DisplayName("GitHub Link")]
        public string JPGithubLink { get; set; }
        [DisplayName("Contact")]
        public bool JPContact { get; set; }
        [DisplayName("Graduated")]
        public bool JPGraduated { get; set; }
        [DisplayName("Hired")]
        public bool JPHired { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        [NotMapped]
        public bool? IsStartDateWithinOneWeekOfCurrentDate
        {
            get
            {
                return DateCalculateHelper.CalculateIsObjectCreatedWithinOneWeekOfCurrentDate(this.JPStartDate);
            }
        }

        public int DaysSinceStart
        {
            get
            {
                int days = ((DateTime.Now - JPStartDate).Days);
                return (days);
            }
        }

        public virtual IList<JPApplication> JPApplications { get; set; }

        public string returnWithSpaces(string location)
        {
            string newString = System.Text.RegularExpressions.Regex.Replace(location, "[A-Z]", " $0");
            newString = newString.Trim();
            return newString;
        }

    }
}