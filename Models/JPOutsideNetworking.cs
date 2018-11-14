using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPlacementDashboard.Models
{
    public class JPOutsideNetworking
    {
        [Key]
        public int OutsideNetworkingID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string CompanyURL { get; set; }
        public string LinkedIn { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Stack { get; set; }

    }
}