using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LanchesApp.Repositories;
using LanchesApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel 
            { 
                LanchesPreferidos = _lancheRepository.LanchesPreferidos 
            };

            return View(homeViewModel);
        }

    }
}
