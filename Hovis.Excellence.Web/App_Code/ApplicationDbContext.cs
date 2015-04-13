using Hovis.Excellence.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Hovis.Excellence.Web
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<SiteCategory> SiteCategories { get; set; }

        public DbSet<PersonRole> PersonRoles { get; set; }

        public DbSet<Site> Sites { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<HovisExcellenceApplication> Applications { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<DocumentLinks> DocumentLinks { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<DocumentCategory> DocumentCategories { get; set; }

        public DbSet<DocumentTabs> DocumentTabs { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}