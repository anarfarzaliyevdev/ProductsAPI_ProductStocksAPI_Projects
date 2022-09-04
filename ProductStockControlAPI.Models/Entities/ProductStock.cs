using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Models.Entities
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }
}
