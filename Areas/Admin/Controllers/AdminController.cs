using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Models;


namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    [Route("[area]/[action]")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RegisterUser()
        {
            var positionsSelectList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Inventory", Text = "Inventory" },
            new SelectListItem { Value = "Cashier", Text = "Cashier" },
        };

            var model = new RegisterUserViewModel { Positions = positionsSelectList };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var claim = new Claim("Position", model.SelectedPosition);
                    await _userManager.AddClaimAsync(user, claim);

                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.Positions = new List<SelectListItem>
        {
            new SelectListItem { Value = "Inventory", Text = "Inventory" },
            new SelectListItem { Value = "Cashier", Text = "Cashier" },
        };
            return View(model);
        }
    }
}


