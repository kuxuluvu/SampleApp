// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="IAuthenticationService.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Services.DTOs;
using System.Threading.Tasks;

namespace SampleApp.Services.Interfaces
{
    /// <summary>
    /// Interface IAuthenticationService
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authentications the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;AccessTokenDto&gt;.</returns>
        Task<AccessTokenDto> Authentication(LoginDto model);
        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>Task&lt;TokenResponseDto&gt;.</returns>
        Task<TokenResponseDto> RefreshToken(string refreshToken);
        /// <summary>
        /// Revokes the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        Task RevokeRefreshToken(string token);
    }
}
