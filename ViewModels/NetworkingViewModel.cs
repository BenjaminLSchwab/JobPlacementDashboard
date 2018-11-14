using JobPlacementDashboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.ViewModels
{
    public class NetworkingViewModel
    {

        [DisplayName("Student Name")]
        public string JPName { get; set; }
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
        //Adding line so we can see hire dates
        [DisplayName("Hire Date")]        
        public DateTime JPHireDate { get; set; }
        //Added
        [DisplayName("LinkedIn Profile")]
        public string JPLinkedIn { get; set; }
        [DisplayName("Company Careers Page")]
        public string JPCareersPage { get; set; }

        public NetworkingViewModel(JPStudent jpstudent, JPHire jphire)
        {
            JPName = jpstudent.JPName;
            JPLinkedIn = jpstudent.JPLinkedIn;
            JPCompanyName = jphire.JPCompanyName;
            JPJobTitle = jphire.JPJobTitle;
            JPJobCategory = jphire.JPJobCategory;
            JPCompanyCity = jphire.JPCompanyCity;
            JPCompanyState = jphire.JPCompanyState;
            JPCareersPage = jphire.JPCareersPage;
            //Adding Line so we can see hire dates
            JPHireDate = jphire.JPHireDate;
            //Added
            JPCareersPage = jphire.JPCareersPage;

        }

        public NetworkingViewModel(JPStudent jpstudent, JPCurrentJob jpcurrentjob)
        {
            JPName = jpstudent.JPName;
            JPLinkedIn = jpstudent.JPLinkedIn;
            JPCompanyName = jpcurrentjob.JPCompanyName;
            JPJobTitle = jpcurrentjob.JPJobTitle;
            JPJobCategory = jpcurrentjob.JPJobCategory;
            JPCompanyCity = jpcurrentjob.JPCompanyCity;
            JPCompanyState = jpcurrentjob.JPCompanyState;
            JPCareersPage = jpcurrentjob.JPCareersPage;
        }
    }
}