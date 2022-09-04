using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Logic.Abstracts;
using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.RequestInputModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
               
                return Ok(await _productRepository.GetProducts());
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var result = await _productRepository.GetProduct(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductCreateViewModel productViewModel)
        {
            try
            {
                if (productViewModel == null)
                {
                    return BadRequest();
                }
            

                var newProduct = await _productRepository.AddProduct(productViewModel);

                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data in db");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct(ProductUpdateViewModel productUpdateViewModel)
        {
            try
            {

                var productToUpdate = await _productRepository.GetProduct(productUpdateViewModel.ProductId);
                if (productToUpdate == null)
                {
                    return NotFound($"Product with {productUpdateViewModel.ProductId} not found");
                }
                return await _productRepository.UpdateProduct(productUpdateViewModel);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in db");
            }
        }
   
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product == null)
                {
                    return NotFound($"Product with {id} not found");
                }
                return Ok(await _productRepository.DeleteProduct(id));
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data in db");
            }
        }
    }
}
