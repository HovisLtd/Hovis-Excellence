using Hovis.Excellence.Web.Models;
using System.Data.Entity.Migrations;

namespace Hovis.Excellence.Web.Migrations
{
    internal partial class Configuration
    {
        private static void SeedDocumentCategories(ApplicationDbContext context)
        {
            var items = new[]
            {
                new DocumentCategory {Id = 1, Name = "Policy or Procedure"},
                new DocumentCategory {Id = 2, Name = "Working Template or Form"},
                new DocumentCategory {Id = 3, Name = "Training or Reference"},
            };

            //this currently UPDATES it if item with ID exists... so if there's something in there that's changed, it'll be overwritten
            foreach (var item in items)
                context.DocumentCategories.AddOrUpdate(app => app.Id, item);
        }
    }
}