using Microsoft.AspNetCore.Http;
using SampleApp.Models;
using SampleApp.ViewModels;
using System;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IUserService
    {
        Task<ListResponseViewModel> GetUsers(UserParameterModel model);
        Task<UserViewModel> Register(UserViewModel user);
        Task<bool> Update(UserViewModel user);
        Task<bool> Delete(string userName);
        Task<bool> Delete(Guid userId);
        Task<User> GetUserById(Guid userId);
        Task<ResponseUploadImageModel> UploadImage(IFormFile file);
        Task Update(User user);
    }
}
