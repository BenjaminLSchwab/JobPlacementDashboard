using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public enum JPJobCategory
    {
        [Display(Name = "Project Management")]
        Project_Management,
        Development,
        [Display(Name = "Technical Support")]
        Tech_Support,
        [Display(Name = "Database Administration")]
        Database_Administration,
        [Display(Name = "Dev Ops")]
        Dev_Ops,
        QA,
        Intern,
        Other
    }
}