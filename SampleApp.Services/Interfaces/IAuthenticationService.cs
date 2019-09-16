using SampleApp.Services.DTOs;
using System.Threading.Tasks;

namespace SampleApp.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AccessTokenDto> Authentication(LoginDto model);
        Task<TokenResponseDto> RefreshToken(string refreshToken);
        Task RevokeRefreshToken(string token);
    }
}
