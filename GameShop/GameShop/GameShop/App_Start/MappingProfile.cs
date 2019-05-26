using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameShop.Models;
using GameShop.Dtos;

namespace GameShop.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Game, GameDto>();
            Mapper.CreateMap<CustomerType, CustomerTypeDto>();
            Mapper.CreateMap<Category, CategoryDto>();

            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<GameDto, Game>()
                .ForMember(c => c.Id, opt => opt.Ignore());

        }
    }
}