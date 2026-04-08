using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;


namespace Flujo
{
        public class ProductoFlujo : IProductoFlujo
        {
            private IProductoDA _ProductoDA;
            private readonly IProductoReglas _productoReglas;

            public ProductoFlujo(IProductoDA ProductoDA, IProductoReglas productoReglas)
            {
                _ProductoDA = ProductoDA;
                _productoReglas = productoReglas;
            }

            public Task<Guid> Agregar(ProductoRequest Producto)
            {
                return _ProductoDA.Agregar(Producto);
        }

            public Task<Guid> Editar(Guid Id, ProductoRequest Producto)
            {
                return _ProductoDA.Editar(Id, Producto);
        }

            public Task<Guid> Eliminar(Guid Id)
            {
               return _ProductoDA.Eliminar(Id);
        }

            public Task<IEnumerable<ProductoResponse>> Obtener()
            {
                return _ProductoDA.Obtener();
        }

            public async Task<ProductoResponse> Obtener(Guid Id)
            {
                var producto = await _ProductoDA.Obtener(Id);
                if (producto == null)
                    return null;

                producto.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);
                return producto;
            }
        }
    }

