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
    public class UserServices : IUserServices
    {
        private readonly IUserReponsitory _userReponsitory;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _tokenHandler;
        public UserServices(IUserReponsitory userReponsitory, IMapper mapper, ITokenHandler tokenHandler)
        {
            _userReponsitory = userReponsitory;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var result = await _userReponsitory.GetAll()
                     .Where(x => x.IsDeleted != false)
                     .Select(x => new UserViewModel()
                     {
                         Username = x.Username,
                         Password = string.Empty,
                         FirstName = x.FirstName,
                         LastName = x.LastName,
                         DayOfBirth = x.DayOfBirth,
                         Email = x.Email,
                         Phone = x.Phone
                     })
                     .ToListAsync();

            return result;
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AccessToken> Authentication(LoginViewModel model)
        {
            var user = await _userReponsitory.FirstOrDefaultAsync(x => x.Username == model.Username && !x.IsDeleted && x.IsActive);

            if (user != null)
            {
                var currentHash = SampleHelper.GenerateSaltedHash(Encoding.ASCII.GetBytes(model.Password),
                                                  Convert.FromBase64String(user.Salt));

                if (SampleHelper.CompareByteArrays(Convert.FromBase64String(currentHash), Convert.FromBase64String(user.Password)))
                {
                    var token = await _tokenHandler.CreateAccessToken(user);

                    return token;
                }
            }

            return null;
        }

        public async Task<TokenResponseModel> RefreshToken(string refreshToken)
        {
            var response = new TokenResponseModel()
            {
                IsSucces = false,
                ErrorMessage = "Invalid refresh token",
            };

            var token = await _tokenHandler.TakeRefreshToken(refreshToken);

            if(token == null)
            {
                return response;
            }

            if (token.IsExpired())
            {
                response.ErrorMessage = "Expired refresh token.";
                return response;
            }

            //var user = await _userReponsitory.FirstOrDefaultAsync(x => x.Username == userName &&
            //                        x.IsActive && !x.IsDeleted);
            //if(user == null)
            //{
            //    response.ErrorMessage = "Invalid refresh token";
            //    return response;
            //}

            var accessToken = await _tokenHandler.CreateAccessToken(token.User);

            response.IsSucces = true;
            response.ErrorMessage = null;
            response.AccessToken = accessToken;

            return response;
        }


        private IEnumerable<Claim> GetClaims(UserViewModel user)
        {
            var claims = new List<Claim>
           {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username)
           };


            return claims;
        }

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

            await _userReponsitory.Add(newUser);

            return user;
        }
    }
}
