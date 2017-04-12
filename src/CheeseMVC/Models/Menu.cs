using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }

        //A list of the join relationships (i.e., CheeseMenu composite IDs)
        public IList<CheeseMenu> CheeseMenus { get; set; } //= new List<CheeseMenu>();
    }
}
