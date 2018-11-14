using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public enum JPStudentLocation
    {
        [Display(Name = "Seattle Local")]
        SeattleLocal,
        [Display(Name = "Denver Local")]
        DenverLocal,
        [Display(Name = "Seattle Remote")]
        SeattleRemote,
        [Display(Name = "Denver Remote")]
        DenverRemote,
        [Display(Name = "Portland Remote")]
        PortlandRemote,
        Remote,
        [Display(Name = "Portland Local")]
        PortlandLocal

    }
}