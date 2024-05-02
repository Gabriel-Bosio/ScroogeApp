using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Dominio;
using ScroogeBackend.Infraestrutura.DTO;
using System.Xml.Linq;

namespace ScroogeBackend.Aplicacao.Controllers
{
    public class ControleCategoriaController : ControllerBase
    {
        private readonly ILogger<ControleCategoriaController> _logger;

        [HttpGet("Todos", Name = "GetAllControleCategorias")]
        public IActionResult GetAllControleCategorias(DateTime mesBase)
        {
            ControleCategoria busca = new ControleCategoria();
            List<ControleCategoriaDTO> controles;
            mesBase = new DateTime(mesBase.Year, mesBase.Month, 1, 0, 0, 0);
            try
            {
                controles = busca.listarControles(mesBase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(controles);
        }
    }
}
