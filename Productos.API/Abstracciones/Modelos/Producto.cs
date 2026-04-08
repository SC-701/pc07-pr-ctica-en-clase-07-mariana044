using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(50, ErrorMessage = "La propiedad nombre debe ser mayor a 3 caracteres y menor de 50", MinimumLength = 3)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La propiedad descripción es requerida")]
        [StringLength(200, ErrorMessage = "La propiedad descripción debe ser mayor a 10 caracteres y menor de 200", MinimumLength = 10)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La propiedad precio es requerida")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public Decimal Precio { get; set; }
        [Required(ErrorMessage = "La propiedad stock es requerida")]
        [Range(0, 25000, ErrorMessage = "El stock no puede superar las 25.000 unidades")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "La propiedad codigo de barras es requerida")]
        [StringLength(13, ErrorMessage = "El código de barras debe de ser de 13 dígitos", MinimumLength = 13)]
        [RegularExpression(@"^\d+$", ErrorMessage = "El código de barras solo debe contener números")]
        public string CodigoBarras { get; set; }
    }
    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string Categoria { get; set; }
        public string SubCategoria { get; set; }
        /// <summary>
        /// Precio en USD (solo se calcula en el detalle por id).
        /// </summary>
        public decimal? PrecioUSD { get; set; }
    }
}
