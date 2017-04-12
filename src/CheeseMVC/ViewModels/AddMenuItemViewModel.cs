using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheeseMVC.Models;


namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int MenuID { get; set; }
        public int CheeseID { get; set; }

        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel() { }

        //Used to render form
        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Cheeses = new List<SelectListItem>();
            
            foreach (var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }
            //Set Menu property within the class
            Menu = menu;
        }
    }
}
