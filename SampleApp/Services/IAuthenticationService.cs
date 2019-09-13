using SampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IAuthenticationService
    {
        Task<AccessTokenViewModel> Authentication(LoginViewModel model);
        Task<TokenResponseModel> RefreshToken(string refreshToken);
        Task RevokeRefreshToken(string token);
    }
}
