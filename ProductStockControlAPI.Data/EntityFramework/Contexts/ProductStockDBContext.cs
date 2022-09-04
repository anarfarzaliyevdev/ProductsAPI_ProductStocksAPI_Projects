using Microsoft.EntityFrameworkCore;
using ProductStockControlAPI.Data.EntityFramework.Configurations;
using ProductStockControlAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Data.EntityFramework.Contexts
{
    public class ProductStockDBContext : DbContext
    {
        public ProductStockDBContext(DbContextOptions<ProductStockDBContext> options)
           : base(options)
        {
        }
        public DbSet<ProductStock> ProductStocks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductStockConfiguration());
     

            base.OnModelCreating(builder);

        }
    }
}
