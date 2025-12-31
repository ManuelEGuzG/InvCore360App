using Microsoft.AspNetCore.Mvc;
using InvCore360_App.BL.Services;
using InvCore360_App.UI.Models;

namespace InvCore360_App.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoriaService _categoriaService;
        private readonly ProductoService _productoService;
        private readonly ProveedorService _proveedorService;
        private readonly VentaService _ventaService;
        private readonly UsuarioService _usuarioService;
        private readonly GastoService _gastoService;
        private readonly AlertaInventarioService _alertaService;

        public HomeController(
            CategoriaService categoriaService,
            ProductoService productoService,
            ProveedorService proveedorService,
            VentaService ventaService,
            UsuarioService usuarioService,
            GastoService gastoService,
            AlertaInventarioService alertaService)
        {
            _categoriaService = categoriaService;
            _productoService = productoService;
            _proveedorService = proveedorService;
            _ventaService = ventaService;
            _usuarioService = usuarioService;
            _gastoService = gastoService;
            _alertaService = alertaService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                CategoriasCount = (await _categoriaService.GetAllAsync()).Count(),
                ProductosCount = (await _productoService.GetAllAsync()).Count(),
                ProveedoresCount = (await _proveedorService.GetAllAsync()).Count(),
                VentasCount = (await _ventaService.GetAllAsync()).Count(),
                UsuariosCount = (await _usuarioService.GetAllAsync()).Count(),
                GastosCount = (await _gastoService.GetAllAsync()).Count(),
                AlertasCount = (await _alertaService.GetAllAsync()).Count()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
