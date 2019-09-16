// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="IUserService.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Http;
using SampleApp.Infrastructure.Models;
using SampleApp.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    /// <summary>
    /// Interface IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ListResponseViewModel&gt;.</returns>
        Task<ListResponseViewModel> GetUsers(UserParameterDto model);
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;UserDto&gt;.</returns>
        Task<UserDto> Register(UserDto user);
        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> Update(UserDto user);
        /// <summary>
        /// Deletes the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> Delete(string userName);
        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> Delete(Guid userId);
        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        Task<User> GetUserById(Guid userId);
        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Task&lt;ResponseUploadImageDto&gt;.</returns>
        Task<ResponseUploadImageDto> UploadImage(IFormFile file);
        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task.</returns>
        Task Update(User user);
    }
}
