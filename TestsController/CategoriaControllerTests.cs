using Moq;
using Microsoft.AspNetCore.Mvc;
using Domain.Services;
using AplicacaoProjeto.Controllers;
using Domain.Models;
using Microsoft.Extensions.Logging;

public class CategoriaControllerTests
{
    private readonly Mock<ICategoriaService> _categoriaServiceMock;
    private readonly Mock<ILogger<CategoriaController>> _logger;
    private readonly CategoriaController _controller;

    public CategoriaControllerTests()
    {
        _categoriaServiceMock = new Mock<ICategoriaService>();
        _logger = new Mock<ILogger<CategoriaController>>();
        _controller = new CategoriaController(_categoriaServiceMock.Object, _logger.Object);
    }

    [Fact]
    public async Task SalvarCategoria_RetornaOkComCategoria()
    {
        // Arrange
        var categoria = new Categoria { ID = 1, Nome = "Bebidas" };
        _categoriaServiceMock
            .Setup(s => s.SalvarCategoria(It.IsAny<string>()))
            .ReturnsAsync(categoria);

        // Act
        var result = await _controller.CadastrarCategoria("Bebidas");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var retorno = Assert.IsType<Categoria>(okResult.Value);
        Assert.Equal("Bebidas", retorno.Nome);
    }

    [Fact]
    public async Task BuscarCategorias_RetornaOkComLista()
    {
        // Arrange
        var lista = new List<Categoria>
        {
            new Categoria { ID = 1, Nome = "Bebidas" },
            new Categoria { ID = 2, Nome = "Comidas" }
        };
        _categoriaServiceMock
            .Setup(s => s.BuscarCategorias(null, null, null, null, "ASC"))
            .ReturnsAsync(lista);

        // Act
        var result = await _controller.BuscarCategorias(null, null, null, null, "ASC");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var retorno = Assert.IsAssignableFrom<IEnumerable<Categoria>>(okResult.Value);
        Assert.Equal(2, ((List<Categoria>)retorno).Count);
    }

    [Fact]
    public async Task EditarCategoria_DeveRetornarOk_QuandoCategoriaExistir()
    {
        // Arrange
        var categoriaEsperada = new Categoria { ID = 1, Nome = "Nova Categoria" };
        _categoriaServiceMock.Setup(s => s.EditarCategoria(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(categoriaEsperada);

        // Act
        var resultado = await _controller.EditarCategoria(1, "Nova Categoria");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(resultado);
        var categoria = Assert.IsType<Categoria>(okResult.Value);
        Assert.Equal(categoriaEsperada.ID, categoria.ID);
    }

    [Fact]
    public async Task EditarCategoria_DeveRetornarNotFound_QuandoCategoriaNaoExistir()
    {
        // Arrange
        _categoriaServiceMock.Setup(s => s.EditarCategoria(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Categoria)null);

        // Act
        var resultado = await _controller.EditarCategoria(1, "Nova Categoria");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(resultado);
        Assert.Contains("Nenhuma categoria encontrada", notFoundResult.Value.ToString());
    }

    [Fact]
    public async Task EditarCategoria_DeveRetornarStatus500_QuandoLancarExcecao()
    {
        // Arrange
        _categoriaServiceMock.Setup(s => s.EditarCategoria(It.IsAny<int>(), It.IsAny<string>())).ThrowsAsync(new Exception("Erro interno"));

        // Act
        var resultado = await _controller.EditarCategoria(1, "Nova Categoria");

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(resultado);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }
}
