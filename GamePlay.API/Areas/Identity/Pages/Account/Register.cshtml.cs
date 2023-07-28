// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using GamePlay.Domain.Contracts.Services;
using GamePlay.Domain.Exceptions;
using GamePlay.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GamePlay.API.Areas.Identity.Pages.Account;
public class RegisterModel : PageModel
{
    private readonly ILogger<RegisterModel> _logger;
    private readonly IUserService _userService;

    public RegisterModel(
        ILogger<RegisterModel> logger,
        IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [BindProperty] public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }


    public async Task OnGetAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (!ModelState.IsValid) return Page();

        var user = CreateUser();
        user.Email = Input.Email;
        user.Username = Input.Username;
        user.Password = Input.Password;
        try
        {
            await _userService.RegisterAsync(user);
            _logger.LogInformation("User logged in.");
            return LocalRedirect(returnUrl);
        }
        catch (BadRequestException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
        }

        return Page();
    }

    private CreateUserModel CreateUser()
    {
        try
        {
            return Activator.CreateInstance<CreateUserModel>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(CreateUserModel)}'. " +
                                                $"Ensure that '{nameof(CreateUserModel)}' is not an abstract class and has a parameterless constructor.");
        }
    }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}