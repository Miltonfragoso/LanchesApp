using LanchesApp.Context;
using LanchesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LanchesApp.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        //Método para retornar lanches e a categoria do lanche
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(a => a.Categoria);

        //Método para retornar Lanches preferidos e as categorias
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(a => a.IsLanchePreferido).Include(a => a.Categoria);

        //Método para retornar um lanche pelo ID
        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

        }
    }
}
