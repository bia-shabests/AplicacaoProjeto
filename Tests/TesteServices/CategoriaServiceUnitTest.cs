﻿using ApplicationServices.Services;
using Castle.Core.Logging;
using Domain.Models;
using Domain.Repositorys;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.TesteServices
{
    [TestClass]
    public class CategoriaServiceUnitTest
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<ILogger<CategoriaService>> _logger;
        private readonly CategoriaService _categoriaService;

        public CategoriaServiceUnitTest()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _logger = new Mock<ILogger<CategoriaService>>();
            _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object, _logger.Object);
        }

        [TestMethod]
        public async Task SalvarCategoria_Valido_DeveSalvarERetornarCategoria()
        {
            // Arrange
            string nome = "Bebidas";
            var categoriaEsperada = new Categoria { ID = 1, Nome = nome, Status = true };

            _categoriaRepositoryMock
                .Setup(r => r.CadastrarCategoria(It.IsAny<Categoria>()))
                .ReturnsAsync(categoriaEsperada);

            // Act
            var resultado = await _categoriaService.SalvarCategoria(nome);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(nome, resultado.Nome);
            Assert.IsTrue(resultado.Status);
            _categoriaRepositoryMock.Verify(r => r.CadastrarCategoria(It.IsAny<Categoria>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task SalvarCategoria_NomeVazio_DeveLancarArgumentException()
        {
            // Arrange
            string nomeInvalido = "   ";

            // Act
            await _categoriaService.SalvarCategoria(nomeInvalido);

            // Assert é tratado pelo ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task BuscarCategorias_OrdenacaoInvalida_DeveLancarArgumentException()
        {
            // Arrange
            string ordenacaoInvalida = "INVALID";

            // Act
            await _categoriaService.BuscarCategorias(null, null, null, null, ordenacaoInvalida);

            // Assert é tratado pelo ExpectedException
        }

        [TestMethod]
        public async Task BuscarCategorias_ParametrosValidos_DeveChamarRepositorio()
        {
            // Arrange
            var categorias = new List<Categoria>
            {
                new Categoria { ID = 1, Nome = "Bebidas", Status = true },
                new Categoria { ID = 2, Nome = "Comidas", Status = true }
            };

            _categoriaRepositoryMock
                .Setup(r => r.BuscarCategorias(null, null, null, "ID", "ASC"))
                .ReturnsAsync(categorias);

            // Act
            var resultado = await _categoriaService.BuscarCategorias(null, null, null, null, "ASC");

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count);
            _categoriaRepositoryMock.Verify(r => r.BuscarCategorias(null, null, null, "ID", "ASC"), Times.Once);
        }

        [TestMethod]
        public async Task EditarCategoria_DeveRetornarCategoria_QuandoCategoriaExistir()
        {
            // Arrange
            var categoriaEsperada = new Categoria { ID = 1, Nome = "Nova Categoria" };
            _categoriaRepositoryMock
                .Setup(r => r.AtualizarCategoria(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(categoriaEsperada);

            // Act
            var resultado = await _categoriaService.EditarCategoria(1, "Nova Categoria");

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(categoriaEsperada.ID, resultado.ID);
            Assert.AreEqual(categoriaEsperada.Nome, resultado.Nome);
        }

        [TestMethod]
        public async Task EditarCategoria_DeveChamarRepositorio_QuandoNomeValido()
        {
            // Arrange
            var categoriaEsperada = new Categoria { ID = 1, Nome = "Valido" };
            _categoriaRepositoryMock
                .Setup(r => r.AtualizarCategoria(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(categoriaEsperada);

            // Act
            var resultado = await _categoriaService.EditarCategoria(1, "Valido");

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Valido", resultado.Nome);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task EditarCategoria_DeveLancarExcecao_QuandoNomeForVazio()
        {
            await _categoriaService.EditarCategoria(1, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task EditarCategoria_DeveLancarExcecao_QuandoNomeExceder150Caracteres()
        {
            var nomeLongo = new string('A', 151);
            await _categoriaService.EditarCategoria(1, nomeLongo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task EditarCategoria_DeveLancarExcecao_QuandoNomeTiverSimbolos()
        {
            await _categoriaService.EditarCategoria(1, "Categoria!@#");
        }
    }
}