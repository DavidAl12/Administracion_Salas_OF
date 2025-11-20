using Microsoft.AspNetCore.Mvc;

namespace MvcSample.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Dashboard()
        {
            var nombre = HttpContext.Session.GetString("UsuarioNombre");
            var rol = HttpContext.Session.GetString("UsuarioRol");

            if (string.IsNullOrEmpty(nombre))
                return RedirectToAction("Index", "Login");

            ViewBag.Nombre = nombre;
            ViewBag.Rol = rol;

            return View();
        }

    }

}

