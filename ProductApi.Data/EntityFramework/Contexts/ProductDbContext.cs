using Microsoft.EntityFrameworkCore;
using ProductApi.Data.EntityFramework.Configurations;
using ProductApi.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Data.Contexts
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
           : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductCategoryConfiguration());
            builder.Entity<ProductCategory>().HasData(
                             new ProductCategory { ProductCategoryId = 1, ProductCategoryName="Category 1" },
                            new ProductCategory { ProductCategoryId = 2, ProductCategoryName = "Category 2" },
                             new ProductCategory { ProductCategoryId = 3, ProductCategoryName = "Category 3" });
            base.OnModelCreating(builder);

        }
    }
}
