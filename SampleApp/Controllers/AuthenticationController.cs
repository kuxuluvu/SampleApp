using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Services;
using SampleApp.ViewModels;

namespace SampleApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
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

                var result = await _authenticationService.Authentication(viewModel);

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
            var result = await _authenticationService.RefreshToken(refreshToken);

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
        /// Revoke refreshToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [Route("revoke")]
        [HttpPost]
        public async Task<IActionResult> RevokeToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(ModelState);
            }

            await _authenticationService.RevokeRefreshToken(refreshToken);
            return NoContent();
        }
    }
}