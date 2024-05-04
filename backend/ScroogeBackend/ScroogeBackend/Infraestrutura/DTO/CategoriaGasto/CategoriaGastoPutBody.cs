namespace ScroogeBackend.Infraestrutura.DTO.CategoriaGasto
{
    public class CategoriaGastoPutBody
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public double limiteCategoria { get; set; }
    }
}
