using ScroogeBackend.Infraestrutura.DTO.CategoriaGasto;

namespace ScroogeBackend.Infraestrutura.DTO.ControleCategoria
{
    public class ControleCategoriaDTO
    {
        public int id { get; set; }
        public double limite { get; set; }

        public double gastoAtual { get; set; }
        public DateTime mesBase { get; set; }
        public string mensagem { get; set; }
        public int id_categoriaGasto { get; set; }
        public CategoriaGastoDTO categoria { get; set; }
    }
}
