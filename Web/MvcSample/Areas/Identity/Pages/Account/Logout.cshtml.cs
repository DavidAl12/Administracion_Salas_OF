using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MvcSample.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<Usuario> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            // Cierra sesión usando Identity y elimina cookies de autenticación
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Elimina todas las cookies (incluyendo cookies persistentes de RememberMe)
            foreach (var cookie in Request.Cookies.Keys)
                Response.Cookies.Delete(cookie);

            _logger.LogInformation("User logged out.");

            // Redirige siempre al login
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}
