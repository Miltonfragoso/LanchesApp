using LanchesApp.Models;
using System.Collections.Generic;

namespace LanchesApp.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
