using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Contexts;
using ProductApi.Logic.Abstracts;
using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.Enums;
using ProductApi.Shared.Models.RequestInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Logic.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext appDbContext;
        private readonly IMapper _mapper;
        public ProductRepository(ProductDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Product> AddProduct(ProductCreateViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            product.CreatedDate = DateTime.Now;
            product.IsDeleted = false;
            product.State = true;
            var newProduct = (await appDbContext.Products.AddAsync(product)).Entity;
           
                await appDbContext.SaveChangesAsync();
            
            
          
            return newProduct;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            var product = await appDbContext.Products
                   .FirstOrDefaultAsync(p=>p.ProductId==productId);
            if (product != null)
            {
                product.IsDeleted = true;
                await appDbContext.SaveChangesAsync();
                return product;
            }
            return null;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await appDbContext.Products.Where(p=>p.IsDeleted==false).ToListAsync();
        }

        public async Task<Product> UpdateProduct(ProductUpdateViewModel productUpdateViewModel)
        {
            var product = await appDbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productUpdateViewModel.ProductId);
            if (product != null)
            {
         
                product.ProductName = productUpdateViewModel.ProductName;
                product.Price = productUpdateViewModel.Price;
                product.ProductCategoryId = productUpdateViewModel.ProductCategoryId;
                await appDbContext.SaveChangesAsync();

                return product;
            }
            return null;
        }
    
        public async Task<Product> GetProduct(int productId)
        {
            return await appDbContext.Products.Where(p => p.IsDeleted == false)
                .FirstOrDefaultAsync(p=>p.ProductId== productId);
        }
    }
}
