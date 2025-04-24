
using Domain.Models;
using Domain.Services_Interfaces_;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AplicacaoProjeto.Controllers
{
    //aqui quando for fazer o vinculo de categoria com subcategoria, fazer com a criação das tabelas onde o registro vai ter uma uma ForeignKey
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost("SalvarCategoria")]
        [SwaggerOperation(Summary = "Salva categorias", OperationId = "Post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SalvarCategoria([FromQuery] string nomeCategoria)
        {
            try
            {

                Categoria categoria = await _categoriaService.SalvarCategoria(nomeCategoria);
                if (categoria != null)
                {
                    return Ok(categoria);
                }
                return BadRequest($"Não foi possivel cadastrar a categoria. CategoriaNome: {nomeCategoria}.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro ao processar a requisição." });
            }
        }

        [HttpGet("BuscarCategoria")]
        [SwaggerOperation(Summary = "Buscar categorias", OperationId = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarCategorias([FromQuery] int? id, [FromQuery] string? nome, [FromQuery] bool? status, [FromQuery] string? ordenarPor, [FromQuery] string ordenacao)
        {
            try
            {
                var categorias = await _categoriaService.BuscarCategorias(id, nome, status, ordenarPor, ordenacao);
                if (categorias == null || !categorias.Any())
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPut("EditarCategoria/{ID}")]
        //[SwaggerOperation(Summary = "Editar categorias", OperationId = "Put")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> EditarCategoria(int ID, [FromBody] Categoria categoria)
        //{
        //    try
        //    {
        //        Categoria categoriaEditada = await _categoriaService.EditarCategoria(categoria);
        //        if (categoriaEditada != null)
        //        {
        //            return Ok(categoriaEditada);
        //        }
        //        return NotFound($"Nenhuma categoria encontrada com os parametros {categoria}.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

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
        //        return BadRequest($"Não foi possível editar o status da categoria {ID}.");

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

        //        return Ok($"A categoria com o ID:{ID} foi excluída.");

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
