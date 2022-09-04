using AutoMapper;
using ProductApi.Shared.Models.Entities;
using ProductApi.Shared.Models.RequestInputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Mapping
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductCreateViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>().ReverseMap();
        }
    }
}
