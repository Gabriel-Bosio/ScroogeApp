namespace ScroogeBackend.Infraestrutura.DTO
{
    public class GastoDTO
    {
        public int id { get; set; }
        public double valorGasto { get; set; }

        public DateTime dataHoraGasto { get; set; }
        public int id_categoriaGasto { get; set; }
        public CategoriaGastoDTO categoria { get; set; }
    }
}
