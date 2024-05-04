using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Aplicacao;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Dominio;
using Microsoft.AspNetCore.Http.Json;
using ScroogeBackend.Infraestrutura.DTO.CategoriaGasto;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaGastoController : ControllerBase
    {
        private readonly ILogger<CategoriaGastoController> _logger;

        [HttpPost(Name = "CreateCategoriaGasto")]
        public IActionResult InserirCategoriaGasto([FromBody] CategoriaGastoPostBody novaCategoria)
        {
            int id = 0;
            try
            {
                CategoriaGasto categoria = new CategoriaGasto();
                id = categoria.inserirCategoria(new CategoriaGastoDTO
                {
                    descricao = novaCategoria.descricao,
                    limiteCategoria = novaCategoria.limiteCategoria,
                    removivel = novaCategoria.removivel
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok($"Nova Categoria de Gasto adicionada com ID: {id}");
        }

        [HttpPut(Name = "UpdateCategoriaGasto")]
        public IActionResult AtualizarCategoriaGasto([FromBody] CategoriaGastoPutBody novaCategoria)
        {
            CategoriaGasto atualizar = new CategoriaGasto();
            try
            {
                atualizar.alterarCategoria(novaCategoria.id, new CategoriaGastoDTO
                {
                    id = novaCategoria.id,
                    descricao = novaCategoria.descricao,
                    limiteCategoria = novaCategoria.limiteCategoria
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok($"Atualizada com sucesso a Categoria de Gasto com ID: {novaCategoria.id}");
        }

        [HttpGet("Todos", Name = "GetAllCategoriaGasto")]
        public IActionResult GetAllCategoriaGastos()
        {
            CategoriaGasto busca = new CategoriaGasto();
            List<CategoriaGastoDTO> categorias;
            try
            {
                categorias = busca.listarCategorias();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(categorias);
        }

        [HttpGet(Name = "GetCategoriaGasto")]
        public IActionResult GetCategoriaGasto(int id)
        {
            CategoriaGasto busca = new CategoriaGasto();
            CategoriaGastoDTO categoria;
            try
            {
                categoria = busca.obterCategoria(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok(categoria);
        }

        [HttpDelete("{id}", Name = "DeleteCategoriaGasto")]
        public IActionResult DeletarCategoriaGasto(int id)
        {
            CategoriaGasto atualizar = new CategoriaGasto();
            try
            {
                atualizar.removerCategoria(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok($"Deletado com sucesso a Categoria de Gasto com ID: {id}");

        }
    }
}
