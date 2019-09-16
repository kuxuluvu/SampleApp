// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="ITokenHandler.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Infrastructure.Models;
using SampleApp.Services.Security;
using System.Threading.Tasks;

namespace SampleApp.Security
{
    /// <summary>
    /// Interface ITokenHandler
    /// </summary>
    public interface ITokenHandler
    {
        /// <summary>
        /// Creates the access token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;AccessToken&gt;.</returns>
        Task<AccessToken> CreateAccessToken(User user);
        /// <summary>
        /// Takes the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;RefreshToken&gt;.</returns>
        Task<RefreshToken> TakeRefreshToken(string token);
        /// <summary>
        /// Revokes the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task.</returns>
        Task RevokeRefreshToken(string token);
    }
}
