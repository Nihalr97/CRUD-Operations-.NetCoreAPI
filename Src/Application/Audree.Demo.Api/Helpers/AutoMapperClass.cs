using Audree.Demo.Application.DTO;
using Audree.Demo.Application.DTO.Admin;
using Audree.Demo.Core.Models;
using Audree.Demo.Core.Models.Admin;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audree.Demo.Api.Helpers
{
    public class AutoMapperClass : Profile
    {
        public AutoMapperClass()
        {
            CreateMap<PLClub, PLClubdto>().ReverseMap();
            CreateMap<Employ, EmployDTO>().ReverseMap();
        }
    }
}
