using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Menu> Menus = context.Menu.ToList();
            return View(Menus);
        }

        // Display Add Menu screen
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        // Create a new menu object and add it to the database
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new menu to my existing menus
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name,
                };

                context.Menu.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }

            return View(addMenuViewModel);
        }

        // Display View Menu screen, called from /Menu/ViewMenu/<id>
        public IActionResult ViewMenu(int id)
        {
            Menu menu = context.Menu.Single(m => m.ID == id);

            List<CheeseMenu> items = context
                    .CheeseMenus
                    .Include(item => item.Cheese)
                    .Where(cm => cm.MenuID == id)
                    .ToList();

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel();
            viewMenuViewModel.Menu = menu;
            viewMenuViewModel.Items = items;

            return View(viewMenuViewModel);
        }

        // display form to add item to menu
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menu.Single(m => m.ID == id);
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, context.Cheeses.ToList());

            return View(addMenuItemViewModel);
        }


        // Add a new cheese to a menu and update the database
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                //verify that the cheese is not already assigned to the menu
                //if existingItems is not null, the relationship exists, and we can't
                //add this cheese to the cheeseMenu table
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseID)
                    .Where(cm => cm.MenuID == addMenuItemViewModel.MenuID).ToList();

                  if (existingItems.Count == 0)
                {
                    CheeseMenu newCheeseMenu = new CheeseMenu
                    {
                        CheeseID = addMenuItemViewModel.CheeseID,
                        MenuID = addMenuItemViewModel.MenuID
                    };

                    context.CheeseMenus.Add(newCheeseMenu);
                    context.SaveChanges();
                }

                return Redirect("/Menu/ViewMenu/" + addMenuItemViewModel.MenuID);
            }

            return View();
        }

    }
}
