using AutoMapper;
using MyAuthServerProject.Core.DTOs;
using MyAuthServerProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Service
{
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<UserApp,UserAppDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, AddProductDto>().ReverseMap();
        }
    }
}
