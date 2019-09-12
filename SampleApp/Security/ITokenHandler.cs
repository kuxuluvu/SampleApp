using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Security
{
    public interface ITokenHandler
    {
        Task<AccessToken> CreateAccessToken(User user);
        Task<RefreshToken> TakeRefreshToken(string token);
    }
}
