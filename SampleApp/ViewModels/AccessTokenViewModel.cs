// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="AccessTokenViewModel.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace SampleApp.ViewModels
{
    /// <summary>
    /// Class AccessTokenViewModel.
    /// </summary>
    public class AccessTokenViewModel
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
        public RefreshTokenViewModel RefreshToken { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenViewModel"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="expiration">The expiration.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <exception cref="ArgumentException">Specify a valid refresh token.</exception>
        public AccessTokenViewModel(string token, long expiration, RefreshTokenViewModel refreshToken)
        {
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
        }
    }
}
