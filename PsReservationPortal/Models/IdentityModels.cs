using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PsReservationPortal.Models
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
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserRegistrationInfoModel> UserRegistrationInfo { get; set; }
        public DbSet<UserExtraInfoModel> UserExtraInfo { get; set; }
        public DbSet<CompanyModel> Company { get; set; }
        public DbSet<OutletModel> Outlet { get; set; }
        public DbSet<ReservationSettingModel> ReservationSetting { get; set; }
        public DbSet<OperationSettingModel> OperationSetting { get; set; }
        public DbSet<TableModel> Table { get; set; }
        public DbSet<DinerModel> Diner { get; set; }
        public DbSet<OutletOffDayModel> OutletOffDay { get; set; }
        public DbSet<OutletWorkingDateModel> OutletWorkingDate { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}