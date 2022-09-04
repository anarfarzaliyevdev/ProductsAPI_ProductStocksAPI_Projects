using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Shared.Models.Entities;
using ProductStockControlAPI.Logic.Abstracts;
using ProductStockControlAPI.Logic.CustomExceptions;
using ProductStockControlAPI.Models.RequestInputModels;
using ProductStockControlAPI.Models.RequestOutModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStockControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStocksController : ControllerBase
    {
        private readonly IProductStockRepository _productStockRepositroy;
        private readonly IProductService _productService;
        public ProductStocksController(IProductStockRepository productStockRepositroy,IProductService productService)
        {
            _productStockRepositroy = productStockRepositroy;
            _productService = productService;
        }
        [HttpPost("addstock")]
        public async Task<ActionResult> AddStock(AddStockInputModel addStockInputModel)
        {
            try
            {
                
                var product = await _productService.GetProduct(addStockInputModel.ProductId);
                if (product == null)
                {
                   
                    throw new InvalidProductIdException(addStockInputModel.ProductId);
                   
                }
                await _productStockRepositroy.AddStocks(addStockInputModel);
                return Ok();
            }
            catch (InvalidProductIdException ex)
            {
                Log.Error($"{ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db. For details review logs");
            }
           
        }
        [HttpPost("removestock")]
        public async Task<ActionResult> RemoveStock(RemoveStockInputModel removeStockInputModel)
        {
            try
            {
                var productStock = await _productStockRepositroy.GetStock(removeStockInputModel.ProductId);
                if (productStock == null)
                {
                    Log.Error("InvalidProductId error appered");
                     throw new InvalidProductIdException(removeStockInputModel.ProductId);
               
                }
                else if ( productStock.StockCount == 0)
                {

                    Log.Error("OutOfStock error appered");
                    throw new OutOfStockException();
                
                }

                await _productStockRepositroy.RemoveStock(removeStockInputModel);

            }
            catch (InvalidProductIdException ex)
            {
                Log.Error($"{ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (OutOfStockException ex)
            {
                Log.Error($"{ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db. For details review logs");
            }
            return Ok();
        }
        [HttpGet("getstock/{productId}")]
        public async Task<ActionResult<GetStockOutputModel>> GetStock(int productId)
        {
            try
            {
             
                return Ok(await _productStockRepositroy.GetStock(productId));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.InnerException.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db. For details review logs");
            }
            
        }
    }
}
