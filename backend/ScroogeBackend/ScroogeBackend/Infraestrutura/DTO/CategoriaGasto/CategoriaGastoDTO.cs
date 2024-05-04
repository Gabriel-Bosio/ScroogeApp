namespace ScroogeBackend.Infraestrutura.DTO.CategoriaGasto
{
    public class CategoriaGastoDTO
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public double limiteCategoria { get; set; }
        public bool removivel { get; set; }
    }
}
