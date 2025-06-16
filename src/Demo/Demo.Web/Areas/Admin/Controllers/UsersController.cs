﻿using Demo.Infrastructure;
using Demo.Infrastructure.Identity;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public UsersController(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;

        }

        public IActionResult AddUserAsync()
        {
            var model = new AddUserModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserAsync(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = CreateUser();

                    await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

                    user.RegistrationDate = DateTime.UtcNow;

                    var result = await _userManager.CreateAsync(user, model.Password);

                    await _userManager.AddToRoleAsync(user, model.Role);

                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = "User added",
                        Type = ResponseTypes.Success
                    });
                }
                catch (Exception ex)
                {
                    var message = "User Create Failed";
                    ModelState.AddModelError("UserCreateFailed", message);
                    TempData.Put("ResponseMessage", new ResponseModel
                    {
                        Message = message,
                        Type = ResponseTypes.Danger
                    });
                }
            }

            return View(model);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}