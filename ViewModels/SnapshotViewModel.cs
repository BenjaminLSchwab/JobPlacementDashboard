using JobPlacementDashboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.ViewModels
{

    public class SnapshotViewModel
    {
        //General
        [DisplayName("New Students")]
        public List<JPStudent> NewStudents { get; set; }
        [DisplayName("Weekly Applications")]
        public List<JPApplication> WeeklyApplications { get; set; }
        [DisplayName("Weekly Hires")]
        public List<JPHire> WeeklyHires { get; set; }
        [DisplayName("Total Weekly Applications")]
        public int TotalWeeklyApplications { get; set; }
        [DisplayName("Total Weekly Hires")]
        public int TotalWeeklyHires { get; set; }
        [DisplayName("Total Students")]
        public int TotalStudents { get; set; }
        [DisplayName("Unhired Graduates")]
        public int UnhiredGraduates { get; set; }
        [DisplayName("Average Days in Job Placement per Student")]              
        public int AvgDaysInJP { get; set; }
        [DisplayName("Monthly Hires")]
        public List<JPHire> MonthlyHires { get; set; }
        [DisplayName("Yearly Hires")]
        public List<JPHire> YearlyHires { get; set; }
        [DisplayName("Total Monthly Students")]
        public int TotalMonthlyHires { get; set; }
        [DisplayName("Total Yearly Students")]
        public int TotalYearlyHires { get; set; }
        

        //Portland
        [DisplayName("New Students")]
        public List<JPStudent> NewStudents_Portland { get; set; }
        [DisplayName("Weekly Applications")]
        public List<JPApplication> WeeklyApplications_Portland { get; set; }
        [DisplayName("Weekly Hires")]
        public List<JPHire> WeeklyHires_Portland { get; set; }
        [DisplayName("Total Weekly Applications")]
        public int TotalWeeklyApplications_Portland { get; set; }
        [DisplayName("Total Weekly Hires")]
        public int TotalWeeklyHires_Portland { get; set; }
        [DisplayName("Total Students")]
        public int TotalStudents_Portland { get; set; }
        [DisplayName("Unhired Graduates")]
        public int UnhiredGraduates_Portland { get; set; }
        [DisplayName("Average Days in Job Placement per Student")]              
        public int PortlandAvgDaysInJP { get; set; }
        [DisplayName("Total Days in Job Placement for All Students")]
        public int PortlandDaysInJP { get; set; }
        [DisplayName("Monthly Hires")]
        public List<JPHire> MonthlyHires_Portland { get; set; }
        [DisplayName("Yearly Hires")]
        public List<JPHire> YearlyHires_Portland { get; set; }
        [DisplayName("Total Monthly Students")]
        public int TotalMonthlyHires_Portland { get; set; }
        [DisplayName("Total Yearly Students")]
        public int TotalYearlyHires_Portland { get; set; }


        //Denver
        [DisplayName("New Students")]
        public List<JPStudent> NewStudents_Denver { get; set; }
        [DisplayName("Weekly Applications")]
        public List<JPApplication> WeeklyApplications_Denver { get; set; }
        [DisplayName("Weekly Hires")]
        public List<JPHire> WeeklyHires_Denver { get; set; }
        [DisplayName("Total Weekly Applications")]
        public int TotalWeeklyApplications_Denver { get; set; }
        [DisplayName("Total Weekly Hires")]
        public int TotalWeeklyHires_Denver { get; set; }
        [DisplayName("Total Students")]
        public int TotalStudents_Denver { get; set; }
        [DisplayName("Unhired Graduates")]
        public int UnhiredGraduates_Denver { get; set; }
        [DisplayName("Average Days in Job Placement per Student")]           
        public int DenverAvgDaysInJP { get; set; }
        [DisplayName("Total Days in Job Placement for All Students")]
        public int DenverDaysInJP { get; set; }
        [DisplayName("Monthly Hires")]
        public List<JPHire> MonthlyHires_Denver { get; set; }
        [DisplayName("Yearly Hires")]
        public List<JPHire> YearlyHires_Denver { get; set; }
        [DisplayName("Total Monthly Students")]
        public int TotalMonthlyHires_Denver { get; set; }
        [DisplayName("Total Yearly Students")]
        public int TotalYearlyHires_Denver { get; set; }


        //Seattle
        [DisplayName("New Students")]
        public List<JPStudent> NewStudents_Seattle { get; set; }
        [DisplayName("Weekly Applications")]
        public List<JPApplication> WeeklyApplications_Seattle { get; set; }
        [DisplayName("Weekly Hires")]
        public List<JPHire> WeeklyHires_Seattle { get; set; }
        [DisplayName("Total Weekly Applications")]
        public int TotalWeeklyApplications_Seattle { get; set; }
        [DisplayName("Total Weekly Hires")]
        public int TotalWeeklyHires_Seattle { get; set; }
        [DisplayName("Total Students")]
        public int TotalStudents_Seattle { get; set; }
        [DisplayName("Unhired Graduates")]
        public int UnhiredGraduates_Seattle { get; set; }
        [DisplayName("Average Days in Job Placement per Student")]
        public int SeattleAvgDaysInJP { get; set; }
        [DisplayName("Total Days in Job Placement for All Students")]
        public int SeattleDaysInJP { get; set; }
        [DisplayName("Monthly Hires")]
        public List<JPHire> MonthlyHires_Seattle { get; set; }
        [DisplayName("Yearly Hires")]
        public List<JPHire> YearlyHires_Seattle { get; set; }
        [DisplayName("Total Monthly Students")]
        public int TotalMonthlyHires_Seattle{ get; set; }
        [DisplayName("Total Yearly Students")]
        public int TotalYearlyHires_Seattle { get; set; }


        //Remote
        [DisplayName("New Students")]
        public List<JPStudent> NewStudents_Remote { get; set; }
        [DisplayName("Weekly Applications")]
        public List<JPApplication> WeeklyApplications_Remote { get; set; }
        [DisplayName("Weekly Hires")]
        public List<JPHire> WeeklyHires_Remote { get; set; }
        [DisplayName("Total Weekly Applications")]
        public int TotalWeeklyApplications_Remote { get; set; }
        [DisplayName("Total Weekly Hires")]
        public int TotalWeeklyHires_Remote { get; set; }
        [DisplayName("Total Students")]
        public int TotalStudents_Remote { get; set; }
        [DisplayName("Unhired Graduates")]
        public int UnhiredGraduates_Remote { get; set; }
        [DisplayName("Average Days in Job Placement per Student")]
        public int RemoteAvgDaysInJP { get; set; }
        [DisplayName("Total Days in Job Placement for All Students")]
        public int RemoteDaysInJP { get; set; }
        [DisplayName("Monthly Hires")]
        public List<JPHire> MonthlyHires_Remote { get; set; }
        [DisplayName("Yearly Hires")]
        public List<JPHire> YearlyHires_Remote { get; set; }
        [DisplayName("Total Monthly Students")]
        public int TotalMonthlyHires_Remote { get; set; }
        [DisplayName("Total Yearly Students")]
        public int TotalYearlyHires_Remote { get; set; }

        public SnapshotViewModel(
            List<JPStudent> newStudents,
            List<JPHire> weeklyHires, List<JPHire> monthlyHires, List<JPHire> yearlyHires, 
            int totalWeeklyApps, int totalMonthlyApps, int totalYearlyApps,
            int totalWeeklyHires, int totalMonthlyHires, int totalYearlyHires,
            int totalStudents, int unhiredGraduates,
            List<JPStudent> newStudents_portland, 
            List<JPHire> weeklyHires_portland, List <JPHire> monthlyHires_Portland, List<JPHire> yearlyHires_Portland,
            int totalWeeklyApps_portland, int totalMonthlyApps_Portland, int totalYearlyApps_Portland,
            int totalWeeklyHires_portland, int totalMonthlyHires_Portland, int totalYearlyHires_Portland,
            int totalStudents_portland, int unhiredGraduates_portland,
            List<JPStudent> newStudents_denver, 
            List<JPHire> weeklyHires_denver, List<JPHire> monthlyHires_Denver, List<JPHire> yearlyHires_Denver,
            int totalWeeklyApps_denver, int totalMonthlyApps_Denver, int totalYearlyApps_Denver,
            int totalWeeklyHires_denver, int totalMonthlyHires_Denver, int totalYearlyHires_Denver,
            int totalStudents_denver, int unhiredGraduates_denver, 
            List<JPStudent> newStudents_seattle, 
            List<JPHire> weeklyHires_seattle, List<JPHire> monthlyHires_Seattle, List<JPHire> yearlyHires_Seattle,
            int totalWeeklyApps_seattle, int totalMonthlyApps_Seattle, int totalYearlyApps_Seattle,
            int totalWeeklyHires_seattle, int totalMonthlyHires_Seattle, int totalYearlyHires_Seattle,
            int totalStudents_seattle, int unhiredGraduates_seattle,
            List<JPStudent> newStudents_remote, 
            List<JPHire> weeklyHires_remote, List<JPHire> monthlyHires_Remote, List<JPHire> yearlyHires_Remote,
            int totalWeeklyApps_remote, int totalMonthlyApps_Remote, int totalYearlyApps_Remote,
            int totalWeeklyHires_remote, int totalMonthlyHires_Remote, int totalYearlyHires_Remote,
            int totalStudents_remote, int unhiredGraduates_remote,
            int avgDaysInJP, int portlandAvgDaysInJP, int denverAvgDaysInJP, int seattleAvgDaysInJP, int remoteAvgDaysInJP, int  portlandDaysInJP, int denverDaysInJP, int seattleDaysInJP, int remoteDaysInJP
            )
        {
            NewStudents = newStudents;
            WeeklyHires = weeklyHires;
            TotalWeeklyApplications = totalWeeklyApps;
            TotalWeeklyHires = totalWeeklyHires;
            TotalStudents = totalStudents;
            UnhiredGraduates = unhiredGraduates;
            MonthlyHires = monthlyHires;
            YearlyHires = yearlyHires;
            TotalMonthlyHires = totalMonthlyHires;
            TotalYearlyHires = totalYearlyHires;


            NewStudents_Portland = newStudents_portland;
            WeeklyHires_Portland = weeklyHires_portland;
            TotalWeeklyApplications_Portland = totalWeeklyApps_portland;
            TotalWeeklyHires_Portland = totalWeeklyHires_portland;
            TotalStudents_Portland = totalStudents_portland;
            UnhiredGraduates_Portland = unhiredGraduates_portland;
            MonthlyHires_Portland = monthlyHires_Portland;
            YearlyHires_Portland = yearlyHires_Portland;
            TotalMonthlyHires_Portland = totalMonthlyHires_Portland;
            TotalYearlyHires_Portland = totalYearlyHires_Portland;

            NewStudents_Denver = newStudents_denver;
            WeeklyHires_Denver = weeklyHires_denver;
            TotalWeeklyApplications_Denver = totalWeeklyApps_denver;
            TotalWeeklyHires_Denver = totalWeeklyHires_denver;
            TotalStudents_Denver = totalStudents_denver;
            UnhiredGraduates_Denver = unhiredGraduates_denver;
            MonthlyHires_Denver = monthlyHires_Denver;
            YearlyHires_Denver = yearlyHires_Denver;
            TotalMonthlyHires_Denver = totalMonthlyHires_Denver;
            TotalYearlyHires_Denver = totalYearlyHires_Denver;

            NewStudents_Seattle = newStudents_seattle;
            WeeklyHires_Seattle = weeklyHires_seattle;
            TotalWeeklyApplications_Seattle = totalWeeklyApps_seattle;
            TotalWeeklyHires_Seattle = totalWeeklyHires_seattle;
            TotalStudents_Seattle = totalStudents_seattle;
            UnhiredGraduates_Seattle = unhiredGraduates_seattle;
            MonthlyHires_Seattle = monthlyHires_Seattle;
            YearlyHires_Seattle = yearlyHires_Seattle;
            TotalMonthlyHires_Seattle = totalMonthlyHires_Seattle;
            TotalYearlyHires_Seattle = totalYearlyHires_Seattle;

            NewStudents_Remote = newStudents_remote;
            WeeklyHires_Remote = weeklyHires_remote;
            TotalWeeklyApplications_Remote = totalWeeklyApps_remote;
            TotalWeeklyHires_Remote = totalWeeklyHires_remote;
            TotalStudents_Remote = totalStudents_remote;
            UnhiredGraduates_Remote = unhiredGraduates_remote;
            MonthlyHires_Remote = monthlyHires_Remote;
            YearlyHires_Remote = yearlyHires_Remote;
            TotalMonthlyHires_Remote = totalMonthlyHires_Remote;
            TotalYearlyHires_Remote = totalYearlyHires_Remote;

            AvgDaysInJP = avgDaysInJP;
            PortlandAvgDaysInJP = portlandAvgDaysInJP;
            DenverAvgDaysInJP = denverAvgDaysInJP;
            SeattleAvgDaysInJP = seattleAvgDaysInJP;
            RemoteAvgDaysInJP = remoteAvgDaysInJP;
            PortlandDaysInJP = portlandDaysInJP;
            DenverDaysInJP = denverDaysInJP;
            SeattleDaysInJP = seattleDaysInJP;
            RemoteDaysInJP = remoteDaysInJP;
            //removed TotalDaysInJP = totalDaysInJP
        }
    }
}