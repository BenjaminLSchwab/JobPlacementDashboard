using JobPlacementDashboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.ViewModels
{
    public class JPStudentRundown
    {

        //public JPStudent jpstudent { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Email")]
        public string StudentEmail { get; set; }
        [DisplayName("Location")]
        public JPStudentLocation StudentLocation { get; set; }
        [DisplayName("Start Date")]
        public int StudentDayCount { get; set; }
        [DisplayName("LinkedIn")]
        public string StudentLinkedIn { get; set; }
        [DisplayName("Portfolio")]
        public string StudentPortfolio { get; set; }
        [DisplayName("TotalApplications")]
        public int TotalApplications { get; set; }
        [DisplayName("Applications this Week")]
        public int TotalApplicationsThisWeek { get; set; }
        [DisplayName("Job Placement ID")]
        public int JPStudentId { get; set; }
        [DisplayName("Graduated")]
        public bool JPGraduated { get; set; }
        [DisplayName("Checklist Completion")]
        public int CheckListStatus { get; set; }
        [DisplayName(" Last contact")]
        public int CalculateLastContactDate { get; set; }
        [DisplayName("Buisness Cards")]
        public bool JPBusinessCards { get; set; }
        [DisplayName("Meetups")]
        public bool JPMeetUps { get; set; }
        [DisplayName("Updated LinkedIn")]
        public bool JPUpdatedLinkedIn { get; set; }
        [DisplayName("Updated portfolio Site")]
        public bool JPUpdatedPortfolioSite { get; set; }
        [DisplayName("Clean GitHub")]
        public bool JPCleanGitHub { get; set; }
        [DisplayName("RoundTables")]
        public bool JpRoundTables { get; set; }
        [DisplayName("Start Date")]
        public DateTime JPStartDate { get; set; }
        [DisplayName("Contact")]
        public bool JPContact { get; set; }
        //:wanting the edit age after completion to pull from the jpstudentsrundownviewmodel..
        public JPStudentRundown(JPStudent student, JPChecklist checklist, int totalApplications, int totalApplicationsThisWeek, int calculateLastContactDate, int checkListStatus)
        {
            StudentName = student.JPName;
            StudentEmail = student.JPEmail;
            StudentLocation = student.JPStudentLocation;
            StudentDayCount = student.DaysSinceStart;
            JPStartDate = student.JPStartDate;
            StudentLinkedIn = student.JPLinkedIn;
            StudentPortfolio = student.JPPortfolio;
            TotalApplications = totalApplications;
            TotalApplicationsThisWeek = totalApplicationsThisWeek;
            JPStudentId = student.JPStudentId;
            JPGraduated = student.JPGraduated;
            CalculateLastContactDate = calculateLastContactDate;
            CheckListStatus = checkListStatus;
            JPBusinessCards = checklist.JPBusinessCards;
            JPMeetUps = checklist.JPMeetups;
            JPUpdatedLinkedIn = checklist.JPUpdatedLinkedIn;
            JPUpdatedPortfolioSite = checklist.JPUpdatedPortfolioSite;
            JPCleanGitHub = checklist.JPCleanGitHub;
            JpRoundTables = checklist.JpRoundTables;
        }
        public JPStudentRundown(JPStudent student)
        {
            StudentName = student.JPName;
            StudentEmail = student.JPEmail;
            StudentLocation = student.JPStudentLocation;
            StudentDayCount = student.DaysSinceStart;
            StudentLinkedIn = student.JPLinkedIn;
            StudentPortfolio = student.JPPortfolio;
            JPStudentId = student.JPStudentId;
        }
    }
}