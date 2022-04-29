using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepsitory;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepsitory, ICategoryRepository categoryRepository)
        {
            _pieRepsitory = pieRepsitory;
            _categoryRepository = categoryRepository;
        }

        //public ViewResult List()
        //{
        //    PiesListViewModel piesListViewModel = new PiesListViewModel();
        //    piesListViewModel.Pies = _pieRepsitory.AllPies;

        //    piesListViewModel.CurrentCategory = "Cheese cakes";
        //    return View(piesListViewModel);
        //}

        public IActionResult Details(int id)
        {
            var pie = _pieRepsitory.GetPieById(id);
            if (pie == null)
                return NotFound();
            return View(pie);
        }

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if(string.IsNullOrEmpty(category))
            {
                pies = _pieRepsitory.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepsitory.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });

        }
    }
}

