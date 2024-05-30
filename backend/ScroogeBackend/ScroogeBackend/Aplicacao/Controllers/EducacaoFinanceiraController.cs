using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Dominio;
using ScroogeBackend.Infraestrutura.DTO.ControleCategoria;
using ScroogeBackend.Infraestrutura.DTO.EducacaoFinanceira;
using System.Xml.Linq;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EducacaoFinanceiraController : ControllerBase
    {
        private readonly ILogger<EducacaoFinanceiraController> _logger;

        [HttpGet(Name = "GetEducacaoFinanceira")]
        public IActionResult GetEducacaoFinanceira()
        {
            EducacaoFinanceira busca = new EducacaoFinanceira();
            EducacaoFinanceiraDTO educacao;
            try
            {
                educacao = busca.buscar();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(educacao);
        }
    }
}
