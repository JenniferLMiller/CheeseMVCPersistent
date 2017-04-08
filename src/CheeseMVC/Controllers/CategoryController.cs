using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<CheeseCategory> cheeseCategories = context.Categories.ToList();

            return View(cheeseCategories);
        }

        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to the category table
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = addCategoryViewModel.Name,
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/Category");
            }
           
            return View(addCategoryViewModel);
        }
    }
}
