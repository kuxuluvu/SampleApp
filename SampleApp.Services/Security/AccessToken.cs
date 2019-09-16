// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="AccessToken.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Infrastructure.Models;
using System;

namespace SampleApp.Services.Security
{
    /// <summary>
    /// Class AccessToken.
    /// </summary>
    public class AccessToken 
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>The expiration.</value>
        public long Expiration { get; set; }
        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <value>The refresh token.</value>
        public RefreshToken RefreshToken { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessToken"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="expiration">The expiration.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <exception cref="ArgumentException">Specify a valid refresh token.</exception>
        public AccessToken(string token, long expiration, RefreshToken refreshToken)
        {
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
        }
    }

}
