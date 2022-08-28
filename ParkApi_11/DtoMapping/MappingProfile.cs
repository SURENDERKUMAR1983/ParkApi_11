using AutoMapper;
using ParkApi_11.Dtos;
using ParkApi_11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkApi_11.DtoMapping
{
    public class MappingProfile : Profile
    { 
        public MappingProfile()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
        }

        
    }
}
