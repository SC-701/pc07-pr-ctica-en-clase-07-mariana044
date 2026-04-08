using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace Abstracciones.Interfaces.API
{
    public interface IProductoController
    {
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Agregar(ProductoRequest Producto);
        Task<IActionResult> Editar(Guid Id, ProductoRequest Producto);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
