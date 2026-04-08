using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {
        private ISubCategoriaDA _subcategoriaDA;

        public SubCategoriaFlujo(ISubCategoriaDA subcategoriaDA)
        {
            _subcategoriaDA = subcategoriaDA;
        }
        public async Task<IEnumerable<SubCategoria>> Obtener(Guid IdCategoria)
        {
            return await _subcategoriaDA.Obtener(IdCategoria);
        }
    }
}
