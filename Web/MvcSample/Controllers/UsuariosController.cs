using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Domain;

namespace MvcSample.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;

        public UsuariosController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string email, string password, string rol)
        {
            var usr = new Usuario
            {
                Nombre = nombre,
                UserName = email,
                Email = email,
                Rol = rol,
                FechaRegistro = DateTime.Now
            };

            var result = await _userManager.CreateAsync(usr, password);

            if (result.Succeeded)
                return RedirectToAction("Index");

            ViewBag.Error = result.Errors.First().Description;
            return View();
        }
    }
}
