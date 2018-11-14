using JobPlacementDashboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.ViewModels
{
    public class AnalyticsViewModel
    {

        [DisplayName("Student")]
        public JPStudent jpstudent { get; set; }
        public string jpName { get; set; }
        [DisplayName("Total Applications")]
        public int totalApplications { get; set; }
        [DisplayName("Company Name")]
        public string JPCompanyName { get; set; }
        [DisplayName("Job Category")]
        public JPJobCategory JPJobCategory { get; set; }
        [DisplayName("Salary")]
        public decimal JPSalary { get; set; }
        [DisplayName("Company City")]
        public string JPCompanyCity { get; set; }
        [DisplayName("Company State")]
        public JPCompanyState JPCompanyState { get; set; }
        [DisplayName("Hire Date")]
        public DateTime? JPHireDate { get; set; }
        [DisplayName("Average Salary"), DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal JPAverageSalary { get; set; }
        [DisplayName("Days on Job Placement")]
        public int JPDaysOnJobPlacement { get; set; }
        [DisplayName("Average Applications")]
        public double JPAverageApps { get; set; }
        [DisplayName("Common Job Category")]
        public JPJobCategory CommonJobCategory { get; set; }
        [DisplayName("Common Company")]
        public string CommonCompany { get; set; }
        [DisplayName("Company Careers Page")]
        public string JPCareersPage { get; set; }

        [DisplayName("Most Common Company Applied For")]
        public string CommonAppliedCompany { get; set; }
        [DisplayName("Common Application Catagory")]
        public JPJobCategory CommonAppCategory { get; set; }

        public AnalyticsViewModel(JPStudent jpstudent, int totalApplications, JPHire jPHire)
        {
            this.jpstudent = jpstudent;
            this.jpName = jpstudent.JPName;
            this.totalApplications = totalApplications;
            this.JPCompanyName = jPHire.JPCompanyName;
            this.JPSalary = jPHire.JPSalary;
            this.JPCompanyCity = jPHire.JPCompanyCity;
            this.JPCompanyState = jPHire.JPCompanyState;
            this.JPHireDate = jPHire.JPHireDate;
            this.JPCareersPage = jPHire.JPCareersPage;





        }

        public AnalyticsViewModel(decimal JPAverageSalary, double JPAverageApps, JPJobCategory CommonJobCategory, string CommonCompany, int JPDaysOnJobPlacement, string CommonAppliedCompany, JPJobCategory CommonAppCategory)
        {
            this.JPAverageSalary = JPAverageSalary;
            this.JPAverageApps = JPAverageApps;
            this.CommonJobCategory = CommonJobCategory;
            this.CommonCompany = CommonCompany;
            this.CommonAppliedCompany = CommonAppliedCompany;
            this.CommonAppCategory = CommonAppCategory;
            this.JPDaysOnJobPlacement = JPDaysOnJobPlacement;

        }

        public AnalyticsViewModel(decimal AverageSalary, double JPAverageApps, JPJobCategory commonJobCategory)
        {
            this.JPAverageSalary = JPAverageSalary;
            this.JPAverageApps = JPAverageApps;
            this.CommonJobCategory = commonJobCategory;
        }

        public AnalyticsViewModel(string fname, string company, string careerPage)
        {
            this.jpName = fname;
            this.JPCompanyName = company;
            this.JPCareersPage = careerPage;
        }


    }
}