using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JobPlacementDashboard.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("JobPlacementDbContext", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<JPStudent> JPStudents { get; set; }
        public DbSet<JPApplication> JPApplications { get; set; }
        public DbSet<JPHire> JPHires { get; set; }
        public DbSet<JPBulletin> JPBulletins { get; set; }
        public DbSet<JPCurrentJob> JPCurrentJobs { get; set; }
        public DbSet<JPLatestContact> JPLatestContacts { get; set; }
        public DbSet<JPChecklist> JPChecklists { get; set; }
        public DbSet<JPMeetupEvent> JPMeetupEvents { get; set; }
        public DbSet<JPNotification> JPNotifications { get; set; }
        public DbSet<JPOutsideNetworking> JPOutsideNetworkings { get; set; }
    }
}