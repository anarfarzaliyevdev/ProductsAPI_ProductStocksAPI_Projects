using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Shared.Models.RequestInputModels
{
    public class ProductCreateViewModel
    {
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public float Price { get; set; }
    }
}
