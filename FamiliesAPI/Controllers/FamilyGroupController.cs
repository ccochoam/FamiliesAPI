using FamiliesAPI.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FamiliesAPI.Helpers;
using FamiliesAPI.Services.Implementation;
using FamiliesAPI.Services.Interface;
using System.Text.Json;
using System.Xml.Linq;

namespace FamiliesAPI.Controllers
{
    [ApiController]
    [Route("api/familygroup")]
    [Authorize]
    public class FamilyGroupController : Controller
    {
        private readonly IFamilyGroupService _familyGroupService;
        private readonly ILoggingService _loggingService;
        public FamilyGroupController(IFamilyGroupService familyGroupService, ILoggingService loggingService)
        {
            _familyGroupService = familyGroupService;
            _loggingService = loggingService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            if (name == null)
                return BadRequest("name is null");

            string username = GetUserAuth();
            try
            {

                var res = await _familyGroupService.Add(name);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "FamilyGroupController: Add", name, JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                await _loggingService.Save("add", username, "FamilyGroupController: Add", name, res.Message, false);
                return StatusCode(500, "Error saving FamilyGroup in database");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "FamilyGroupController: Add", name, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string username = GetUserAuth();
            try
            {
                var res = await _familyGroupService.GetAll();
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "FamilyGroupController: GetAll", null, JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                await _loggingService.Save("add", username, "FamilyGroupController: GetAll", null, res.Message, false);
                return StatusCode(404, "Users not found");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "FamilyGroupController: GetAll", null, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
                return BadRequest("Id is null");

            string username = GetUserAuth();
            try
            {
                var res = await _familyGroupService.Get(id);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "FamilyGroupController: GetById", id.ToString(), JsonSerializer.Serialize(res.Result), true);
                    return Ok(res);
                }
                await _loggingService.Save("add", username, "FamilyGroupController: GetById", id.ToString(), res.Message, false);
                return StatusCode(404, "User not found");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "FamilyGroupController: GetById", id.ToString(), null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FamilyGroupDto familyGroupDto)
        {
            if (familyGroupDto == null)
                return BadRequest("FamilyGroup model is null");

            if (id != familyGroupDto.FamilyGroupId)
                return BadRequest("Id parameter is not equal to the Id parameter in the model.");

            string json = JsonSerializer.Serialize(familyGroupDto);
            string username = GetUserAuth();
            try
            {
                var res = await _familyGroupService.Update(id, familyGroupDto);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "FamilyGroupController: Update", json, JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                if (res.StatusCode == 400)
                {
                    await _loggingService.Save("add", username, "FamilyGroupController: Update", json, res.Message, false);
                    return StatusCode(400, res.Message);
                }
                await _loggingService.Save("add", username, "FamilyGroupController: Update", json, res.Message, false);
                return BadRequest("Internal Server Error");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "FamilyGroupController: Update", json, null, false, exception);
                return BadRequest("Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest("Id is null");

            string username = GetUserAuth();
            try
            {

                var res = await _familyGroupService.Delete(id);
                if (res.Success)
                {
                    await _loggingService.Save("add", username, "FamilyGroupController: Delete", id.ToString(), JsonSerializer.Serialize(res.Result), true);
                    return Ok(res.Result);
                }
                await _loggingService.Save("add", username, "FamilyGroupController: Delete", id.ToString(), res.Message, false);
                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                var exception = ex.InnerException?.ToString() ?? ex.Message;
                await _loggingService.Save("add", username, "FamilyGroupController: Delete", id.ToString(), null, false, exception);
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
