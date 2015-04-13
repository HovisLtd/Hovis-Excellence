using Hovis.Excellence.Web.Models;
using System.Data.Entity.Migrations;

namespace Hovis.Excellence.Web.Migrations
{
    internal partial class Configuration
    {
        private static void SeedApplications(ApplicationDbContext context)
        {
            var items = new[]
            {
                new HovisExcellenceApplication {Id = 1, Name = "Health & Safety", ApplicationKey = "health-and-safety"},
                new HovisExcellenceApplication {Id = 2, Name = "Customer Loyalty", ApplicationKey = "customer-loyalty"},
                new HovisExcellenceApplication
                {
                    Id = 3,
                    Name = "Environmental Stewardship",
                    ApplicationKey = "environment"
                },
            };

            //this currently UPDATES it if item with ID exists... so if there's something in there that's changed, it'll be overwritten
            foreach (var item in items)
                context.Applications.AddOrUpdate(app => app.Id, item);
        }
    }
}