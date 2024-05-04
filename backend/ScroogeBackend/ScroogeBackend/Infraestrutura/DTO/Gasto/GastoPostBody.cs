using ScroogeBackend.Infraestrutura.DTO.CategoriaGasto;

namespace ScroogeBackend.Infraestrutura.DTO.Gasto
{
    public class GastoPostBody
    {
        public double valorGasto { get; set; }
        public int id_categoriaGasto { get; set; }
    }
}
