using AutoMapper;
using SampleApp.Models;
using SampleApp.Security;
using SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(o => string.Empty));

            CreateMap<UserViewModel, User>();
            CreateMap<RefreshTokenViewModel, RefreshToken>();
            CreateMap<RefreshToken, RefreshTokenViewModel>();
            CreateMap<AccessTokenViewModel, AccessToken>();
            CreateMap<AccessToken, AccessTokenViewModel>();
        }
    }
}
