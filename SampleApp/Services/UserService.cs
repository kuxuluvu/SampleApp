using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApp.Helper;
using SampleApp.Models;
using SampleApp.Reponsitory;
using SampleApp.Security;
using SampleApp.ViewModels;

namespace SampleApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReponsitory _userReponsitory;
        private readonly IMapper _mapper;
        public UserService(IUserReponsitory userReponsitory, IMapper mapper)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
        }

        public async Task<ListResponseViewModel> GetUsers(UserParameterModel model)
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
                IsActive = x.IsActive
            }).ToListAsync();

            return result;
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserViewModel> Register(UserViewModel user)
        {
            var exist = await _userReponsitory.FirstOrDefaultAsync(x => x.Username == user.Username && !x.IsDeleted);

            if (exist != null) return null;

            var newUser = _mapper.Map<UserViewModel, User>(user);

            var salt = SampleHelper.CreateSalt(16);
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

        public async Task<bool> Update(UserViewModel user)
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
    }
}
