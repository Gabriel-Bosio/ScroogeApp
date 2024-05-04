using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Aplicacao;
using ScroogeBackend.Infraestrutura.DTO;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Dominio;
using Microsoft.AspNetCore.Http.Json;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GastoController : ControllerBase
    {
        [HttpPost(Name = "CreateGasto")]
        public IActionResult InserirGasto([FromBody] GastoDTO novoGasto)
        {
            int id = 0;
            try
            {
                Gasto gasto = new Gasto();
                id = gasto.inserirGasto(novoGasto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok($"Novo Gasto adicionado com ID: {id}");
        }

        [HttpGet("Todos", Name = "GetAllGastos")]
        public IActionResult GetAllGastos()
        {
            Gasto busca = new Gasto();
            List<GastoDTO> gastos;
            try
            {
                gastos = busca.listarGastos();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(gastos);
        }

        [HttpDelete("{id}", Name = "DeleteGasto")]
        public IActionResult DeletarGasto(int id)
        {
            Gasto gasto = new Gasto();
            try
            {
                gasto.removerGasto(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok($"Deletado com sucesso Gasto com ID: {id}");

        }
    }
}
