// ***********************************************************************
// Assembly         : SampleApp.Services
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UserService.cs" company="SampleApp.Services">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using SampleApp.Infrastructure.Helper;
using SampleApp.Infrastructure.Models;
using SampleApp.Reponsitory.Intefaces;
using SampleApp.Services.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SampleApp.Services
{
    /// <summary>
    /// Class UserService.
    /// Implements the <see cref="SampleApp.Services.IUserService" />
    /// </summary>
    /// <seealso cref="SampleApp.Services.IUserService" />
    public class UserService : IUserService
    {
        /// <summary>
        /// The user reponsitory
        /// </summary>
        private readonly IUserReponsitory _userReponsitory;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The application settings
        /// </summary>
        private readonly AppSettings _appSettings;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userReponsitory">The user reponsitory.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="appSettings">The application settings.</param>
        public UserService(IUserReponsitory userReponsitory, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;ListResponseViewModel&gt;.</returns>
        public async Task<ListResponseViewModel> GetUsers(UserParameterDto model)
        {
            var result = new ListResponseViewModel();

            var users = _userReponsitory.Query(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                users = users.Where(x =>
                    x.Username.Contains(model.Search) ||
                    x.FirstName.Contains(model.Search) ||
                    x.LastName.Contains(model.Search) ||
                    x.Email.Contains(model.Search));
            }

            result.Total = await users.CountAsync();

            switch (model.ColumnSort)
            {
                case "Username":
                    users = model.OrderBy == "asc" ? users.OrderBy(x => x.Username) : users.OrderByDescending(x => x.Username);
                    break;

                case "FullName":
                    users = model.OrderBy == "asc" ? users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                                : users.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
                    break;

                case "Phone":
                    users = model.OrderBy == "asc" ? users.OrderBy(x => x.Phone)
                                                  : users.OrderByDescending(x => x.Phone);
                    break;

                case "DayOfBirth":
                    users = model.OrderBy == "asc" ? users.OrderBy(x => x.DayOfBirth)
                                                  : users.OrderByDescending(x => x.DayOfBirth);
                    break;

                case "Email":
                    users = model.OrderBy == "asc" ? users.OrderBy(x => x.Email)
                                                  : users.OrderByDescending(x => x.Email);
                    break;

                default:
                    users = model.OrderBy == "asc" ? users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                              : users.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
                    break;
            }

            users = users.Skip(model.Page - 1).Take(model.PageSize);

            result.Resources = await users.Select(x => new UserResponseViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username,
                DayOfBirth = x.DayOfBirth,
                Email = x.Email,
                Phone = x.Phone,
                ImageUrl = x.ImageUrl,
                IsActive = x.IsActive
            }).ToListAsync();

            return result;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;UserDto&gt;.</returns>
        public async Task<UserDto> Register(UserDto user)
        {
            var exist = await _userReponsitory.FirstOrDefaultAsync(x => x.Username == user.Username && !x.IsDeleted);

            if (exist != null) return null;

            var newUser = _mapper.Map<UserDto, User>(user);

            var salt = SampleHelper.CreateSalt();
            var hashPassword = SampleHelper.GenerateSaltedHash(Encoding.ASCII.GetBytes(newUser.Password),
                        Convert.FromBase64String(salt));

            newUser.Id = Guid.NewGuid();
            newUser.IsActive = true;
            newUser.IsDeleted = false;
            newUser.Salt = salt;
            newUser.Password = hashPassword;

            await _userReponsitory.AddAsync(newUser);

            return user;
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Update(UserDto user)
        {
            var existUser = await GetUserById(user.Id);

            if (existUser == null) return false;

            existUser.Email = user.Email;
            existUser.DayOfBirth = user.DayOfBirth;
            existUser.FirstName = user.FirstName;
            existUser.LastName = user.LastName;
            existUser.Phone = user.Phone;

            await _userReponsitory.UpdateAsync(existUser);
            return true;
        }

        /// <summary>
        /// Deletes the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Delete(string userName)
        {

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var existUser = await _userReponsitory.SingleOrDefaultAsync(x => x.Username == userName && !x.IsDeleted);

                if (existUser == null) return false;

                existUser.IsDeleted = true;
                await _userReponsitory.UpdateAsync(existUser);

                scope.Complete();
            }

            return true;
        }

        /// <summary>
        /// Deletes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Delete(Guid userId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var existUser = await GetUserById(userId);

                if (existUser == null) return false;

                existUser.IsDeleted = true;
                await _userReponsitory.UpdateAsync(existUser);

                scope.Complete();
            }

            return true;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;User&gt;.</returns>
        public async Task<User> GetUserById(Guid userId)
        {
            return
                await _userReponsitory.SingleOrDefaultAsync(x => x.Id == userId && !x.IsDeleted);
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Task&lt;ResponseUploadImageDto&gt;.</returns>
        public async Task<ResponseUploadImageDto> UploadImage(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                var client = new RestClient(_appSettings.UrlUpload);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Authorization", "Bearer " + _appSettings.TokenUpload);
                request.AddHeader("content-type", "multipart/form-data");
                request.AddParameter("application/octet-stream", stream.ToArray(), ParameterType.RequestBody);

                var response = await client.ExecuteTaskAsync(request);
                var result = JsonConvert.DeserializeObject<ResponseUploadImageDto>(response.Content);

                return result;
            }
        }

        /// <summary>
        /// Uploads the image  - V2
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ResponseUploadImageDto> UploadPhoto(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_appSettings.UrlUpload)
                };

                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("Authorization", "Bearer " + _appSettings.TokenUpload);

                var byteContent = new ByteArrayContent(stream.ToArray());

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                request.Content = byteContent;

                var response = await httpClient.SendAsync(request);

                var stringResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseUploadImageDto>(stringResult);

                return result;
            }
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task.</returns>
        public async Task Update(User user)
        {
            await _userReponsitory.UpdateAsync(user);
        }
    }
}
