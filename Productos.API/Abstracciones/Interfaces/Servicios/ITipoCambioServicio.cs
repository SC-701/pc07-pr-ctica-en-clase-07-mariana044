namespace Abstracciones.Interfaces.Servicios
{
    public interface ITipoCambioServicio
    {
        /// <summary>
        /// Obtiene el tipo de cambio de venta del dólar (colones por USD) para la fecha actual según el BCCR.
        /// </summary>
        Task<decimal> ObtenerTipoCambioVentaDolarAsync(CancellationToken cancellationToken = default);
    }
}
