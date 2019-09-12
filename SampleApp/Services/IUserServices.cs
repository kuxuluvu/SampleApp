using SampleApp.Models;
using SampleApp.Security;
using SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IUserServices
    {
        Task<IEnumerable<UserViewModel>> GetUsers();
        Task<UserViewModel> Register(UserViewModel user);
        Task<AccessToken> Authentication(LoginViewModel model);
        Task<TokenResponseModel> RefreshToken(string refreshToken);
        Task RevokeRefreshToken(string token);
    }
}
