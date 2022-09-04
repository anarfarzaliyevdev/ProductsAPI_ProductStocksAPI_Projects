using ProductApi.Logic.Abstracts;
using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.Enums;
using ProductApi.Shared.Models.RequestInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Tests.Repositories
{
    public class ProductRepositoryFake : IProductRepository
    {
        private readonly List<Product> _products;
        public ProductRepositoryFake()
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
       

        public async Task<Product> AddProduct(ProductCreateViewModel product)
        {
            var newProduct = new Product();
            newProduct.CreatedDate = DateTime.Now;
            newProduct.IsDeleted = false;
            newProduct.ProductId= (_products.FirstOrDefault(p => p.ProductId == _products.Count).ProductId) + 1;
            newProduct.Price = product.Price;
            newProduct.ProductName = product.ProductName;
            _products.Add(newProduct);
            return await Task.FromResult(newProduct);
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            var existing = _products.First(p => p.ProductId == productId);
            var removedProduct = existing;
            _products.Remove(existing);
            return await Task.FromResult(removedProduct);
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.ProductId == productId));
        }

        public async Task<List<Product>> GetProducts()
        {
            return await Task.FromResult(_products);
        }

        public async Task<Product> UpdateProduct(ProductUpdateViewModel product)
        {
            var existing = _products.First(p => p.ProductId == product.ProductId);
            existing.ProductCategoryId = product.ProductCategoryId;
            existing.ProductName = product.ProductName;
            existing.Price = product.Price;
            return await Task.FromResult(existing);
        }

       
    }
}
