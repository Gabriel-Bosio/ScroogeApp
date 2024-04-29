using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeBackend.Infraestrutura.DTO
{
    public class CategoriaGastoDTO
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public double? limiteCategoria { get; set; }
        public bool removivel { get; set; }
    }
}
