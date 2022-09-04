using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Logic.Abstracts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IProductCategoryRepository _productCategoryRepository;
        public CategoriesController(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {

                return Ok(await _productCategoryRepository.GetCategories());
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db");
            }
        }

    }
}
