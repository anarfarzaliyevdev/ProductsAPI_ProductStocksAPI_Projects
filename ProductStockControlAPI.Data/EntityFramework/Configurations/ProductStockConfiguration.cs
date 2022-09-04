using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStockControlAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Data.EntityFramework.Configurations
{
    
    internal class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProductId);
            builder.Property(p => p.ProductCount);

        }
    }
}
