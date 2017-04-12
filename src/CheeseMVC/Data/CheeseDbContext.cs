using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        public DbSet<Cheese> Cheeses { get; set; }
        public DbSet<CheeseCategory> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CheeseMenu> CheeseMenus { get; set; }

        //Constructor overloading (overriding??). This constructor needs to matchup
        //to settings in Startup.cs
        public CheeseDbContext(DbContextOptions<CheeseDbContext> options) 
            : base(options)
        { }

        //Sets up the composite primary key for join table CheeseMenus
        //protected means only this class and its children have access to this method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   //In class Garry had PKs defined for all tables, not
            //just join table
            //He said we had to create our tables before we reference 
            //them
            //modelBuilder.Entity<Cheese>().HasKey(c => c.ID);
            //modelBuilder.Entity<Menu>().HasKey(m => m.ID);

            //Define primary key for CheeseMenu record
            modelBuilder.Entity<CheeseMenu>()
                .HasKey(c => new { c.CheeseID, c.MenuID });

            //Garry also had things like this to explicitly define relationship:
            //modelBuilder.Entity<Cheese>().HasMany(c => c.CheeseMenus);
            //modelBuilder.Entity<Menu>().HasMany(m => m.CheeseMenus);
        }

    }
}
