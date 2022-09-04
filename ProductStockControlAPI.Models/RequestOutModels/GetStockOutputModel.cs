using System;
using System.Collections.Generic;
using System.Text;

namespace ProductStockControlAPI.Models.RequestOutModels
{
    public class GetStockOutputModel
    {
        public int ProductId { get; set; }
        public int StockCount { get; set; }
    }
}
