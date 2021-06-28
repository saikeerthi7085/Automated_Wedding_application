using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Automated_Wedding_Application.Areas.Identity.Data;
using Automated_Wedding_Application.Services;

namespace Automated_Wedding_Application.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.IEmailSender _sender;
        private readonly Services.IRazorPartialToStringRenderer _renderer;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, Services.IEmailSender sender, Services.IRazorPartialToStringRenderer renderer)
        {
            _userManager = userManager;
            _sender = sender;
            _renderer = renderer;
        }

        public string Email { get; set; }


        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;

           
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

            var htmlEmail = await _renderer.RenderPartialToStringAsync("/Templates/_ConfirmEmail.cshtml", EmailConfirmationUrl);

            await _sender.SendEmailAsync(Email, "Wedding Planner Application - Confirm Email", htmlEmail);
            return Page();
        }
    }
}
