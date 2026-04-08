using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase, ICategoriaController
    {
        private ICategoriaFlujo _marcaFlujo;
        private ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaFlujo marcaFLujo, ILogger<CategoriaController> logger)
        {
            _marcaFlujo = marcaFLujo;
            _logger = logger;
        }
        #region Operaciones
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _marcaFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        #endregion Operaciones

    }
}
