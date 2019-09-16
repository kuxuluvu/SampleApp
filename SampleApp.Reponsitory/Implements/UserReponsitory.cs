// ***********************************************************************
// Assembly         : SampleApp.Reponsitory
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UserReponsitory.cs" company="SampleApp.Reponsitory">
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
    /// Class UserReponsitory.
    /// Implements the <see cref="SampleApp.Reponsitory.BaseReponsitory{SampleApp.Infrastructure.Models.User}" />
    /// Implements the <see cref="SampleApp.Reponsitory.Intefaces.IUserReponsitory" />
    /// </summary>
    /// <seealso cref="SampleApp.Reponsitory.BaseReponsitory{SampleApp.Infrastructure.Models.User}" />
    /// <seealso cref="SampleApp.Reponsitory.Intefaces.IUserReponsitory" />
    public class UserReponsitory : BaseReponsitory<User>, IUserReponsitory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserReponsitory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserReponsitory(SampleContext context) : base(context)
        {
        }
    }
}
