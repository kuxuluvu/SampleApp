using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleApp.Helper;
using SampleApp.Models;
using SampleApp.Reponsitory;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Security
{
    public class TokenHandler : ITokenHandler
    {
        private SigningConfigurations _signingConfigurations;
        private readonly IRefreshTokenReponsitory _refreshTokenReponsitory;
        public TokenHandler(IRefreshTokenReponsitory refreshTokenReponsitory,
                SigningConfigurations signingConfigurations)
        {
            _refreshTokenReponsitory = refreshTokenReponsitory;
            _signingConfigurations = signingConfigurations;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var refreshToken = BuildRefreshToken(user);
            var accessToken = BuildAccessToken(user, refreshToken);

            //save token
            var existRefreshToken = await _refreshTokenReponsitory.FirstOrDefaultAsync(x => x.Token == refreshToken.Token);
            if (existRefreshToken != null)
            {
                throw new DuplicateWaitObjectException("Duplicate token");
            }

            refreshToken.Id = Guid.NewGuid();
            refreshToken.UserId = user.Id;
            await _refreshTokenReponsitory.AddAsync(refreshToken);

            return accessToken;
        }

        public async Task<RefreshToken> TakeRefreshToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var refreshToken = await _refreshTokenReponsitory.FirstOrDefaultAsync(x => x.Token == token);
            if (refreshToken != null)
            {
                await _refreshTokenReponsitory.DeleteAsync(refreshToken);
            }

            return refreshToken;
        }

        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(20);

            var securityToken = new JwtSecurityToken
            (
                issuer: "Sample",
                audience: "Sample",
                claims: GetClaims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        private RefreshToken BuildRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = SampleHelper.HashString(Guid.NewGuid().ToString()),
                Expiration = DateTime.UtcNow.AddMinutes(20).Ticks
            };

            return refreshToken;
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
           };

            return claims;
        }

        public async Task RevokeRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenReponsitory.FirstOrDefaultAsync(x => x.Token == token);
            if (refreshToken != null)
            {
                await _refreshTokenReponsitory.DeleteAsync(refreshToken);
            }
        }
    }
}
