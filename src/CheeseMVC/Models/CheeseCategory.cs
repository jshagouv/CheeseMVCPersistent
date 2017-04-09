using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class CheeseCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }

        //The fact that this is a list within the context of a single
        //CheeseCategory, ID's this as a One-to-Many relationship:
        //For each CheeseCategory, there are many Cheeses associated with that category
        //This is a list of the objects the particular CheeseCategory owns.
        public IList<Cheese> Cheeses { get; set; }
    }
}
