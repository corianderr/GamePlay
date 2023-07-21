// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Entities;
using GamePlay.Domain.Exceptions;
using GamePlay.Domain.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamePlay.Web.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly IUserService _userService;

    public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger,
        IUserService userService)
    {
        _userService = userService;
        _logger = logger;
    }

    [BindProperty] public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage)) ModelState.AddModelError(string.Empty, ErrorMessage);

        returnUrl ??= Url.Content("~/");
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        try
        {
            if (ModelState.IsValid)
            {
                var user = new LoginUserModel
                {
                    EmailOrUsername = Input.EmailOrUsername,
                    Password = Input.Password,
                    RememberMe = Input.RememberMe
                };
                await _userService.LoginAsync(user);
                //_logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
        }
        catch (NotFoundException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return Page();
        }
        catch (BadRequestException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return Page();
        }

        return Page();
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Email or Username")]
        public string EmailOrUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
    }
}