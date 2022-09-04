using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductStockControlAPI.Models.RequestInputModels
{
    public class AddStockInputModel
    {
        public int ProductId { get; set; }
        
        public int NewAddedProductCount { get; set; }
    }
}
