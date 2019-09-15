﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Models;
using SampleApp.Services;
using SampleApp.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SampleApp.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userServices;

        public UserController(IMapper mapper, IUserService userServices)
        {
            _mapper = mapper;
            _userServices = userServices;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// Update user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userServices.Update(model);
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
        /// <param name="userName"></param>
        /// <returns></returns>
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUser([FromBody] UserParameterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userServices.GetUsers(model);

            return Ok(result);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <param name="model"></param>
        /// <returns></returns>
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

                var result = await _userServices.UploadImage(model.File);

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
