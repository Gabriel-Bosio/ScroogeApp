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
    public class CategoriaGastoController : ControllerBase
    {
        private readonly ILogger<CategoriaGastoController> _logger;

        [HttpPost(Name = "CreateCategoriaGasto")]
        public IActionResult InserirCategoriaGasto([FromBody] CategoriaGastoDTO novaCategoria)
        {
            int id = 0;
            try
            {
                CategoriaGasto categoria = new CategoriaGasto();
                id = categoria.inserirCategoria(novaCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok($"Nova Categoria de Gasto adicionada com ID: {id}");
        }

        [HttpPut(Name = "UpdateCategoriaGasto")]
        public IActionResult AtualizarCategoriaGasto([FromBody] CategoriaGastoDTO novaCategoria)
        {
            CategoriaGasto atualizar = new CategoriaGasto();
            try
            {
                atualizar.alterarCategoria(novaCategoria.id, novaCategoria);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok($"Atualizada com sucesso a Categoria de Gasto com ID: {novaCategoria.id}");
        }

        [HttpGet(Name = "GetAllCategoriaGasto")]
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
                return BadRequest(ex);
            }
            return Ok(categorias);
        }

        [HttpGet("Todos", Name = "GetCategoriaGasto")]
        public IActionResult GetCategoriaGastos(int id)
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
        public IActionResult Delete(int id)
        {
            CategoriaGasto atualizar = new CategoriaGasto();
            try
            {
                atualizar.removerCategoria(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok($"Deletado com sucesso a Categoria de Gasto com ID: {id}");

        }
    }
}
