using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Aplicacao;
using ScroogeBackend.Infraestrutura.DTO.Saldo;
using ScroogeBackend.Dominio;
using Microsoft.AspNetCore.Http.Json;
using ScroogeBackend.Infraestrutura.DTO;
using ScroogeBackend.Infraestrutura.DTO.Renda;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaldoController : ControllerBase
    {
        [HttpPost(Name = "CreateSaldo")]
        public IActionResult InserirRenda()
        {
            int id = 0;
            try
            {
                Saldo saldo = new Saldo();
                id = saldo.inserirSaldo();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok($"Novo Gasto adicionado com ID: {id}");
        }

        [HttpGet(Name = "GetSaldo")]
        public IActionResult GetSaldo()
        {
            SaldoDTO saldo = new SaldoDTO();
            Saldo busca = new Saldo();
            try
            {
                saldo = busca.obterSaldo(1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(saldo);
        }
    }
}
