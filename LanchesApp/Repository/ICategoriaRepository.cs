using LanchesApp.Models;
using System.Collections.Generic;

namespace LanchesApp.Repositories
{
    public interface ICategoriaRepository
    {
        //Esse método nos permite somente ler os dados dessa coleção por conta da palavra "get"
        IEnumerable<Categoria> Categorias { get; }
    }
}
