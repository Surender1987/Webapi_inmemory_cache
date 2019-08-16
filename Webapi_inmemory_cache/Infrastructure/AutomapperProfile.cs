using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapi_inmemory_cache.DataaccessLayer.Models;
using Webapi_inmemory_cache.Models;

namespace Webapi_inmemory_cache.Infrastructure
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
