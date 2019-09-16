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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReponsitory _userReponsitory;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UserService(IUserReponsitory userReponsitory, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

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
        /// <param name="model"></param>
        /// <returns></returns>
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

        public async Task<bool> Delete(string userName)
        {
            var existUser = await _userReponsitory.SingleOrDefaultAsync(x => x.Username == userName && !x.IsDeleted);

            if (existUser == null) return false;

            existUser.IsDeleted = true;
            await _userReponsitory.UpdateAsync(existUser);

            return true;
        }

        public async Task<bool> Delete(Guid userId)
        {
            var existUser = await GetUserById(userId);

            if (existUser == null) return false;

            existUser.IsDeleted = true;
            await _userReponsitory.UpdateAsync(existUser);

            return true;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return
                await _userReponsitory.SingleOrDefaultAsync(x => x.Id == userId && !x.IsDeleted);
        }

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

        public async Task Update(User user)
        {
            await _userReponsitory.UpdateAsync(user);
        }
    }
}
