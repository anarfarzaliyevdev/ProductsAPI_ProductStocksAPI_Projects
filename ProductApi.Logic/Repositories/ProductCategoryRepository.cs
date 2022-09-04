using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Contexts;
using ProductApi.Logic.Abstracts;
using ProductApi.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Logic.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ProductDbContext appDbContext;
        private readonly IMapper _mapper;
        public ProductCategoryRepository(ProductDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<List<ProductCategory>> GetCategories()
        {
            return await appDbContext.ProductCategories.ToListAsync();
        }
    }
}
