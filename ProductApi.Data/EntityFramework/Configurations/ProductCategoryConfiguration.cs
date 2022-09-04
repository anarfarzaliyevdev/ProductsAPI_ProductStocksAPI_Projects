using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApi.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Data.EntityFramework.Configurations
{
    internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(p => p.ProductCategoryId);
       
            builder.Property(p => p.ProductCategoryName);
           
        }
    }
}
