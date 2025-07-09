using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AplicacaoProjeto.Controllers
{
    //aqui quando for fazer o vinculo de categoria com subcategoria, fazer com a cria��o das tabelas onde o registro vai ter uma uma ForeignKey
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ILogger<CategoriaController> _logger;
        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
            _logger = logger;
        }

        [HttpPost("CadastrarCategoria")]
        [SwaggerOperation(Summary = "Cadastra novas categorias", OperationId = "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarCategoria([FromBody] string nomeCategoria)
        {
            try
            {
                _logger.LogInformation("Iniciando o cadastro da categoria: {NomeCategoria}", nomeCategoria);

                Categoria categoria = await _categoriaService.SalvarCategoria(nomeCategoria);

                if (categoria != null)
                {
                    _logger.LogInformation("Categoria cadastrada com sucesso. ID: {ID}", categoria.ID);
                    return Ok(categoria);
                }

                _logger.LogWarning("Falha ao cadastrar categoria. Nome: {NomeCategoria}", nomeCategoria);
                return BadRequest($"N�o foi poss�vel cadastrar a categoria. CategoriaNome: {nomeCategoria}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar categoria: {NomeCategoria}", nomeCategoria);
                return StatusCode(500, new { erro = "Ocorreu um erro ao processar a requisi��o." });
            }
        }

        [HttpGet("BuscarCategoria")]
        [SwaggerOperation(Summary = "Buscar categorias", OperationId = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarCategorias(
            [FromQuery] int? id,
            [FromQuery] string? nome,
            [FromQuery] bool? status,
            [FromQuery] string? ordenarPor,
            [FromQuery] string? tipoOrdenacao)
        {
            try
            {
                List<Categoria>? categorias = await _categoriaService.BuscarCategorias(id, nome, status, ordenarPor, tipoOrdenacao);

                if (categorias == null || !categorias.Any())
                {
                    _logger.LogInformation("Nenhuma categoria encontrada com os filtros informados.");
                    return NotFound("Nenhuma categoria encontrada.");
                }

                _logger.LogInformation("Busca conclu�da. Total encontrado: {Quantidade}", categorias.Count);
                return Ok(categorias);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Par�metro inv�lido na busca de categorias.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categorias.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }


        [HttpPatch("EditarCategoria/{ID}")]
        [SwaggerOperation(Summary = "Editar categorias", OperationId = "Patch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditarCategoria([FromRoute] int ID, [FromBody] string nomeCategoria)
        {
            _logger.LogInformation("Iniciando edi��o da categoria ID={ID} com novo nome: {NomeCategoria}", ID, nomeCategoria);

            try
            {
                Categoria categoriaEditada = await _categoriaService.EditarCategoria(ID, nomeCategoria);

                if (categoriaEditada != null)
                {
                    _logger.LogInformation("Categoria ID={ID} editada com sucesso", ID);
                    return Ok(categoriaEditada);
                }

                _logger.LogWarning("Categoria ID={ID} n�o encontrada", ID);
                return NotFound($"Nenhuma categoria encontrada com o ID {ID}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao editar categoria ID={ID}", ID);
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPatch("EditarStatusCategoria/{ID}")]
        //[SwaggerOperation(Summary = "Editar status das categorias", OperationId = "Patch")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> EditarStatusCategoria(int ID)
        //{
        //    try
        //    {
        //        Categoria categoriaEditada = await _categoriaService.EditarStatusCategoria(ID);
        //        if (categoriaEditada != null)
        //        {
        //            return Ok(categoriaEditada);
        //        }
        //        if(categoriaEditada == null)
        //        {
        //            return NotFound($"Nenhuma categoria foi encontrada para o ID:{ID}");
        //        }
        //        return BadRequest($"N�o foi poss�vel editar o status da categoria {ID}.");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpDelete("ExcluirCategoria/{ID}")]
        //[SwaggerOperation(Summary = "Excluir categorias", OperationId = "Delete")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> ExcluirCategoria(int ID)
        //{
        //    try
        //    {
        //        Categoria categoriaEditada = await _categoriaService.ExcluirCategoria(ID);
        //        if (categoriaEditada == null)
        //        {
        //            return NotFound($"Nenhuma categoria foi encontrada para o ID:{ID}");
        //        }

        //        return Ok($"A categoria com o ID:{ID} foi exclu�da.");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
