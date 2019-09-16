// ***********************************************************************
// Assembly         : SampleApp.Reponsitory
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="RefreshTokenReponsitory.cs" company="SampleApp.Reponsitory">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Infrastructure;
using SampleApp.Infrastructure.Models;
using SampleApp.Reponsitory.Intefaces;

namespace SampleApp.Reponsitory
{
    /// <summary>
    /// Class RefreshTokenReponsitory.
    /// Implements the <see cref="SampleApp.Reponsitory.BaseReponsitory{SampleApp.Infrastructure.Models.RefreshToken}" />
    /// Implements the <see cref="SampleApp.Reponsitory.Intefaces.IRefreshTokenReponsitory" />
    /// </summary>
    /// <seealso cref="SampleApp.Reponsitory.BaseReponsitory{SampleApp.Infrastructure.Models.RefreshToken}" />
    /// <seealso cref="SampleApp.Reponsitory.Intefaces.IRefreshTokenReponsitory" />
    public class RefreshTokenReponsitory : BaseReponsitory<RefreshToken>, IRefreshTokenReponsitory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenReponsitory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RefreshTokenReponsitory(SampleContext context) : base(context)
        {
        }
    }
}
