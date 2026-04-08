using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;

namespace Reglas
{
    public class ProductoReglas : IProductoReglas
    {
        private readonly ITipoCambioServicio _tipoCambioServicio;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio)
        {
            _tipoCambioServicio = tipoCambioServicio;
        }

        public async Task<decimal> CalcularPrecioUSD(decimal precioColones, CancellationToken cancellationToken = default)
        {
            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambioVentaDolarAsync(cancellationToken);
            if (tipoCambio <= 0)
                throw new InvalidOperationException("El tipo de cambio debe ser mayor que cero.");

            return Math.Round(precioColones / tipoCambio, 2, MidpointRounding.AwayFromZero);
        }
    }
}
