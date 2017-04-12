using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }

            return View(addMenuViewModel);
        }

        public IActionResult ViewMenu(int id)
        {
            //Display all cheeses in menu. So from the join class
            //So we need Cheese IDs, so include Cheese
            //Where the MenuID is id
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            //get the menu
            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewMenuViewModel);
        }
        // /Menu/AddItem/menu_id
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            //To populate dropdown option, pass list of cheeses
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(menu, cheeses));
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuID;

                //Check to see if the composite ID already exists (we shouldn't
                //add the same cheeseID & menuID to CheeseMenu).
                //We don't call .Single because it doesn't allow for nothing to be returned
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        //Don't understand the Warning given in assignment
                        //instructions that seems to reference the query above
                        //and this step (the query above is exactly as specified
                        //in directions).
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };

                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();
                }

                return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
            }

            return View(addMenuItemViewModel);
        }
    }
}
