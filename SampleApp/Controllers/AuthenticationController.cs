﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Services;
using SampleApp.Services.Interfaces;
using SampleApp.ViewModels;
using System.Threading.Tasks;
using SampleApp.Services.DTOs;
namespace SampleApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
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
                var modelMapper = _mapper.Map<LoginViewModel, LoginDto>(viewModel);
                var result = await _authenticationService.Authentication(modelMapper);

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