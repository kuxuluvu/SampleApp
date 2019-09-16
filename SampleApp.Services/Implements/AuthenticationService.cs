// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="AuthenticationService.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using SampleApp.Infrastructure.Helper;
using SampleApp.Reponsitory.Intefaces;
using SampleApp.Security;
using SampleApp.Services.DTOs;
using SampleApp.Services.Interfaces;
using SampleApp.Services.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Services.Implements
{
    /// <summary>
    /// Class AuthenticationService.
    /// Implements the <see cref="SampleApp.Services.Interfaces.IAuthenticationService" />
    /// </summary>
    /// <seealso cref="SampleApp.Services.Interfaces.IAuthenticationService" />
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The user reponsitory
        /// </summary>
        private readonly IUserReponsitory _userReponsitory;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The token handler
        /// </summary>
        private readonly ITokenHandler _tokenHandler;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="userReponsitory">The user reponsitory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="tokenHandler">The token handler.</param>
        public AuthenticationService(IUserReponsitory userReponsitory, IMapper mapper, ITokenHandler tokenHandler)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
        }

        /// <summary>
        /// Authentications the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;AccessTokenDto&gt;.</returns>
        public async Task<AccessTokenDto> Authentication(LoginDto model)
        {
            var user = await _userReponsitory.FirstOrDefaultAsync(x => x.Username == model.Username && !x.IsDeleted && x.IsActive);

            if (user != null)
            {
                var currentHash = SampleHelper.GenerateSaltedHash(Encoding.ASCII.GetBytes(model.Password),
                                                  Convert.FromBase64String(user.Salt));

                if (SampleHelper.CompareByteArrays(Convert.FromBase64String(currentHash), Convert.FromBase64String(user.Password)))
                {
                    var token = await _tokenHandler.CreateAccessToken(user);

                    return _mapper.Map<AccessToken, AccessTokenDto>(token);
                }
            }

            return null;
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>Task&lt;TokenResponseDto&gt;.</returns>
        public async Task<TokenResponseDto> RefreshToken(string refreshToken)
        {
            var response = new TokenResponseDto()
            {
                IsSucces = false,
                ErrorMessage = "Invalid refresh token",
            };

            var token = await _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
                return response;
            }

            if (token.IsExpired())
            {
                response.ErrorMessage = "Expired refresh token.";
                return response;
            }
            var user = await _userReponsitory.FirstOrDefaultAsync(x => x.Id == token.UserId);
            var accessToken = await _tokenHandler.CreateAccessToken(user);

            response.IsSucces = true;
            response.ErrorMessage = null;
            response.AccessToken = _mapper.Map<AccessToken, AccessTokenDto>(accessToken);

            return response;
        }

        /// <summary>
        /// Revokes the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        public async Task RevokeRefreshToken(string token)
        {
            await _tokenHandler.RevokeRefreshToken(token);
        }

        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>IEnumerable&lt;Claim&gt;.</returns>
        private IEnumerable<Claim> GetClaims(UserDto user)
        {
            var claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
           };

            return claims;
        }
    }
}
