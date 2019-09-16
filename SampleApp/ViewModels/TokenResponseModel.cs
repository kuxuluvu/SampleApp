// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="TokenResponseModel.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SampleApp.ViewModels
{
    /// <summary>
    /// Class TokenResponseModel.
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is succes.
        /// </summary>
        /// <value><c>true</c> if this instance is succes; otherwise, <c>false</c>.</value>
        public bool IsSucces { get; set; }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public AccessTokenViewModel AccessToken { get; set; }
    }
}
