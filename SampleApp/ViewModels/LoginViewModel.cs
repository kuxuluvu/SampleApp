// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-12-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-12-2019
// ***********************************************************************
// <copyright file="LoginViewModel.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    /// <summary>
    /// Class LoginViewModel.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Class ResponseModel.
    /// </summary>
    public class  ResponseModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is succes.
        /// </summary>
        /// <value><c>true</c> if this instance is succes; otherwise, <c>false</c>.</value>
        public bool IsSucces { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }
    }
}
