using Hovis.Excellence.Web.Areas.MasterData.ViewModels;
using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    [Authorize(Roles = "Admin, Site Admin")]
    public class PersonController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            //if you want to order by something other than first name later, i would look at ordering client site
            //take a look at the UI project (the theme) - data tables - these can be sorted client side with javascript

            var peopleBySite = _db.People
                .OrderBy(x => x.FirstName)
                .GroupBy(x => x.Site.Name)
                .ToList()
                .Select(x => new PeopleListViewModel.NamedList { Name = x.Key, People = x.ToList() });

            var peopleByRole = _db.People
                .OrderBy(x => x.FirstName)
                .GroupBy(x => x.Role.Name)
                .ToList()
                .Select(x => new PeopleListViewModel.NamedList { Name = x.Key, People = x.ToList() });

            var viewModel = new PeopleListViewModel
            {
                ByRole = peopleByRole,
                BySite = peopleBySite
            };

            return View(viewModel);
        }

        public ActionResult New()
        {
            var personRoles = _db.PersonRoles.ToList();

            if (!personRoles.Any())
            {
                TempData["error"] =
                    string.Format(
                        @"No roles have been created. <a href=""{0}"" class=""alert-link"" > Click here </a> to add a role now.",
                        Url.Action("New", "PersonRole"));

                return RedirectToAction("Index");
            }

            var sites = GetAvailableSitesForUser();

            if (sites == null)
            {
                TempData["error"] = "You were not found in the 'Person' database. Please contact admin";
                return RedirectToAction("Index");
            }

            if (!sites.Any())
            {
                //if the user is an admin, it means there's probably not been any added
                TempData["error"] =
                    string.Format(
                        @"No sites have been created. <a href=""{0}"" class=""alert-link"" > Click here </a> to add a site now.",
                        Url.Action("New", "Site"));

                return RedirectToAction("Index");
            }

            var personViewModel = new PersonAddEditViewModel
            {
                AvailablePersonRoles = personRoles,
                AvailableSites = sites,

                Person = new Person()
            };

            //if we're a site admin, set the person site id to our site
            if (User.IsInRole("Site Admin"))
                personViewModel.Person.SiteId = sites.First().Id;

            return View("NewOrEdit", personViewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var personRoles = _db.PersonRoles.ToList();

            var person = await _db.People.
                SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (person == null)
                return new HttpNotFoundResult();

            var personViewModel = new PersonAddEditViewModel
            {
                AvailablePersonRoles = personRoles,
                AvailableSites = GetAvailableSitesForUser(),

                Person = person
            };

            return View("NewOrEdit", personViewModel);
        }

        [HttpPost]
        public ActionResult Save(Person person)
        {
            if (person.Id != 0)
            {
                _db.People.Attach(person);
                _db.Entry(person).State = EntityState.Modified;
            }
            else
                _db.People.Add(person);

            SaveChanges();

            TempData["success"] = "Person " + person.Name + " saved";

            return RedirectToAction("Index");
        }

        private IEnumerable<Site> GetAvailableSitesForUser()
        {
            //here's where we get the sites available to choose from
            //if they're an admin, they can select from all
            //if site admin, only their site.

            if (User.IsInRole("Admin"))
                return _db.Sites.ToList();

            //get the 'Person' by email address
            var person = _db.People.SingleOrDefault(x => x.EmailAddress.Equals(User.Identity.Name));

            //if we didnt' find the person, they're not in the 'people' table
            //this is different from 'application users'
            if (person == null)
                return null;

            return _db.Sites.Where(x => x.Id.Equals(person.SiteId))
                .ToList();
        }
    }
}