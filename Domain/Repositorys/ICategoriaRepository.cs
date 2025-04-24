using Domain.Models;

namespace Domain.Repositorys_interfaces_
{
    public interface ICategoriaRepository
    {
        Task<Categoria> CadastrarCategoria(Categoria categoria);
    }
}
