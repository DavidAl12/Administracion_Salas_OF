using Microsoft.AspNetCore.Mvc;

namespace MvcSample.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Dashboard()
        {
            // Si no ha iniciado sesión, ir al login
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioNombre")))
                return RedirectToAction("Index", "Login");

            return View();
        }
    }
}
