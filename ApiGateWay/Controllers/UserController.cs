using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace ApiGateWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("GetAllUser attempt started");
                try
                {
                    _logger.LogInformation($"Calling GetAllUser service");
                    var result = await _service.GetAllUser();
                    if (result._status == 200)
                    {
                        _logger.LogInformation($"GetAllUser successful");
                        return StatusCode(200, result);
                    }
                    else
                    {
                        _logger.LogWarning($"GetAllUser failed with status {result._status}");
                        return StatusCode(result._status, result._message);
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred during GetAllUser");
                    Console.WriteLine("Something went wrong GetAllUser" + ex.Message);
                    return StatusCode(400, ex.Message);
                }
            }
        }

        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(string searchRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("GetUser attempt started");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("GetUser failed due to invalid data");
                    return StatusCode(400, "Invalid data");
                }
                try
                {
                    _logger.LogInformation($"Calling GetUser service for user {searchRequest}");
                    var result = await _service.GetUser(searchRequest);
                    if (result._status == 200)
                    {
                        _logger.LogInformation($"GetUser successful for user {searchRequest}");
                        return StatusCode(200, result);
                    }
                    else
                    {
                        _logger.LogWarning($"GetUser failed for user {searchRequest} with status {result._status}");
                        return StatusCode(result._status, result._message);
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred during GetUser for user {searchRequest}");
                    Console.WriteLine("Something went wrong GetUser" + ex.Message);
                    return StatusCode(400, ex.Message);
                }
            }
        }

        [HttpPost("GetUserByTag")]
        public async Task<IActionResult> GetUserByTag(string searchRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("GetUserByTag attempt started");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("GetUserByTag failed due to invalid data");
                    return StatusCode(400, "Invalid data");
                }
                try
                {
                    _logger.LogInformation($"Calling GetUserByTag service for user {searchRequest}");
                    var result = await _service.GetUserByTag(searchRequest);
                    if (result._status == 200)
                    {
                        _logger.LogInformation($"GetUserByTag successful for user {searchRequest}");
                        return StatusCode(200, result);
                    }
                    else
                    {
                        _logger.LogWarning($"GetUserByTag failed for user {searchRequest} with status {result._status}");
                        return StatusCode(result._status, result._message);
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred during GetUserByTag for user {searchRequest}");
                    Console.WriteLine("Something went wrong GetUserByTag" + ex.Message);
                    return StatusCode(400, ex.Message);
                }
            }
        }
    }
}