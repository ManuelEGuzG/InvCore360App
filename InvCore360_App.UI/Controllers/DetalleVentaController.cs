using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvCore360_App.BL.Services;
using InvCore360_App.Model.Models;

namespace InvCore360_App.UI.Controllers
{
    public class DetalleVentaController : Controller
    {
        private readonly DetalleVentaService _service;
        private readonly ProductoService _productoService;
        private readonly VentaService _ventaService;

        public DetalleVentaController(DetalleVentaService service, ProductoService productoService, VentaService ventaService)
        {
            _service = service;
            _productoService = productoService;
            _ventaService = ventaService;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetalleVenta model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", model.ProductoID);
                ViewBag.Ventas = new SelectList(await _ventaService.GetAllAsync(), "VentaID", "NumeroVenta", model.VentaID);
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
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DetalleVenta model)
        {
            if (id != model.DetalleVentaID) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", model.ProductoID);
                ViewBag.Ventas = new SelectList(await _ventaService.GetAllAsync(), "VentaID", "NumeroVenta", model.VentaID);
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
