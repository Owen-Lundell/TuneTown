using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TuneTown.Models;
using TuneTown.Repo;
using System.Data;

namespace TuneTown.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private ISubmissionRepository submissionRepo;

        public UserController(UserManager<AppUser> userMngr,
                               RoleManager<IdentityRole> roleMngr,
                               ISubmissionRepository repo)
        {
            userManager = userMngr;
            roleManager = roleMngr;
            submissionRepo = repo;
        }

        /// Get a list of all users. Each user will have a list of their
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }

            UserVM model = new UserVM
            {
                Users = users, // List of AppUsers
                Roles = roleManager.Roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                { // if failed
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }

        #region Role Addition
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. "
                + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToPoster(string id)
        {
            IdentityRole posteRole = await roleManager.FindByNameAsync("Poster");
            if (posteRole == null)
            {
                TempData["message"] = "Poster role does not exist. "
                + "Click 'Create Poster Role' button to create it.";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, posteRole.Name);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Role Removal
        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromPoster(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Poster");
            return RedirectToAction("Index");
        }
        #endregion

        #region Role Deletion
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
        #endregion

        #region Role Creation
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreatePosterRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Poster"));
            return RedirectToAction("Index");
        }
        #endregion

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterVM model)
        {

            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
