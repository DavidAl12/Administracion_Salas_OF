using Microsoft.AspNetCore.Mvc;
using Services;
using Servicios.Models;
using Domain;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

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
            var usuarios = _userManager.Users.ToList();
            return View(usuarios);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreateViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var usuario = new Usuario
            {
                UserName = input.Email,
                Email = input.Email,
                Nombre = input.Nombre,
                Rol = input.Rol,
                FechaRegistro = input.FechaRegistro
            };
            var result = await _userManager.CreateAsync(usuario, input.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, input.Rol);
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(input);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();

            var vm = new UsuarioEditViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol,
                FechaRegistro = usuario.FechaRegistro
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioEditViewModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var usuario = _userManager.Users.FirstOrDefault(u => u.Id == input.Id);
            if (usuario == null)
                return NotFound();

            usuario.Nombre = input.Nombre;
            usuario.Email = input.Email;
            usuario.UserName = input.Email;
            usuario.Rol = input.Rol;
            usuario.FechaRegistro = input.FechaRegistro;

            await _userManager.UpdateAsync(usuario);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
                await _userManager.DeleteAsync(usuario);

            return RedirectToAction(nameof(Index));
        }
    }
}
