namespace Abstracciones.Modelos.Servicios.BancoCentral
{
    public class BccrIndicadoresCuadro491Response
    {
        public bool Estado { get; set; }
        public string? Mensaje { get; set; }
        public List<BccrDatoCuadro491>? Datos { get; set; }
    }

    public class BccrDatoCuadro491
    {
        public string? Titulo { get; set; }
        public string? Periodicidad { get; set; }
        public List<BccrIndicador491>? Indicadores { get; set; }
    }

    public class BccrIndicador491
    {
        public string? CodigoIndicador { get; set; }
        public string? NombreIndicador { get; set; }
        public List<BccrSerie491>? Series { get; set; }
    }

    public class BccrSerie491
    {
        public string? Fecha { get; set; }
        public decimal ValorDatoPorPeriodo { get; set; }
    }
}
