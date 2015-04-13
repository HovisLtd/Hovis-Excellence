using Hovis.Excellence.Web.Models;
using System.Data.Entity.Migrations;

namespace Hovis.Excellence.Web.Migrations
{
    internal partial class Configuration
    {
        private static void SeedPersonRoles(ApplicationDbContext context)
        {
            var items = new[]
            {
                new PersonRole {Id = 1, Name = "Operations Director"},
                new PersonRole {Id = 2, Name = "Head of Operations"},
                new PersonRole {Id = 3, Name = "Head of TPM"},
                new PersonRole {Id = 4, Name = "Head of H&S and Risk"},
                new PersonRole {Id = 5, Name = "Head of Operations Excellence"},
                new PersonRole {Id = 6, Name = "Head of Investment"},
                new PersonRole {Id = 7, Name = "Head of Food Safety and Quality"},
                new PersonRole {Id = 8, Name = "Site Director"},
                new PersonRole {Id = 9, Name = "Operations Manager"},
                new PersonRole {Id = 10, Name = "Engineering Manager"},
                new PersonRole {Id = 11, Name = "Technical Manager"},
                new PersonRole {Id = 12, Name = "H&S Manager"},
                new PersonRole {Id = 13, Name = "Supply Chain Manager"},
                new PersonRole {Id = 14, Name = "Site Manager"},
                new PersonRole {Id = 15, Name = "Continuous Improvement Leader"},
                new PersonRole {Id = 16, Name = "FLM"},
                new PersonRole {Id = 17, Name = "Operator"},
                new PersonRole {Id = 18, Name = "Engineer"},
            };

            //this currently UPDATES it if item with ID exists... so if there's something in there that's changed, it'll be overwritten
            foreach (var item in items)
                context.PersonRoles.AddOrUpdate(app => app.Id, item);
        }
    }
}