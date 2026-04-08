using System.Text.Json;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.BancoCentral;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        public const string HttpClientName = "BancoCentralCR";
        private const string CodigoTipoCambioVenta = "318";

        private readonly IHttpClientFactory _httpClientFactory;

        public TipoCambioServicio(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<decimal> ObtenerTipoCambioVentaDolarAsync(CancellationToken cancellationToken = default)
        {
            var fecha = DateTime.Now.ToString("yyyy/MM/dd");
            var query = $"?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            var client = _httpClientFactory.CreateClient(HttpClientName);
            var respuesta = await client.GetAsync(query, cancellationToken);
            respuesta.EnsureSuccessStatusCode();

            var json = await respuesta.Content.ReadAsStringAsync(cancellationToken);
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var modelo = JsonSerializer.Deserialize<BccrIndicadoresCuadro491Response>(json, opciones)
                ?? throw new InvalidOperationException("Respuesta del BCCR vacía o inválida.");

            if (!modelo.Estado)
                throw new InvalidOperationException(modelo.Mensaje ?? "El BCCR indicó estado en falso.");

            var valor = ExtraerTipoCambioVenta(modelo);
            if (valor <= 0)
                throw new InvalidOperationException("No se pudo obtener un tipo de cambio válido del BCCR.");

            return valor;
        }

        private static decimal ExtraerTipoCambioVenta(BccrIndicadoresCuadro491Response modelo)
        {
            foreach (var dato in modelo.Datos ?? Enumerable.Empty<BccrDatoCuadro491>())
            {
                foreach (var ind in dato.Indicadores ?? Enumerable.Empty<BccrIndicador491>())
                {
                    if (ind.CodigoIndicador == CodigoTipoCambioVenta && ind.Series?.Count > 0)
                        return ind.Series[0].ValorDatoPorPeriodo;
                }
            }

            foreach (var dato in modelo.Datos ?? Enumerable.Empty<BccrDatoCuadro491>())
            {
                foreach (var ind in dato.Indicadores ?? Enumerable.Empty<BccrIndicador491>())
                {
                    var primero = ind.Series?.FirstOrDefault();
                    if (primero != null && primero.ValorDatoPorPeriodo > 0)
                        return primero.ValorDatoPorPeriodo;
                }
            }

            return 0;
        }
    }
}
