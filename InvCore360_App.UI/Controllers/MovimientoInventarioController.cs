using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvCore360_App.BL.Services;
using InvCore360_App.Model.Models;

namespace InvCore360_App.UI.Controllers
{
    public class MovimientoInventarioController : Controller
    {
        private readonly MovimientoInventarioService _service;
        private readonly ProductoService _productoService;
        private readonly VentaService _ventaService;
        private readonly UsuarioService _usuarioService;

        public MovimientoInventarioController(
            MovimientoInventarioService service,
            ProductoService productoService,
            VentaService ventaService,
            UsuarioService usuarioService)
        {
            _service = service;
            _productoService = productoService;
            _ventaService = ventaService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre");
            ViewBag.Ventas = new SelectList(await _ventaService.GetAllAsync(), "VentaID", "NumeroVenta");
            ViewBag.Usuarios = new SelectList(await _usuarioService.GetAllAsync(), "UsuarioID", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovimientoInventario model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", model.ProductoID);
                ViewBag.Ventas = new SelectList(await _ventaService.GetAllAsync(), "VentaID", "NumeroVenta", model.VentaID);
                ViewBag.Usuarios = new SelectList(await _usuarioService.GetAllAsync(), "UsuarioID", "NombreCompleto", model.UsuarioID);
                return View(model);
            }

            await _service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", item.ProductoID);
            ViewBag.Ventas = new SelectList(await _ventaService.GetAllAsync(), "VentaID", "NumeroVenta", item.VentaID);
            ViewBag.Usuarios = new SelectList(await _usuarioService.GetAllAsync(), "UsuarioID", "NombreCompleto", item.UsuarioID);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovimientoInventario model)
        {
            if (id != model.MovimientoID) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", model.ProductoID);
                ViewBag.Ventas = new SelectList(await _ventaService.GetAllAsync(), "VentaID", "NumeroVenta", model.VentaID);
                ViewBag.Usuarios = new SelectList(await _usuarioService.GetAllAsync(), "UsuarioID", "NombreCompleto", model.UsuarioID);
                return View(model);
            }

            await _service.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
