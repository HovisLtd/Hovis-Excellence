using Hovis.Excellence.Web.Models;
using System.Data.Entity.Migrations;

namespace Hovis.Excellence.Web.Migrations
{
    internal partial class Configuration
    {
        private static void SeedSiteCategories(ApplicationDbContext context)
        {
            var items = new[]
            {
                new SiteCategory {Id = 1, Name = "Bakeries"},
                new SiteCategory {Id = 2, Name = "Mills"},
                new SiteCategory {Id = 3, Name = "Distribution"},
                new SiteCategory {Id = 4, Name = "Offices"},
            };

            //this currently UPDATES it if item with ID exists... so if there's something in there that's changed, it'll be overwritten
            foreach (var item in items)
                context.SiteCategories.AddOrUpdate(app => app.Id, item);
        }
    }
}