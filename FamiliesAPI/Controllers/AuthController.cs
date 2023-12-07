using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FamiliesAPI.Services.Interface;
using FamiliesAPI.Helpers;
using FamiliesAPI.Entities.DTOs;
using System.Text.Json;

namespace FamiliesAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly ILoggingService _loggingService;

        public AuthController(IAuthService authService, IConfiguration configuration, ILoggingService loggingService)
        {
            _authService = authService;
            _configuration = configuration;
            _loggingService = loggingService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(loginDTO==null)
                return BadRequest("User model is null");

            string json = JsonSerializer.Serialize(loginDTO);

            try
            {
                var result = await _authService.Authenticate(loginDTO.UserName, loginDTO.Password);

                if (result.Success)
                {
                    var token = JwtHelpers.GetToken(result.Result.Username, _configuration["Jwt:SecretKey"], _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"], _configuration["Jwt:ExpirationHours"]);
                    var response = new { Token = token };
                    await _loggingService.Save("add", loginDTO.UserName, "Login", json, JsonSerializer.Serialize(response), true);
                    return Ok(response);
                }
                await _loggingService.Save("add", loginDTO.UserName, "Login", json, result.Message, false, result.Exception);
                return Unauthorized(new { Message = "Incorrect username or password." });
            }
            catch(Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", loginDTO.UserName, "Login", json, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        //[HttpPost("validate")]
        //public async Task<IActionResult> validateToken([FromBody] string token)
        //{
        //    await Task.Delay(100);
        //    var res = JwtHelpers.ValidateToken(
        //        token, 
        //        _configuration["Jwt:SecretKey"],
        //        _configuration["Jwt:Issuer"],
        //        _configuration["Jwt:Audience"]);
        //    if (res.Identity.IsAuthenticated)
        //        return Ok();
        //    else return Unauthorized(new { Message = "Usuario sin acceso." });
        //}
    }
}
