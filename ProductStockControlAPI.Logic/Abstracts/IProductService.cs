using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.RequestInputModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductStockControlAPI.Logic.Abstracts
{
    public interface IProductService
    {
        Task<Product> GetProduct(int productId);
      
    }
}
