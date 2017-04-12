using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    //Join class for Cheese and Menu
    //Plays the role of a join table in an OO setting
    public class CheeseMenu
    {
        //Each one of these corresponds to a relationship
        //In this case many-to-many
        public int CheeseID { get; set; }
        public Cheese Cheese { get; set; }

        public int MenuID { get; set; }
        public Menu Menu { get; set; } 
    }
}
