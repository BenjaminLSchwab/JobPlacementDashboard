using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public class JPChecklist
    {
        public int JPChecklistid { get; set; }
        public string ApplicationUserid { get; set; }
        public bool JPBusinessCards { get; set; }
        public bool JPMeetups { get; set; }
        public bool JPUpdatedLinkedIn { get; set; }
        public bool JPUpdatedPortfolioSite { get; set; }
        public bool JPCleanGitHub { get; set; }
        public bool JpRoundTables { get; set; }

        public virtual ApplicationUser ApplicationUser{ get; set; }

    }
}