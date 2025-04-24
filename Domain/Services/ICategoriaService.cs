using Domain.Models;

namespace Domain.Services_Interfaces_
{
    public interface ICategoriaService
    {
        Task<Categoria> SalvarCategoria(string nomeCategoria);
        Task<List<Categoria>> BuscarCategorias(int? ID, string? nome, bool? status, string? ordenarPor, string tipoOrdenacao);
    }
}
