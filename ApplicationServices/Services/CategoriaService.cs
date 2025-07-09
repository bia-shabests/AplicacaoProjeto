using Domain.Models;
using Domain.Services;
using Domain.Repositorys;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriaService> _logger;

        public CategoriaService(ICategoriaRepository categoriaRepository, ILogger<CategoriaService> logger)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }

        public async Task<Categoria> SalvarCategoria(string nomeCategoria)
        {
            _logger.LogInformation("Iniciando validação da categoria: {NomeCategoria}", nomeCategoria);

            ValidarNomeCategoria(nomeCategoria);

            _logger.LogInformation("Validação concluída. Preparando objeto Categoria.");

            var categoria = new Categoria
            {
                Nome = nomeCategoria,
                Status = true,
                DataCriacao = DateTime.Now.ToLocalTime(),
                DataModificacao = null
            };

            _logger.LogInformation("Salvando categoria no repositório.");

            Categoria resultado = await _categoriaRepository.CadastrarCategoria(categoria);

            _logger.LogInformation("Categoria salva com sucesso. ID: {ID}", resultado.ID);

            return resultado;
        }

        public async Task<Categoria> EditarCategoria(int ID, string novoNomeCategoria)
        {
            _logger.LogInformation("Iniciando validação da categoria: {NomeCategoria}", novoNomeCategoria);

            ValidarNomeCategoria(novoNomeCategoria);

            _logger.LogInformation("Validação concluída. Preparando objeto Categoria para atualização.");

            Categoria categoriaAtualizada = await _categoriaRepository.AtualizarCategoria(ID, novoNomeCategoria, DateTime.Now);

            _logger.LogInformation("Atualização concluída");

            return categoriaAtualizada;
        }

        public async Task<List<Categoria>> BuscarCategorias(int? ID, string? nome, bool? status, string? ordenarPor, string tipoOrdenacao)
        {
            _logger.LogInformation("Iniciando busca de categorias. ID: {ID}, Nome: {Nome}, Status: {Status}, OrdenarPor: {OrdenarPor}, Ordenacao: {Ordenacao}",
                ID, nome, status, ordenarPor, tipoOrdenacao);

            if (tipoOrdenacao == null || tipoOrdenacao.ToUpper() != "ASC" && tipoOrdenacao.ToUpper() != "DESC")
            {
                _logger.LogWarning("Parâmetro de ordenação inválido: {Ordenacao}", tipoOrdenacao);
                throw new ArgumentException("O parâmetro 'ordenacao' deve ser 'ASC' ou 'DESC'.");
            }

            string campoOrdenacao = string.IsNullOrEmpty(ordenarPor) ? "ID" : ordenarPor;

            List<Categoria> resultado = await _categoriaRepository.BuscarCategorias(ID, nome, status, campoOrdenacao, tipoOrdenacao);

            _logger.LogInformation("Busca concluída. Total encontrado: {Quantidade}", resultado.Count);

            return resultado;
        }

        private void ValidarNomeCategoria(string nomeCategoria)
        {
            if (string.IsNullOrWhiteSpace(nomeCategoria))
            {
                _logger.LogWarning("Validação falhou: nome da categoria vazio ou em branco.");
                throw new ArgumentException("O nome é obrigatório.");
            }

            if (nomeCategoria.Length > 150)
            {
                _logger.LogWarning("Validação falhou: nome da categoria excede 150 caracteres.");
                throw new ArgumentException("O nome não pode ter mais que 150 caracteres.");
            }

            if (!Regex.IsMatch(nomeCategoria, @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$"))
            {
                _logger.LogWarning("Validação falhou: nome da categoria contém caracteres inválidos.");
                throw new ArgumentException("O nome não pode conter símbolos. Apenas letras e acentuação são permitidas.");
            }

            _logger.LogInformation("Validação da categoria concluída com sucesso.");
        }
    }
}