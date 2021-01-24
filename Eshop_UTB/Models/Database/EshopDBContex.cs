using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop_UTB.Models.Database.Configurations;
using Eshop_UTB.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Eshop_UTB.Models.Database
{
    public class EshopDBContex : IdentityDbContext<User,Role,int>
    {

        public EshopDBContex(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Carousel> Carousels { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Eshop_UTB.Models.Identity.User> User { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CarouselConfiguration());

          

        }

        
    }
}
