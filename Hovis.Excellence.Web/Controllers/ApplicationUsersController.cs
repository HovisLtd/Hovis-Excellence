using Hovis.Excellence.Web.Identity;
using Hovis.Excellence.Web.Models;
using Hovis.Excellence.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: ApplicationUsers
        public async Task<ActionResult> Index()
        {
            var viewModel = new List<ApplicationUserViewModel>();

            foreach (var applicationUser in await UserManager.Users.ToListAsync())
            {
                var roles = await UserManager.GetRolesAsync(applicationUser.Id);

                //this is where you could use AutoMapper - to map between the ApplicationUser and ApplicationUserViewModel
                //without doing the below manual mapping
                viewModel.Add(new ApplicationUserViewModel
                {
                    Id = applicationUser.Id,
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    Email = applicationUser.Email,
                    CreatedDate = applicationUser.CreatedDate,
                    SelectedRoles = roles
                });
            }

            return View(viewModel);
        }

        // GET: /ApplicationUsers/New
        public ActionResult New()
        {
            var model = new ApplicationUserViewModel();

            ViewBag.AvailableRoles = RoleManager.Roles;
            return View(model);
        }

        //
        // POST: /ApplicationUsers/New
        [HttpPost]
        public async Task<ActionResult> New(ApplicationUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //create an instance of ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName
                };

                //save the user
                var userManagerCreateResult = await UserManager.CreateAsync(user);

                if (userManagerCreateResult.Succeeded)
                {
                    //Add User to the selected Roles if we chose any
                    if (userViewModel.SelectedRoles != null)
                    {
                        //iterate over each selected role
                        foreach (var selectedRole in userViewModel.SelectedRoles)
                        {
                            var result = await UserManager.AddToRolesAsync(user.Id, selectedRole);

                            //if it failed, return the view showing the error
                            if (!result.Succeeded)
                            {
                                ModelState.AddModelError("", result.Errors.First());
                                ViewBag.AvailableRoles = RoleManager.Roles;

                                return View(userViewModel);
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", userManagerCreateResult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();
                }

                TempData["success"] = "User " + user.Email + " created successfully";
                return RedirectToAction("Index");
            }

            ViewBag.AvailableRoles = RoleManager.Roles;
            return View(userViewModel);
        }

        // GET: /ApplicationUsers/Edit/id
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            ViewBag.AvailableRoles = RoleManager.Roles;

            var selectedRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new ApplicationUserViewModel
            {
                Id = user.Id,

                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                SelectedRoles = selectedRoles
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ApplicationUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);

                if (user == null)
                    return HttpNotFound();

                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                if (editUser.SelectedRoles == null)
                    editUser.SelectedRoles = new string[] { };

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                var rolesToAdd = editUser.SelectedRoles.Except(userRoles);

                foreach (var roleToAdd in rolesToAdd)
                {
                    var result = await UserManager.AddToRolesAsync(user.Id, roleToAdd);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                }

                var rolesToRemove = userRoles.Except(editUser.SelectedRoles);

                foreach (var roleToRemove in rolesToRemove)
                {
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, roleToRemove);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        return View();
                    }
                }

                var updateUserResult = await UserManager.UpdateAsync(user);

                if (updateUserResult.Succeeded)
                    TempData["success"] = "User " + user.Email + " details edited successfully.";
                else
                    TempData["error"] = "User " + user.Email + " details were not edited.";

                return RedirectToAction("Index");
            }

            TempData["error"] = "Something failed";
            return View();
        }

        public ActionResult Delete(string id)
        {
            var user = UserManager.FindById(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var user = await UserManager.FindByIdAsync(id);

                if (user == null)
                    return HttpNotFound();

                var result = await UserManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                TempData["success"] = "User " + user.Email + " has been deleted";
                return RedirectToAction("Index");
            }
            return View();
        }
    }

    public class ApplicationUserListViewModel
    {
        public object Users { get; set; }
    }
}