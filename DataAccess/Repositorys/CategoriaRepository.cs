using Dapper;
using DataAccess.Common;
using Domain.Models;
using Domain.Repositorys;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositorys
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionStringSql;
        private readonly IConfiguration _configuration;
        public CategoriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionStringSql = _configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Categoria> CadastrarCategoria(Categoria categoria)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Nome", categoria.Nome);
            parametros.Add("@Status", categoria.Status);
            parametros.Add("@DataCriacao", categoria.DataCriacao);
            parametros.Add("@DataModificacao", categoria.DataModificacao);

            var resultado = await DatabaseExecutor.QueryFirstOrDefaultAsync<int>(_connectionStringSql,"Categoria_CadastrarCategoria", parametros);

            categoria.ID = resultado;
            return categoria;
        }

        public async Task<List<Categoria>> BuscarCategorias(int? ID, string? nome, bool? status, string? ordenarPor, string ordenacao)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ID", ID);
            parametros.Add("@Nome", nome);
            parametros.Add("@Status", status);
            parametros.Add("@Ordenar_Por", ordenarPor);
            parametros.Add("@Ordenacao", ordenacao);

            return await DatabaseExecutor.QueryAsync<Categoria>(_connectionStringSql,"Categoria_BuscarCategorias",parametros);
        }

        public async Task<Categoria> AtualizarCategoria(int ID, string novoNome, DateTime dataModificacao)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ID", ID);
            parametros.Add("@NovoNome", novoNome);
            parametros.Add("@DataModificacao", dataModificacao);

            return await DatabaseExecutor.QueryFirstOrDefaultAsync<Categoria>(_connectionStringSql, "Categoria_AtualizarCategoria", parametros);
        }
    }
}
