using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Shared.Models.RequestInputModels
{
    public class ProductUpdateViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryId { get; set; }
        public float Price { get; set; }
    }
}
