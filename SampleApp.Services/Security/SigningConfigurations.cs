// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-12-2019
// ***********************************************************************
// <copyright file="SigningConfigurations.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SampleApp.Security
{
    /// <summary>
    /// Class SigningConfigurations.
    /// </summary>
    public class SigningConfigurations
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public SecurityKey Key { get; }
        /// <summary>
        /// Gets the signing credentials.
        /// </summary>
        /// <value>The signing credentials.</value>
        public SigningCredentials SigningCredentials { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SigningConfigurations"/> class.
        /// </summary>
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
