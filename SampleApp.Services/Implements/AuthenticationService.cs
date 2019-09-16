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
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserReponsitory _userReponsitory;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        public AuthenticationService(IUserReponsitory userReponsitory, IMapper mapper, ITokenHandler tokenHandler)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
        }

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

        public async Task RevokeRefreshToken(string token)
        {
            await _tokenHandler.RevokeRefreshToken(token);
        }

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
