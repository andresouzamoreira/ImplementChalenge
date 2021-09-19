using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImplementChallenge.Api.Domain;
using ImplementChallenge.Api.ViewModels;

namespace ImplementChallenge.Api.Data.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Curtidas, CurtidasViewModel>().ReverseMap();
        }

    }
}
