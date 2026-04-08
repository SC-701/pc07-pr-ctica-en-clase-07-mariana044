namespace Abstracciones.Interfaces.Reglas
{
    public interface IProductoReglas
    {
        /// <summary>
        /// Convierte un monto en colones a USD usando el tipo de cambio de venta del BCCR.
        /// </summary>
        Task<decimal> CalcularPrecioUSD(decimal precioColones, CancellationToken cancellationToken = default);
    }
}
