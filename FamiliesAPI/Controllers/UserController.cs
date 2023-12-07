using AutoMapper;
using FamiliesAPI.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FamiliesAPI.Helpers;
using FamiliesAPI.Services.Interface;
using System.Text.Json;

namespace FamiliesAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoggingService _loggingService;

        public UserController(IUserService userService, ILoggingService loggingService)
        {
            _userService = userService;
            _loggingService = loggingService;
        }

        [AllowAnonymous]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("User model is null");

            string json = JsonSerializer.Serialize(userDTO);
            string username = GetUserAuth();
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(errors);
                }
                var res = await _userService.Add(userDTO);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "UserController: Add", json, JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                await _loggingService.Save("add", username, "UserController: Add", json, res.Message, false);
                return StatusCode(500, "Error saving User in database");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "UserController: Add", json, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string username = GetUserAuth();
            try
            {
                var res = await _userService.GetAll();
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "UserController: GetAll", null, null, true);
                    return Ok(res.Result);
                }
                await _loggingService.Save("add", username, "UserController: GetAll", null, res.Message, false);
                return StatusCode(404, "Users not found");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "UserController: GetAll", null, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(long id)
        {
            if (id == 0)
                return BadRequest("Id is null");

            string username = GetUserAuth();
            try
            {
                var res = await _userService.Get(id);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "UserController: GetbyId", id.ToString(), JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                await _loggingService.Save("add", username, "UserController: GetbyId", id.ToString(), res.Message, false);
                return StatusCode(404, "User not found");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "UserController: GetbyId", id.ToString(), null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
                return BadRequest("User model is null");

            if (id != userDTO.UserId)
                return BadRequest("Id parameter is not equal to the Id parameter in the model.");

            string json = JsonSerializer.Serialize(userDTO);
            string username = GetUserAuth();
            try
            {

                var res = await _userService.Update(id, userDTO);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "UserController: Update", json, JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                if (res.StatusCode == 400)
                {
                    await _loggingService.Save("add", username, "UserController: Update", json, res.Message, false);
                    return StatusCode(400, res.Message);
                }
                await _loggingService.Save("add", username, "UserController: Update", json, res.Message, false);
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", userDTO.Username, "UserController: Update", json, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            string username = GetUserAuth();
            try
            {
                if (id == 0)
                    return BadRequest("Id is null");

                var res = await _userService.Delete(id);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "UserController: Delete", id.ToString(), JsonSerializer.Serialize(res.Result), true);
                    return Ok();
                }
                await _loggingService.Save("add", username, "UserController: Delete", id.ToString(), res.Message, false);
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "UserController: Delete", id.ToString(), null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        private string GetUserAuth()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            return JwtHelpers.GetUserByToken(token);
        }
    }
}
