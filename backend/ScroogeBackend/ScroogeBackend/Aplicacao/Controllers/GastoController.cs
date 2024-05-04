using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Aplicacao;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Dominio;
using Microsoft.AspNetCore.Http.Json;
using ScroogeBackend.Infraestrutura.DTO.Gasto;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GastoController : ControllerBase
    {
        [HttpPost(Name = "CreateGasto")]
        public IActionResult InserirGasto([FromBody] GastoPostBody novoGasto)
        {
            int id = 0;
            try
            {
                Gasto gasto = new Gasto();
                id = gasto.inserirGasto(new GastoDTO
                {
                    valorGasto = novoGasto.valorGasto,
                    id_categoriaGasto = novoGasto.id_categoriaGasto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }
            return Ok($"Deletado com sucesso Gasto com ID: {id}");

        }
    }
}
