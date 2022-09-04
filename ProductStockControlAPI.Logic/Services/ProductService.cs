using ProductApi.Shared.Models.Entities;
using ProductStockControlAPI.Logic.Abstracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ProductApi.Shared.Models.RequestInputModels;

namespace ProductStockControlAPI.Logic.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Product> GetProduct(int productId)
        {
            var product = await _httpClient.GetFromJsonAsync<Product>($"api/products/{productId}");


            return product;
        }
        

       
    }
}
