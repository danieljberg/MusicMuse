using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MusicMuse.Models;

namespace MusicMuse.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string UserRole { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
        public class StaticDetails
        {
            public const string BandEndUser = "Band";
            public const string MusicianEndUser = "Musician";
            public const string BusinessEndUser = "Business";
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    RoleString = Input.UserRole,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    if(!await _roleManager.RoleExistsAsync(StaticDetails.BandEndUser))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(StaticDetails.BandEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.MusicianEndUser))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(StaticDetails.MusicianEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticDetails.BusinessEndUser))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(StaticDetails.BusinessEndUser));
                    }
                    if (Input.UserRole == "Band")
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.BandEndUser);
                    }
                    if (Input.UserRole == "Musician")
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.MusicianEndUser);
                    }
                    if (Input.UserRole == "Business")
                    {
                        await _userManager.AddToRoleAsync(user, StaticDetails.BusinessEndUser);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                    if (user.RoleString == "Band")
                    {
                        return RedirectToAction("Create", "Bands");
                    }
                    if (user.RoleString == "Musician")
                    {
                        return RedirectToAction("Create", "Musicians");
                    }
                    if (user.RoleString == "Business")
                    {
                        return RedirectToAction("Create", "Businesses");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
