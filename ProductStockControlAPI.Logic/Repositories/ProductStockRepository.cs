using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.Enums;
using ProductApi.Shared.Models.RequestInputModels;
using ProductStockControlAPI.Data.EntityFramework.Contexts;
using ProductStockControlAPI.Logic.Abstracts;
using ProductStockControlAPI.Models.Entities;
using ProductStockControlAPI.Models.RequestInputModels;
using ProductStockControlAPI.Models.RequestOutModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductStockControlAPI.Logic.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly ProductStockDBContext appDbContext;
        //private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductStockRepository(ProductStockDBContext appDbContext, /*IMapper mapper,*/IProductService productService)
        {
            this.appDbContext = appDbContext;
            //_mapper = mapper;
            _productService = productService;


        }
        public async Task AddStocks(AddStockInputModel addStockInputModel)
        {
            var productStock = await appDbContext.ProductStocks
                     .FirstOrDefaultAsync(p => p.ProductId == addStockInputModel.ProductId);
            if (productStock != null)
            {
                productStock.ProductCount += addStockInputModel.NewAddedProductCount < 0 ? 0 : addStockInputModel.NewAddedProductCount;

                await appDbContext.SaveChangesAsync();

            }
            else
            {
                var product = _productService.GetProduct(addStockInputModel.ProductId);
                if (product != null)
                {
                    ProductStock newProductStock = new ProductStock();
                    newProductStock.ProductId = addStockInputModel.ProductId;
                    newProductStock.ProductCount = addStockInputModel.NewAddedProductCount;
                    await appDbContext.ProductStocks.AddAsync(newProductStock);
                    await appDbContext.SaveChangesAsync();
                }
                else
                {
                    Log.Warning("Product not found while adding stock");
                }
            }
        }

        public async Task<GetStockOutputModel> GetStock(int productId)
        {
            var productStock = await appDbContext.ProductStocks
                    .FirstOrDefaultAsync(p => p.ProductId == productId);
            var getStockOutputModel = new GetStockOutputModel();
            if (productStock != null)
            {

                getStockOutputModel.ProductId = productStock.ProductId;
                getStockOutputModel.StockCount = productStock.ProductCount;
                await appDbContext.SaveChangesAsync();

            }
            else
            {
                getStockOutputModel = null;
            }
            return getStockOutputModel;
        }

        public async Task RemoveStock(RemoveStockInputModel removeStockInputModel)
        {

            var productStock = await appDbContext.ProductStocks
                      .FirstOrDefaultAsync(p => p.ProductId == removeStockInputModel.ProductId);
            if (productStock != null)
            {
                if (productStock.ProductCount < removeStockInputModel.NewSoldProductCount)
                {
                    productStock.ProductCount = 0;


                }
                else
                {

                    productStock.ProductCount -= removeStockInputModel.NewSoldProductCount < 0 ? 0 : removeStockInputModel.NewSoldProductCount; ;

                }
                await appDbContext.SaveChangesAsync();

            }
            else
            {
                Log.Warning("Requested product stock item not found ");
            }
        }



    }
}
