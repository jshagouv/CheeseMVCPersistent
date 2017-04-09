using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //In addition to getting cheeses list, also get associated
            //categories for each cheese in list (using Linq syntax)
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(context.Categories.ToList());
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                //Use Linq syntax to retrieve a specific category ID... so when form 
                //is submitted, CategoryID from the form should correspond to an ID from the Categories DB table
                CheeseCategory newCheeseCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCheeseCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        //Display all cheeses within a given category
        public IActionResult Category (int id)
        {
            if (id == 0)
            {
                return Redirect("/Category");
            }

            CheeseCategory theCategory = context.Categories
                .Include(cat => cat.Cheeses)
                .Single(cat => cat.ID == id);

            //Alternate way to achieve the same result by querying for the cheeses
            //from the other side of the relationship (i.e., start with list of cheeses
            //and filter down to the ones with the proper category ID). But this way
            //makes it harder to get Category Name.
            /*
             * IList<Cheese> theCheeses = context.Cheeses
             * .Include(c => c.Category)
             * .Where(c => c.CategoryID == id)
             * .ToList();
            */

            ViewBag.title = "Cheeses in category: " + theCategory.Name;
            return View("Index", theCategory.Cheeses);
        }
    }
}
