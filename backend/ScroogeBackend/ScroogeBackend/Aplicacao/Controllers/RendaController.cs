using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Aplicacao;
using ScroogeBackend.Infraestrutura.DTO.Renda;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Dominio;
using Microsoft.AspNetCore.Http.Json;
using ScroogeBackend.Infraestrutura.DTO;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RendaController : ControllerBase
    {
        [HttpPost(Name = "CreateRenda")]
        public IActionResult InserirRenda([FromBody] RendaPostBody novaRenda)
        {
            int id = 0;
            try
            {
                Renda renda = new Renda();
                id = renda.inserirRenda(new RendaDTO
                {
                    valorRenda = novaRenda.valorRenda
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok($"Novo Gasto adicionado com ID: {id}");
        }

        [HttpGet("Todos", Name = "GetAllRenda")]
        public IActionResult GetAllRenda()
        {
            Renda busca = new Renda();
            List<RendaDTO> renda;
            try
            {
                renda = busca.listarRenda();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(renda);
        }

        [HttpDelete("{id}", Name = "DeleteRenda")]
        public IActionResult DeletarRenda(int id)
        {
            Renda renda = new Renda();
            try
            {
                renda.removerRenda(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok($"Deletada com sucesso Renda com ID: {id}");

        }
    }
}
