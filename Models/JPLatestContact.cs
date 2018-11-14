using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPlacementDashboard.Models
{
    public class JPLatestContact
    {
        internal static Guid id;

        [Key]
        public Guid JPLatestContactId { get; set; }
        [DisplayName("Latest Contact Date")]
        public DateTime JPLatestContactDate { get; set; }
        public string ApplicationUserId { get; set; }

        public int CalculateLastContactDate
        {
            get
            {
                int days = ((DateTime.Now - JPLatestContactDate).Days);
                return (days);
            }

        }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}