using Microsoft.AspNetCore.Mvc;
using InvCore360_App.BL.Services;

namespace InvCore360_App.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ProductoService _productoService;
        private readonly UsuarioService _usuarioService;

        public ApiController(ProductoService productoService, UsuarioService usuarioService)
        {
            _productoService = productoService;
            _usuarioService = usuarioService;
        }

        [HttpGet("productos")]
        public async Task<IActionResult> GetProductos()
        {
            var list = await _productoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var list = await _usuarioService.GetAllAsync();
            return Ok(list);
        }
    }
}