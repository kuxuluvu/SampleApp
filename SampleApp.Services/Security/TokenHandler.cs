// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="TokenHandler.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Infrastructure.Helper;
using SampleApp.Infrastructure.Models;
using SampleApp.Reponsitory.Intefaces;
using SampleApp.Services.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleApp.Security
{
    /// <summary>
    /// Class TokenHandler.
    /// Implements the <see cref="SampleApp.Security.ITokenHandler" />
    /// </summary>
    /// <seealso cref="SampleApp.Security.ITokenHandler" />
    public class TokenHandler : ITokenHandler
    {
        /// <summary>
        /// The signing configurations
        /// </summary>
        private SigningConfigurations _signingConfigurations;
        /// <summary>
        /// The refresh token reponsitory
        /// </summary>
        private readonly IRefreshTokenReponsitory _refreshTokenReponsitory;
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenHandler"/> class.
        /// </summary>
        /// <param name="refreshTokenReponsitory">The refresh token reponsitory.</param>
        /// <param name="signingConfigurations">The signing configurations.</param>
        public TokenHandler(IRefreshTokenReponsitory refreshTokenReponsitory,
                SigningConfigurations signingConfigurations)
        {
            _refreshTokenReponsitory = refreshTokenReponsitory;
            _signingConfigurations = signingConfigurations;
        }

        /// <summary>
        /// Creates the access token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;AccessToken&gt;.</returns>
        /// <exception cref="DuplicateWaitObjectException">Duplicate token</exception>
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

        /// <summary>
        /// Takes the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;RefreshToken&gt;.</returns>
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

        /// <summary>
        /// Builds the access token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>AccessToken.</returns>
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

        /// <summary>
        /// Builds the refresh token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>RefreshToken.</returns>
        private RefreshToken BuildRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = SampleHelper.HashString(Guid.NewGuid().ToString()),
                Expiration = DateTime.UtcNow.AddMinutes(20).Ticks
            };

            return refreshToken;
        }

        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>IEnumerable&lt;Claim&gt;.</returns>
        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
           };

            return claims;
        }

        /// <summary>
        /// Revokes the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
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
