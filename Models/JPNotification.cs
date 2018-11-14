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
    public class JPNotification
    {
        [Key]
        [DisplayName("Notification Id")]
        public int NotificationId { get; set; }
        [DisplayName("Graduate")]
        public bool Graduate { get; set; }
        [DisplayName("Hire")]
        public bool Hire { get; set; }
        [DisplayName("Notification Date")]
        public DateTime NotificationDate { get; set; }

        public virtual JPStudent JPStudent { get; set; }
    }
}
