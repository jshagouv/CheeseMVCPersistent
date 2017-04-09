namespace CheeseMVC.Models
{
    public class Cheese
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        //The "one side" of the one-to-many relationship...
        //A foreign key to link a cheese to a single CategoryID
        public int CategoryID { get; set; }
        //Navigation property that corresponds to above
        //foreign key; the key is what is stored in the DB but
        //this is the object that corresponds to that key:
        public CheeseCategory Category { get; set; }
    }
}
