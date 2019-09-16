// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-12-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-12-2019
// ***********************************************************************
// <copyright file="RefreshTokenViewModel.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    /// <summary>
    /// Class RefreshTokenViewModel.
    /// </summary>
    public class RefreshTokenViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }
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
    }
}
