using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using InvCore360_App.BL.Services;
using InvCore360_App.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace InvCore360_App.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UsuarioService _usuarioService;
        public AccountController(UsuarioService usuarioService) => _usuarioService = usuarioService;

        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string nombreUsuario, string contrasena, string returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                ModelState.AddModelError(string.Empty, "Ingrese usuario y contraseña.");
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            if (!await _usuarioService.ValidateCredentialsAsync(nombreUsuario, contrasena))
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            var user = await _usuarioService.GetByNombreUsuarioAsync(nombreUsuario);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.NombreCompleto ?? user.NombreUsuario),
        new Claim("UsuarioID", user.UsuarioID.ToString()),
        new Claim(ClaimTypes.Role, user.Rol ?? string.Empty)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirigir correctamente
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
