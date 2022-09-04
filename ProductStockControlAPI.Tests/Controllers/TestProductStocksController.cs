using Microsoft.AspNetCore.Mvc;
using ProductStockControlAPI.Controllers;
using ProductStockControlAPI.Logic.Abstracts;
using ProductStockControlAPI.Models.Entities;
using ProductStockControlAPI.Models.RequestInputModels;
using ProductStockControlAPI.Models.RequestOutModels;
using ProductStockControlAPI.Tests.Repositories;
using ProductStockControlAPI.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductStockControlAPI.Tests.Controllers
{
    public class TestProductStocksController
    {
        private readonly IProductStockRepository _productStockRepository;
        private readonly IProductService _productService;
        private readonly ProductStocksController _controller;
        public TestProductStocksController()
        {
            _productService = new ProductServiceFake();
            _productStockRepository = new ProductStockRepositoryFake(_productService);
       
            _controller = new ProductStocksController(_productStockRepository, _productService);
        }

        [Fact]
        public async Task AddStocks_WhenCalled_ReturnsOkResult()
        {
            //Arrange 
            AddStockInputModel addStockInputModel = new AddStockInputModel();
            addStockInputModel.ProductId = 1;
            addStockInputModel.NewAddedProductCount = 5;
            // Act
            var okResult = await _controller.AddStock(addStockInputModel);
            // Assert
            Assert.IsType<OkResult>(okResult);
        }

        [Fact]
        public async Task AddStocks_WhenCalled_ReturnsBadRequest()
        {
            //Arrange 
            AddStockInputModel addStockInputModel = new AddStockInputModel();
            addStockInputModel.ProductId = 100;
            addStockInputModel.NewAddedProductCount = 5;
            //Act
            var badResult = await _controller.AddStock(addStockInputModel);
            // Assert
            Assert.IsType<ObjectResult>(badResult);
        }

        [Fact]
        public async Task RemoveStocks_WhenCalled_ReturnsOkResult()
        {
            //Arrange 
            RemoveStockInputModel removeStockInputModel = new RemoveStockInputModel();
            removeStockInputModel.ProductId = 1;
            removeStockInputModel.NewSoldProductCount = 5;
            //Act
            var okResult = await _controller.RemoveStock(removeStockInputModel);
            // Assert
            Assert.IsType<OkResult>(okResult);
        }
        [Fact]
        public async Task RemoveStocks_WhenCalled_ReturnsBadResult()
        {
            //Arrange 
            RemoveStockInputModel removeStockInputModel = new RemoveStockInputModel();
            removeStockInputModel.ProductId = 99;
            removeStockInputModel.NewSoldProductCount = 5;
            //Act
            var badResult = await _controller.RemoveStock(removeStockInputModel);
            // Assert
            Assert.IsType<ObjectResult>(badResult);
        }
        [Fact]
        public async Task GetStocks_WhenCalled_ReturnsOkResult()
        {
            //Arrange 
            var productId = 1;
            //Act
            var okResult = await _controller.GetStock(productId);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public async Task GetStocks_WhenCalled_ReturnsRightItem()
        {
            //Arrange 
            var productId = 1;
            //Act
            var actionResult = (await _controller.GetStock(productId)).Result;
            var okResult = actionResult as OkObjectResult;
            var productStockOutputModel = okResult.Value as GetStockOutputModel;
            // Assert
            Assert.IsType<GetStockOutputModel>(productStockOutputModel);
            Assert.Equal(productId, productStockOutputModel.ProductId);

        }
    }
}
