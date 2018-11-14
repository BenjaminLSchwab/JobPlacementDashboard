using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPlacementDashboard.Models
{
    public class JPBulletin
    {
        [Key]
        public int BulletinId { get; set; }
        [AllowHtml]
        public string BulletinBody { get; set; }
        [DisplayName("Tag a Category")]
        public BulletinCategoryEnum BulletinCategoryEnum { get; set; }
        public DateTime BulletinDate { get; set; }
    }
}