using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.RequestInputModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Logic.Abstracts
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> AddProduct(ProductCreateViewModel product);
        Task<Product> UpdateProduct(ProductUpdateViewModel product);
        Task<Product> DeleteProduct(int productId);
        Task<Product> GetProduct(int productId);
      
    }
}
