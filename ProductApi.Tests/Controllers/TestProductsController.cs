using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Controllers;
using ProductApi.Logic.Abstracts;
using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.RequestInputModels;
using ProductApi.Tests.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductApi.Tests.Controllers
{
    public class TestProductsController
    {
        private readonly ProductsController _controller;
        private readonly IProductRepository _repository;
        public TestProductsController()
        {
            _repository = new ProductRepositoryFake();
            _controller = new ProductsController(_repository);
        }
       
        [Fact]
        public async Task GetProducts_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult =await _controller.GetProducts() ;
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public async Task GetProducts_WhenCalled_ReturnsAllProducts()
        {
            // Act
            var okResult = await _controller.GetProducts() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetProduct_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult =await _controller.GetProduct(5) as ActionResult<Product>;
            // Assert
            Assert.IsType<ActionResult<Product>>(notFoundResult);
        }
        [Fact]
        public void GetProduct_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 2;
            // Act
            var okResult =  _controller.GetProduct(testId).Result ;
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public async Task Product_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 2;
            // Act
            var response = await  _controller.GetProduct(testId);
            var okResult = response.Result as OkObjectResult;
            var product = okResult.Value as Product;
            // Assert
            Assert.IsType<Product>(product);
            Assert.Equal(testId, product.ProductId);
        }

        [Fact]
        public async Task  AddProduct_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var categoryWrongItem = new ProductCreateViewModel()
            {
                Price=10,
                ProductCategoryId=2,
                ProductName="Test"
            };
            _controller.ModelState.AddModelError("ProductCategoryId","Wrong category id");
            // Act
            var badResponse =await _controller.CreateProduct(categoryWrongItem);
            // Assert
            Assert.IsType<ActionResult<Product>>(badResponse);
        }
        [Fact]
        public async Task AddProduct_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            ProductCreateViewModel testItem = new ProductCreateViewModel()
            {
                Price = 10,
                ProductCategoryId = 1,
                ProductName = "Test"
            };
            // Act
            var createdResponse =(await _controller.CreateProduct(testItem)).Result;
            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]
        public async Task AddProduct_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new ProductCreateViewModel()
            {
                Price = 10,
                ProductCategoryId = 1,
                ProductName = "Test2"
            };
            // Act
            var createdResponse =await _controller.CreateProduct(testItem) ;
            var actionResult =createdResponse.Result as CreatedAtActionResult;
            var item = actionResult.Value as Product;
            // Assert
            Assert.IsType<Product>(item);
            Assert.Equal("Test2", item.ProductName);
        }
        [Fact]
        public async Task  DeleteProduct_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 10;

            // Act
            var badResponse = (await _controller.DeleteProduct(notExistingId)).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse);
        }

        [Fact]
        public async Task DeleteProduct_ExistingIdPassed_ReturnsOkresult()
        {
            // Arrange
            var existingId = 1;

            // Act
            var okResponse = (await _controller.DeleteProduct(existingId)).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public async Task UpdateProduct_NotExistingIdPassed_ReturnsNotFound()
        {
            // Arrange
            var modelMissingId = new ProductUpdateViewModel() { Price=10,ProductCategoryId=1,ProductId=10,ProductName="TestName"};

            // Act
            var notFoundResult = (await _controller.UpdateProduct(modelMissingId)).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }
        [Fact]
        public async Task  UpdateProduct_ExistingIdPassed_ReturnsOkResponse()
        {
            // Arrange
            var modelMissingId = new ProductUpdateViewModel() { Price = 10, ProductCategoryId = 1, ProductId = 1, ProductName = "TestName" };

            // Act
            var okResponse =await _controller.UpdateProduct(modelMissingId) as ActionResult<Product>;
            var item = okResponse.Value as Product;
            // Assert
            Assert.IsType<Product>(item);
            Assert.Equal("TestName", item.ProductName);

        }
    }
}
