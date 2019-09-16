// ***********************************************************************
// Assembly         : SampleApp.Reponsitory
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="IUserReponsitory.cs" company="SampleApp.Reponsitory">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SampleApp.Infrastructure.Models;

namespace SampleApp.Reponsitory.Intefaces
{
    /// <summary>
    /// Interface IUserReponsitory
    /// Implements the <see cref="SampleApp.Reponsitory.Intefaces.IBaseReponsitory{SampleApp.Infrastructure.Models.User}" />
    /// </summary>
    /// <seealso cref="SampleApp.Reponsitory.Intefaces.IBaseReponsitory{SampleApp.Infrastructure.Models.User}" />
    public interface IUserReponsitory : IBaseReponsitory<User>
    {
    }
}
