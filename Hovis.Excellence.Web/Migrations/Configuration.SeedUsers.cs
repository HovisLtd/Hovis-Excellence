using Hovis.Excellence.Web.Identity;
using Hovis.Excellence.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;

namespace Hovis.Excellence.Web.Migrations
{
    internal partial class Configuration
    {
        // array with users that will be created for this application on startup
        private static readonly UserToCreate[] UsersToCreate =
        {
            new UserToCreate("Alex", "Brown", "alex.brown@hovis.co.uk", "Admin"),
            new UserToCreate("Andy", "Taylor", "andy.taylor@hovis.co.uk", "Admin"),
            //other users to create can be created in this array like this:
            //new UserToCreate("John", "Smith", "john.smith@hovis.co.uk", "Admin", "AnotherRole", "AndAnotherRole"),
        };

        public static void SeedUsers(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            foreach (var userToCreate in UsersToCreate)
            {
                //check to see if the user already exists
                var user = userManager.FindByEmail(userToCreate.EmailAddress);

                //if user isn't found, create it
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Email = userToCreate.EmailAddress,
                        FirstName = userToCreate.FirstName,
                        LastName = userToCreate.LastName
                    };

                    userManager.Create(user);
                }

                // Add user to specified roles
                foreach (var roleToAddUserTo in userToCreate.Roles)
                {
                    var rolesForUser = userManager.GetRoles(user.Id);

                    if (!rolesForUser.Contains(roleToAddUserTo))
                        userManager.AddToRole(user.Id, roleToAddUserTo);
                }
            }
        }

        //private class only used within seeding functionatly to hold info on user
        //makes it easy to use array in here
        private class UserToCreate
        {
            public UserToCreate(string firstName, string lastName, string emailAddress, params string[] roles)
            {
                FirstName = firstName;
                LastName = lastName;
                EmailAddress = emailAddress;
                Roles = roles;
            }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string EmailAddress { get; set; }

            public string[] Roles { get; set; }
        }
    }
}