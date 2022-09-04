using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductApi.Shared.Models.Entities
{
    public class ProductCategory
    {
     
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
