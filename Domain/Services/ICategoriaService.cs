using Domain.Models;

namespace Domain.Services_Interfaces_
{
    public interface ICategoriaService
    {
        Task<Categoria> SalvarCategoria(string nomeCategoria);
    }
}
