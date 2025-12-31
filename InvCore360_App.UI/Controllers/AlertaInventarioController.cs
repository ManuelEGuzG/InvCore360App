using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvCore360_App.BL.Services;
using InvCore360_App.Model.Models;

namespace InvCore360_App.UI.Controllers
{
    public class AlertaInventarioController : Controller
    {
        private readonly AlertaInventarioService _service;
        private readonly ProductoService _productoService;

        public AlertaInventarioController(AlertaInventarioService service, ProductoService productoService)
        {
            _service = service;
            _productoService = productoService;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlertaInventario model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", model.ProductoID);
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
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlertaInventario model)
        {
            if (id != model.AlertaID) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(await _productoService.GetAllAsync(), "ProductoID", "Nombre", model.ProductoID);
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
