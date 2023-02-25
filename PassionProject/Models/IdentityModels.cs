using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PassionProject.Models
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
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Models are instatiated here as database tables

        public DbSet<cafe> Cafes { get; set; }

        public DbSet<coffee> Coffees { get; set; }

        public DbSet<review> Reviews { get; set; }


        //public DbSet<cafexcoffee> CafexCoffee { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

// how to do a migration:

// add the column under the idenity models class
// go to tools > Nuget package manager > package manager columns
// "add-migration <column-name>"
// this creates the migration and adds column/relationship to the database
// "update-database" to apply these changes
// you can update the database rows if you open the table through view > SQL server object explorer
