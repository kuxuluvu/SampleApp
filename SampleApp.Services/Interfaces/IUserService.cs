using Microsoft.AspNetCore.Http;
using SampleApp.Infrastructure.Models;
using SampleApp.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IUserService
    {
        Task<ListResponseViewModel> GetUsers(UserParameterDto model);
        Task<UserDto> Register(UserDto user);
        Task<bool> Update(UserDto user);
        Task<bool> Delete(string userName);
        Task<bool> Delete(Guid userId);
        Task<User> GetUserById(Guid userId);
        Task<ResponseUploadImageDto> UploadImage(IFormFile file);
        Task Update(User user);
    }
}
