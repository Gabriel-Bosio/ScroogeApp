using Microsoft.AspNetCore.Mvc;
using ScroogeBackend.Aplicacao;
using ScroogeBackend.Infraestrutura.DTO;
using ScroogeBackend.Infraestrutura.DAO;
using ScroogeBackend.Dominio;

namespace ScroogeBackend.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaGastoController : ControllerBase
    {
        private readonly ILogger<CategoriaGastoController> _logger;

        [HttpPost(Name = "CreateCategoriaGasto")]
        public IActionResult Post([FromBody] CategoriaGastoDTO novaCategoria)
        {
            int id = 0;
            try
            {
                CategoriaGasto categoria = new CategoriaGasto(novaCategoria);
                id = categoria.inserirCategoria();
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }

            return Ok($"Nova Categoria de Gasto adicionada com ID: {id}");
        }

        [HttpGet(Name = "GetCategoriaGasto")]
        public IEnumerable<CategoriaGastoDTO> GetCategoriaGastos()
        {
            CategoriaGastoDAO busca = new CategoriaGastoDAO();
            List<CategoriaGastoDTO> categorias = busca.obterTodos();
            return (IEnumerable<CategoriaGastoDTO>)categorias;
        }
    }
}
