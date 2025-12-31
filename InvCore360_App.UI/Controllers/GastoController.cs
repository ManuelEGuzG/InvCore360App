using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvCore360_App.BL.Services;
using InvCore360_App.Model.Models;

namespace InvCore360_App.UI.Controllers
{
    public class GastoController : Controller
    {
        private readonly GastoService _service;
        private readonly UsuarioService _usuarioService;

        public GastoController(GastoService service, UsuarioService usuarioService)
        {
            _service = service;
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
            ViewBag.Usuarios = new SelectList(await _usuarioService.GetAllAsync(), "UsuarioID", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gasto model)
        {
            if (!ModelState.IsValid)
            {
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
            ViewBag.Usuarios = new SelectList(await _usuarioService.GetAllAsync(), "UsuarioID", "NombreCompleto", item.UsuarioID);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Gasto model)
        {
            if (id != model.GastoID) return BadRequest();
            if (!ModelState.IsValid)
            {
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