using AutoMapper;
using SampleApp.Infrastructure.Models;
using SampleApp.Services.DTOs;
using SampleApp.Services.Security;
using SampleApp.ViewModels;

namespace SampleApp.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserViewModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(o => string.Empty));
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(o => string.Empty));
            CreateMap<UserDto, User>();
            CreateMap<UserViewModel, UserDto>();

            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<RefreshTokenViewModel, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshTokenViewModel>();
            CreateMap<AccessTokenViewModel, AccessTokenDto>();
            CreateMap<AccessTokenDto, AccessTokenViewModel>();
      
            CreateMap<UserParameterModel, UserParameterDto>();
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<AccessToken, AccessTokenDto>();
        }
    }
}
