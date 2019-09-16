// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="MappingProfile.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using SampleApp.Infrastructure.Models;
using SampleApp.Services.DTOs;
using SampleApp.Services.Security;
using SampleApp.ViewModels;

namespace SampleApp.Configs
{
    /// <summary>
    /// Class MappingProfile.
    /// Implements the <see cref="AutoMapper.Profile" />
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
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
