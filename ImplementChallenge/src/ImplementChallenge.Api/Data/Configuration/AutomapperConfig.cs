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
        //Este método não é chamado de nenhum ponto, ele é reconhecido apartir do startup na parte de  services.AddAutoMapper(typeof(Startup))
        public AutomapperConfig()
        {
            CreateMap<Curtidas, CurtidasViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();           
        }

    }
}
