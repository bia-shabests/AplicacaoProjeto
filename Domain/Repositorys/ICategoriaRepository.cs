using Domain.Models;

namespace Domain.Repositorys_interfaces_
{
    public interface ICategoriaRepository
    {
        Task<Categoria> CadastrarCategoria(Categoria categoria);
        Task<List<Categoria>> BuscarCategorias(int? ID, string? nome, bool? status, string? ordenarPor, string ordenacao);
    }
}
