using Microsoft.AspNetCore.Mvc;

namespace MvcSample.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Index(string nombre, string rol)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return View();

            // Guardar en sesión
            HttpContext.Session.SetString("UsuarioNombre", nombre);
            HttpContext.Session.SetString("UsuarioRol", rol);

            // Ir al dashboard
            return RedirectToAction("Dashboard", "Usuario");
        }
    }
}
