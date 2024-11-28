using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceApi.Service;
using Microsoft.AspNetCore.Mvc;
using UserServiceApi.Request_Responce;
using Microsoft.AspNetCore.Authorization;
using Serilog.Context;

namespace UserServiceApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserServiceController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserServiceController> _logger;


        public UserServiceController(IUserService service, ILogger<UserServiceController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("GetAllUser")]
        public async Task<GeneralResponse> GetAllUser()
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("GetAllUser attempt started");
                try
                {
                    var result = await _service.GetAllUser();
                    if (result._status == 200)
                    {
                        _logger.LogInformation($"GetAllUser successful");
                        return new GeneralResponse(200, result._message, result._users);
                    }
                    else
                    {
                        _logger.LogWarning($"GetAllUser failed with status {result._status}");
                        return new GeneralResponse(404, "there is no user in database");
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred during GetAllUser");
                    return new GeneralResponse(400, ex.Message);
                }
            }
        }

        [HttpPost("GetUser")]
        public async Task<GeneralResponse> GetUser([FromBody] UserRequest searchRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("GetUser attempt started");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("GetUser failed due to invalid data");
                    return new GeneralResponse(400, "Invalid data");
                }
                try
                {
                    var result = await _service.GetUser(searchRequest);
                    if (result._status == 200)
                    {
                        _logger.LogInformation($"GetUser successful for user {searchRequest.UserName}");
                        return new GeneralResponse(200, result._message, result._users);
                    }
                    else
                    {
                        _logger.LogWarning($"GetUser failed for user {searchRequest.UserName} with status {result._status}");
                        return new GeneralResponse(result._status, result._message);
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred during GetUser for user {searchRequest.UserName}");
                    return new GeneralResponse(400, ex.Message);
                }
            }
        }

        [HttpPost("GetUserByTag")]
        public async Task<GeneralResponse> GetUserByTag([FromBody] UserRequest searchRequest)
        {
            using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
            {
                _logger.LogInformation("GetUserByTag attempt started");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("GetUserByTag failed due to invalid data");
                    return new GeneralResponse(400, "Invalid data");
                }
                try
                {
                    var result = await _service.GetUserByTag(searchRequest);
                    if (result._status == 200)
                    {
                        _logger.LogInformation($"GetUserByTag successful for user {searchRequest.UserName}");
                        return new GeneralResponse(200, result._message, result._users);
                    }
                    else
                    {
                        _logger.LogWarning($"GetUserByTag failed for user {searchRequest.UserName} with status {result._status}");
                        return new GeneralResponse(result._status, result._message);
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred during GetUserByTag for user {searchRequest.UserName}");
                    return new GeneralResponse(400, ex.Message);
                }
            }
        }
    }
}