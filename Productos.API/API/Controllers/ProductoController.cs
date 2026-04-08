using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoController
    {
        private IProductoFlujo _ProductoFlujo;
        private ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo ProductoFlujo, ILogger<ProductoController> logger)
        {
            _ProductoFlujo = ProductoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(ProductoRequest Producto)
        {
            var resultado = await _ProductoFlujo.Agregar(Producto);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, ProductoRequest Producto)
        {
            var resultado = await _ProductoFlujo.Editar(Id, Producto);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            var resultado = await _ProductoFlujo.Eliminar(Id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _ProductoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            var resultado = await _ProductoFlujo.Obtener(Id);
            return Ok(resultado);
        }
    }
}
