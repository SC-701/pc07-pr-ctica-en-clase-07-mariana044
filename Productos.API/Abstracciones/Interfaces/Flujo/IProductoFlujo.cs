using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IProductoFlujo
    {
        Task<IEnumerable<ProductoResponse>> Obtener();
        Task<ProductoResponse> Obtener(Guid Id);
        Task<Guid> Agregar(ProductoRequest Producto);
        Task<Guid> Editar(Guid Id, ProductoRequest Producto);
        Task<Guid> Eliminar(Guid Id);
    }
}
