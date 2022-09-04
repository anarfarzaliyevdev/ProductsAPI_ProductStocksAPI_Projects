using ProductApi.Shared.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Logic.Abstracts
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetCategories();
    }
}
