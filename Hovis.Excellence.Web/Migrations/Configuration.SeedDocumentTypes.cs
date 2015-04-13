using Hovis.Excellence.Web.Models;
using System.Data.Entity.Migrations;

namespace Hovis.Excellence.Web.Migrations
{
    internal partial class Configuration
    {
        private static void SeedDocumentTypes(ApplicationDbContext context)
        {
            var items = new[]
            {
                new DocumentType {Id = 1, Name = "Policy Manual"},
                new DocumentType {Id = 2, Name = "Procedures Manual"},
                new DocumentType {Id = 3, Name = "Working Templates & Tools"},
                new DocumentType {Id = 4, Name = "Training & Reference"},
                new DocumentType {Id = 5, Name = "Best Practice"},
            };

            //this currently UPDATES it if item with ID exists... so if there's something in there that's changed, it'll be overwritten
            foreach (var item in items)
                context.DocumentTypes.AddOrUpdate(app => app.Id, item);
        }
    }
}