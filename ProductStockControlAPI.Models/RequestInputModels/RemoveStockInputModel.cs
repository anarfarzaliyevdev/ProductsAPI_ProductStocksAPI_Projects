using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Models.RequestInputModels
{
    public class RemoveStockInputModel
    {
        public int ProductId { get; set; }
        public int NewSoldProductCount { get; set; }
    }
}
