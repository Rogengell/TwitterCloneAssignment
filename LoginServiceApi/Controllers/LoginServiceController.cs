using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFramework.Data;
using LoginServiceApi.Request_Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LoginServiceController : Controller
{
    private readonly LoginServiceApi.Service.ILoginService _service;
    private readonly ILogger<LoginServiceController> _logger;

    public LoginServiceController(LoginServiceApi.Service.ILoginService service, ILogger<LoginServiceController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("Login")]
    public async Task<GeneralResponce> Login([FromBody] LoginRequest loginRequest)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("Login attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login failed due to invalid data");
                return new GeneralResponce(400, "Invalid data");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {loginRequest.Email}");
                GeneralResponce result = await _service.Login(loginRequest);
                if (result._status == 200)
                {
                    _logger.LogInformation($"Login successful for user {loginRequest.Email}");
                    return result;
                }
                else
                {
                    _logger.LogWarning($"Login failed for user {loginRequest.Email} with status {result._status}");
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during Login for user {loginRequest.Email}");
                return new GeneralResponce(400, ex.Message);
            }
        }
    }

    [HttpPut("Create")]
    public async Task<GeneralResponce> CreateAccount([FromBody] CreateRequest request)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("creating user attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("creating user failed due to invalid data");
                return new GeneralResponce(400, "Invalid data");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {request.email}");
                GeneralResponce result = await _service.CreateAccount(request);
                if (result._status == 200)
                {
                    _logger.LogInformation($"creating user successful for user {request.email}");
                    return result;
                }
                else
                {
                    _logger.LogWarning($"creating user failed for user {request.email} with status {result._status}");
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during creating user for user {request.email}");
                return new GeneralResponce(400, ex.Message);
            }
        }
    }

    [HttpPost("Update")]
    public async Task<GeneralResponce> UpdateAccount([FromBody] UpdateRequest updateRequest)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("update user attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("update user failed due to invalid data");
                return new GeneralResponce(400, "Invalid data");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {updateRequest.Email}");
                GeneralResponce result = await _service.UpdateAccount(updateRequest);
                if (result._status == 200)
                {
                    _logger.LogInformation($"updating user successful for user {updateRequest.Email}");
                    return result;
                }
                else
                {
                    _logger.LogWarning($"updating user failed for user {updateRequest.Email} with status {result._status}");
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during updating user for user {updateRequest.Email}");
                return new GeneralResponce(400, ex.Message);
            }
        }
    }

    [HttpPost("Delete")]
    public async Task<GeneralResponce> DeleteAccount([FromBody] DeleteRequest request)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("delete user attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("delete user failed due to invalid data");
                return new GeneralResponce(400, "Invalid data");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {request.email}");
                GeneralResponce result = await _service.DeleteAccount(request);
                if (result._status == 200)
                {
                    _logger.LogInformation($"deleting user successful for user {request.email}");
                    return result;
                }
                else
                {
                    _logger.LogWarning($"deleting user failed for user {request.email} with status {result._status}");
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during deleting user for user {request.email}");
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}
