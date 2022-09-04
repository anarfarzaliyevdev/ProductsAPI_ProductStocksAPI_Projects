using ProductApi.Shared.Models.Entities;
using ProductStockControlAPI.Logic.Abstracts;
using ProductStockControlAPI.Models.Entities;
using ProductStockControlAPI.Models.RequestInputModels;
using ProductStockControlAPI.Models.RequestOutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStockControlAPI.Tests.Repositories
{
    public class ProductStockRepositoryFake : IProductStockRepository
    {
        private readonly List<ProductStock> _productStocks;
        private readonly IProductService _productService;
        public ProductStockRepositoryFake(IProductService productService)
        {
            _productStocks = new List<ProductStock>() { new ProductStock() { Id = 1, ProductCount = 5, ProductId = 1 } };
            _productService = productService;


        }
        public async Task AddStocks(AddStockInputModel addStockInputModel)
        {
            var ps = _productStocks.FirstOrDefault(ps => ps.ProductId == addStockInputModel.ProductId);
            if (ps == null)
            {
                var product = _productService.GetProduct(addStockInputModel.ProductId).Result;
                if (product != null)
                {
                    var id = (_productStocks.FirstOrDefault(p => p.Id == _productStocks.Count).Id) + 1;
                    var newProductStock = new ProductStock() { Id = id, ProductId = product.ProductId, ProductCount = addStockInputModel.NewAddedProductCount };

                    _productStocks.Add(newProductStock);
                }
             
            }
            else
            {
                ps.ProductCount += addStockInputModel.NewAddedProductCount;
            }
            
          
    
             await Task.CompletedTask;
        }

        public async Task<GetStockOutputModel> GetStock(int productId)
        {
            GetStockOutputModel outputModel = new GetStockOutputModel();
            var ps = _productStocks.Find(p => p.ProductId == productId);
            if (ps != null)
            {
                outputModel.ProductId = ps.ProductId;
                outputModel.StockCount = ps.ProductCount;
            }
           
            return await Task.FromResult(outputModel);
        }

        public async Task RemoveStock(RemoveStockInputModel removeStockInputModel)
        {
            var ps = _productStocks.FirstOrDefault(ps => ps.ProductId == removeStockInputModel.ProductId);
            if (ps != null)
            {
                if (ps.ProductCount < removeStockInputModel.NewSoldProductCount)
                {
                    ps.ProductCount = 0;
                }
                else
                {
                    ps.ProductCount -= removeStockInputModel.NewSoldProductCount;
                }

              
            }
            await Task.CompletedTask;

        }
    }
}
