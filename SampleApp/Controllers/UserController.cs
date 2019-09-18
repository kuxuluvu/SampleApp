// ***********************************************************************
// Assembly         : SampleApp
// Author           : duc.nguyen
// Created          : 09-16-2019
//
// Last Modified By : duc.nguyen
// Last Modified On : 09-16-2019
// ***********************************************************************
// <copyright file="UserController.cs" company="SampleApp">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Infrastructure.Models;
using SampleApp.Services;
using SampleApp.Services.DTOs;
using SampleApp.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SampleApp.Controllers
{
    /// <summary>
    /// Class UserController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize]
    public class UserController : Controller
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// The user services
        /// </summary>
        private readonly IUserService _userServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userServices">The user services.</param>
        public UserController(IMapper mapper, IUserService userServices)
        {
            _mapper = mapper;
            _userServices = userServices;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = new ResponseModel()
                {
                    IsSucces = false,
                    Message = "Register failed"
                };
                var modelMapper = _mapper.Map<UserViewModel, UserDto>(model);
                var result = await _userServices.Register(modelMapper);

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
        /// Update user
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelMapper = _mapper.Map<UserViewModel, UserDto>(model);
                var result = await _userServices.Update(modelMapper);
                if (!result)
                {
                    return BadRequest("Update user failed");
                }

                return Ok("Update successfully");
            }

            return BadRequest("Invalid model");
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid userId)
        {
            if (ModelState.IsValid && userId != Guid.Empty)
            {
                var result = await _userServices.Delete(userId);
                if (!result)
                {
                    return BadRequest("Delete user failed");
                }

                return Ok("Delete user successfully");
            }

            return BadRequest("Invalid model");
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUser([FromBody] UserParameterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var modelMapper = _mapper.Map<UserParameterModel, UserParameterDto>(model);
            var result = await _userServices.GetUsers(modelMapper);

            return Ok(result);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [Route("Get/{userId}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            if (ModelState.IsValid && userId != Guid.Empty)
            {
                var result = await _userServices.GetUserById(userId);
                if (result == null)
                {
                    return BadRequest(ModelState);
                }

                return Ok(_mapper.Map<User, UserViewModel>(result));
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Upload image for User
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UpLoadImage(UploadImageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }
            try
            {
                var user = await _userServices.GetUserById(model.UserId);
                if (user == null)
                {
                    return BadRequest("Upload image failed");
                }

                var result = await _userServices.UploadPhoto(model.File);

                if (result != null && result.Status == HttpStatusCode.OK)
                {
                    user.ImageUrl = result.Data.Link;

                    await _userServices.Update(user);
                }

                return Ok(result.Data.Link);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
