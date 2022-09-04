using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Data.EntityFramework.Configurations
{
   
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
           
            builder.Property(p => p.ProductName);
            builder.Property(p => p.ProductCategoryId);
            builder.Property(p => p.Price);
            builder.Property(p => p.CreatedDate);
            builder.Property(p => p.State);
            builder.Property(p => p.IsDeleted);
        }
    }
}
