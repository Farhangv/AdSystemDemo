using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AdSystem.Models;

namespace AdSystem.MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class AdDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<RentAd> RentAds { get; set; }
        public virtual DbSet<SaleAd> SaleAds { get; set; }

        public static AdDbContext Create()
        {
            return new AdDbContext();
        }
    }
}