using LanchesApp.Repositories;
using LanchesApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesApp.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        //Método para listar os lanches
        public ActionResult List()
        {
            //Formas de passar dados do controlador para a View
            ViewBag.Lanche = "lanches";
            ViewData["Categoria"] = "Categoria";


            //var lanches = _lancheRepository.Lanches;
            //return View(lanches);

            var lanchesListViewModel = new LanchesListViewModel();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria Atual";

            return View(lanchesListViewModel);
        }
    }
}
