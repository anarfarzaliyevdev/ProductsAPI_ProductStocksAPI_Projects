using ProductStockControlAPI.Models.Entities;
using ProductStockControlAPI.Models.RequestInputModels;
using ProductStockControlAPI.Models.RequestOutModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductStockControlAPI.Logic.Abstracts
{
    public interface IProductStockRepository
    {
        Task AddStocks(AddStockInputModel addStockInputModel);
        Task RemoveStock(RemoveStockInputModel removeStockInputModel);
        Task<GetStockOutputModel> GetStock(int productId);
    }
}
