using Domain.Models;

namespace Domain.Repositorys
{
    public interface ICategoriaRepository
    {
        Task<Categoria> CadastrarCategoria(Categoria categoria);
        Task<List<Categoria>> BuscarCategorias(int? ID, string? nome, bool? status, string? ordenarPor, string ordenacao);
    }
}
