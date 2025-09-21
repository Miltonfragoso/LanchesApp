using LanchesApp.Models;
using System.Collections;
using System.Collections.Generic;

namespace LanchesApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
