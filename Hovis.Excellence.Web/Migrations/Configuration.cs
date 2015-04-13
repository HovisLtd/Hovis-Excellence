using System.Web;

namespace Hovis.Excellence.Web.Migrations
{
    using System.Data.Entity.Migrations;

    internal partial class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //SeedRoles(context);
            //SeedUsers(context);

            //SeedApplications(context);
            //SeedDocumentCategories(context);
            //SeedDocumentTypes(context);

            //SeedSiteCategories(context);
            //SeedPersonRoles(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}