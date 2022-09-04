using ProductApi.Shared.Models.Entities;
using ProductStockControlAPI.Logic.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStockControlAPI.Tests.Services
{
    public class ProductServiceFake : IProductService
    {
        private readonly List<Product> _products;
        public ProductServiceFake()
        {
            _products = new List<Product>()
            {
                new Product() { ProductId = 1,
                    ProductName = "Table",  CreatedDate=DateTime.Now,IsDeleted=false,Price=10,ProductCategoryId=1,State=true },
                new Product() { ProductId = 2,
                    ProductName = "Apple",  CreatedDate=DateTime.Now,IsDeleted=false,Price=6,ProductCategoryId=1,State=true },
               new Product() { ProductId = 3,
                    ProductName = "Chair",  CreatedDate=DateTime.Now,IsDeleted=false,Price=5,ProductCategoryId=1,State=true }
            };
        }
        public async Task<Product> GetProduct(int productId)
        {
           return await Task.FromResult(_products.FirstOrDefault(p => p.ProductId == productId));
        }
    }
}
