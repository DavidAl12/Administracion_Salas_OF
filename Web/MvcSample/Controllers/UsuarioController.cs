using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Security.Claims;

namespace MvcSample.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : Controller
    {
        private readonly IServicioUsuarioDashboard _servicioUsuarioDashboard;

        public UsuarioController(IServicioUsuarioDashboard servicioUsuarioDashboard)
        {
            _servicioUsuarioDashboard = servicioUsuarioDashboard;
        }

        public IActionResult Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dashboardVM = _servicioUsuarioDashboard.GetDashboardData(userId);
            return View(dashboardVM);
        }
    }
}
