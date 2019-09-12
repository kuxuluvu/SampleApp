using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Services;
using SampleApp.ViewModels;

namespace SampleApp.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = new ResponseModel()
                {
                    IsSucces = false,
                    Message = "Register failed"
                };

                var result = await _userServices.Register(model);

                if (result == null)
                {
                    response.Message = "Username existed";
                }
                else
                {
                    response.IsSucces = true;
                    response.Message = "Register succesfully";
                }

                return Ok(response);
            }

            return BadRequest("Invalid model");
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var response = new ResponseModel()
                {
                    IsSucces = false,
                    Message = "Login failed"
                };

                var result = await _userServices.Authentication(viewModel);

                if (result != null)
                {
                    response.IsSucces = true;
                    response.Data = result;
                    response.Message = "Login succesfully";
                }

                return Ok(response);
            }

            return BadRequest("Invalid view model");
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [Route("refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(ModelState);
            }
            var result = await _userServices.RefreshToken(refreshToken);

            if (!result.IsSucces)
            {
                return BadRequest(result.ErrorMessage);
            }

            var response = new ResponseModel()
            {
                IsSucces = true,
                Message = "Refresh token successfully",
                Data = result.AccessToken
            };

            return Ok(response);
        }
        /// <summary>
        /// Revoke Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("revoke")]
        [HttpPost]
        public async Task<IActionResult> RevokeToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(ModelState);
            }

            await _userServices.RevokeRefreshToken(token);
            return NoContent();
        }
    }
}
