using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant_Category>().HasKey(pc => new
            {
                pc.PlantId,
                pc.CategoryId
            });


            modelBuilder.Entity<Plant_Category>().HasOne(p => p.Plant).WithMany(pc => pc.Plants_Categories).HasForeignKey(p => p.PlantId);
            modelBuilder.Entity<Plant_Category>().HasOne(c => c.Category).WithMany(pc => pc.Plants_Categories).HasForeignKey(p => p.CategoryId);


            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Plant_Category> Plants_Categories { get; set; }
                //ORDERS TABLES

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPlant> OrderPlant { get; set; }
        public DbSet<ShoppingCardItem> ShoppingCardItems { get; set; }
    }
}
