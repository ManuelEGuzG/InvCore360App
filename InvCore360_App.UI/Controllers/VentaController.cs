using Microsoft.AspNetCore.Mvc;
using InvCore360_App.BL.Services;
using InvCore360_App.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvCore360_App.UI.Controllers
{
    public class VentaController : Controller
    {
        private readonly VentaService _service;
        private readonly UsuarioService _usuarioService;

        public VentaController(VentaService service, UsuarioService usuarioService)
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
        public async Task<IActionResult> Create(Venta model)
        {
            if (!ModelState.IsValid) return View(model);

            // For simplicity, just save header. Detailed registration should use RegisterSaleAsync from BL.
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
        public async Task<IActionResult> Edit(int id, Venta model)
        {
            if (id != model.VentaID) return BadRequest();
            if (!ModelState.IsValid) return View(model);

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

        // Example endpoint to register a full sale with details (expects JSON body)
        [HttpPost]
        public async Task<IActionResult> RegisterSale([FromBody] SaleDto dto)
        {
            if (dto == null || dto.Detalles == null || !dto.Detalles.Any()) return BadRequest("Detalles requeridos");

            var venta = new Venta
            {
                Subtotal = dto.Subtotal,
                Impuesto = dto.Impuesto,
                Descuento = dto.Descuento,
                Total = dto.Total,
                MetodoPago = dto.MetodoPago,
                UsuarioID = dto.UsuarioID,
                Observaciones = dto.Observaciones
            };

            var detalles = dto.Detalles.Select(d => new DetalleVenta
            {
                ProductoID = d.ProductoID,
                Cantidad = d.Cantidad,
                Descuento = d.Descuento
            }).ToList();

            try
            {
                var result = await _service.RegisterSaleAsync(venta, detalles);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class SaleDto
        {
            public decimal Subtotal { get; set; }
            public decimal Impuesto { get; set; }
            public decimal Descuento { get; set; }
            public decimal Total { get; set; }
            public string MetodoPago { get; set; }
            public int? UsuarioID { get; set; }
            public string? Observaciones { get; set; }
            public List<SaleDetailDto> Detalles { get; set; } = new();
        }

        public class SaleDetailDto
        {
            public int ProductoID { get; set; }
            public int Cantidad { get; set; }
            public decimal Descuento { get; set; }
        }
    }
}