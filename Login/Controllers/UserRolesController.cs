using Login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Actions

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesList = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var thisViewModel = new UserRolesViewModel
                {
                    UserID = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await Utilities.UtilityLib.GetUserRoles(_userManager, user)
                };
                userRolesList.Add(thisViewModel);
            }
            return View(userRolesList);
        }

        [HttpGet]
        public async Task<IActionResult> Manage (string userID)
        {
            // Passing the userId directly to the View
            ViewBag.userId = userID;

            // tries to the get the user from the database
            var user = await _userManager.FindByIdAsync(userID);

            // returns not found if the user doesn't exist in the database
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userID} cannot be found";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;

            var model = new List<ManageUserRolesViewModel>();

            var roles = _roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }

                model.Add(userRolesViewModel);
            }
            // return the list of roles to the View
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userID)
        {
            // Get user from the database
            var user = await _userManager.FindByIdAsync(userID);

            // if user is null return current view();
            if (user == null)
            {
                return View();
            }

            // Gets all roles from the database
            var roles = await _userManager.GetRolesAsync(user);

            // To add role, I am removing all the roles from the current user
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            // return error if the previous action fails
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            // Add the role based on the user selection from the ManageUserRolesViewModel
            var selectedRoles = model.Where(x => x.Selected).Select(y => y.RoleName);
            result = await _userManager.AddToRolesAsync(user, selectedRoles);

            // returns error if the addition process fails
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            //redirect the user back to the index page if everything goes well
            return RedirectToAction("Index");
        }
        #endregion
    }
}
