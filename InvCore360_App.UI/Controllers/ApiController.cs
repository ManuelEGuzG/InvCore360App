using Microsoft.AspNetCore.Mvc;
using InvCore360_App.BL.Services;

namespace InvCore360_App.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ApiController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var list = await _productoService.GetAllAsync();
            return Ok(list);
        }
    }
}