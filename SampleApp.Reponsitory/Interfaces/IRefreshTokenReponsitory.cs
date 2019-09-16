// ***********************************************************************
// Assembly         : SampleApp.Reponsitory
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="IRefreshTokenReponsitory.cs" company="SampleApp.Reponsitory">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Infrastructure.Models;

namespace SampleApp.Reponsitory.Intefaces
{
    /// <summary>
    /// Interface IRefreshTokenReponsitory
    /// Implements the <see cref="SampleApp.Reponsitory.Intefaces.IBaseReponsitory{SampleApp.Infrastructure.Models.RefreshToken}" />
    /// </summary>
    /// <seealso cref="SampleApp.Reponsitory.Intefaces.IBaseReponsitory{SampleApp.Infrastructure.Models.RefreshToken}" />
    public interface IRefreshTokenReponsitory : IBaseReponsitory<RefreshToken>
    {
    }
}
