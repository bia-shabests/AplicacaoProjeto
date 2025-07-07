using Domain.Models;
using Domain.Services_Interfaces_;
using static Dapper.SqlMapper;
using System;
using Domain.Repositorys_interfaces_;
using System.Text.RegularExpressions;

namespace ApplicationServices
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Categoria> SalvarCategoria(string nomeCategoria)
        {
            ValidarCategoria(nomeCategoria);

            var categoria = new Categoria
            {
                Nome = nomeCategoria,
                Status = true,
                DataCriacao = DateTime.Now.ToLocalTime(),
                DataModificacao = null
            };

            return await _categoriaRepository.CadastrarCategoria(categoria);
        }
        
        public async Task<List<Categoria>> BuscarCategorias(int? ID, string? nome, bool? status, string? ordenarPor, string tipoOrdenacao)
        {
            if (tipoOrdenacao == null || tipoOrdenacao != "ASC" && tipoOrdenacao != "DESC")
            {
                throw new ArgumentException("O parâmetro 'ordenacao' deve ser 'ASC' ou 'DESC'.");
            }
            string campoOrdenacao = string.IsNullOrEmpty(ordenarPor) ? "ID" : ordenarPor;

            return await _categoriaRepository.BuscarCategorias(ID, nome, status, campoOrdenacao, tipoOrdenacao);
        }

        private void ValidarCategoria(string nomeCategoria)
        {
            if (string.IsNullOrWhiteSpace(nomeCategoria))
                throw new ArgumentException("O nome é obrigatório.");

            if (nomeCategoria.Length > 150)
                throw new ArgumentException("O nome não pode ter mais que 150 caracteres.");

            if (!Regex.IsMatch(nomeCategoria, @"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$"))
                throw new ArgumentException("O nome não pode conter símbolos. Apenas letras e acentuação são permitidas.");
        }





        //CREATE TABLE[Projeto].Subcategoria
        //(
        //ID INT identity(1,1) not null //incluir PRIMARY KEY
        //,Nome VARCHAR(100)NOT NULL
        //, Status BIT NOT NULL
        //,DataCriacao DATETIME NOT NULL
        //, DataModificacao DATETIME
        //FOREIGN KEY(ID) REFERENCES [Projeto].Categoria(ID)
        //)

        //o primeiro ID é da tabela atual (ou seja, tabela de subcategoria) e a REFERENCE que vai ser nossa FK é da Tabela Categoria

    }
}
