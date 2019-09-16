using SampleApp.Infrastructure.Models;
using SampleApp.Services.Security;
using System.Threading.Tasks;

namespace SampleApp.Security
{
    public interface ITokenHandler
    {
        Task<AccessToken> CreateAccessToken(User user);
        Task<RefreshToken> TakeRefreshToken(string token);
        Task RevokeRefreshToken(string token);
    }
}
