using Dapper;
using DataAccess.Common;
using Domain.Models;
using Domain.Repositorys_interfaces_;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositorys
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string _connectionStringMySql;
        private readonly IConfiguration _configuration;
        public CategoriaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionStringMySql = _configuration.GetConnectionString("MySqlConnection")!;
        }

        public async Task<Categoria> CadastrarCategoria(Categoria categoria)
        {
            var parametros = new DynamicParameters();
            parametros.Add("Nome", categoria.Nome);
            parametros.Add("Status", categoria.Status);
            parametros.Add("DataCriacao", categoria.DataCriacao);
            parametros.Add("DataModificacao", categoria.DataModificacao);

            var resultado = await DatabaseExecutor.QueryFirstOrDefaultAsync<int>(_connectionStringMySql,"Categoria_CadastrarCategoria", parametros);

            categoria.ID = resultado;
            return categoria;
        }

        public async Task<List<Categoria>> BuscarCategoriasAsync(int? id, string nome, string status, string ordenarPor = "Id", string ordenacao = "ASC")
        {
            var parametros = new DynamicParameters();
            parametros.Add("p_Id", id);
            parametros.Add("p_Nome", nome);
            parametros.Add("p_Status", status);
            parametros.Add("p_OrdenarPor", ordenarPor);
            parametros.Add("p_Ordenacao", ordenacao);

            return await DatabaseExecutor.QueryAsync<Categoria>(_connectionStringMySql,"Categoria_BuscarCategorias",parametros);
        }
    }
}
