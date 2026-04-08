using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class ProductoDA : IProductoDA

    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;
        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones
        public async Task<Guid> Agregar(ProductoRequest Producto)
        {
            string query = @"AgregarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {

                Id = Guid.NewGuid(),
                IdSubCategoria = Producto.IdSubCategoria,
                Nombre = Producto.Nombre,
                Descripcion = Producto.Descripcion,
                Precio = Producto.Precio,
                Stock = Producto.Stock,
                CodigoBarras = Producto.CodigoBarras,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest Producto)
        {
            await verificarProductoExiste(Id);
            string query = @"EditarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {

                Id = Id,
                IdSubCategoria = Producto.IdSubCategoria,
                Nombre = Producto.Nombre,
                Descripcion = Producto.Descripcion,
                Precio = Producto.Precio,
                Stock = Producto.Stock,
                CodigoBarras = Producto.CodigoBarras,
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarProductoExiste(Id);
            string query = @"EliminarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query,
            new { Id = Id });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion
        #region Helpers
        private async Task verificarProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("No se encontro el producto");
        }
        #endregion
    }

}
