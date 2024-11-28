using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;


[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly ApiGateWay.Service.ILoginService _service;
    private readonly ILogger<LoginController> _logger;


    public LoginController(ApiGateWay.Service.ILoginService service, ILogger<LoginController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Add contextual information to the log
        using (LogContext.PushProperty("Username", username ?? "UnknownUser"))
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("Login attempt started");

            // Example log to show current directory (can be removed)
            System.Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "../EFramework"));

            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
            {
                _logger.LogWarning("Login failed due to empty username or password");
                return StatusCode(400, "Empty username or password");
            }

            try
            {
                _logger.LogInformation($"Calling login service for user {username}");
                var result = await _service.Login(username, password);

                if (result._status == 200)
                {
                    _logger.LogInformation($"Login successful for user {username}");
                    return StatusCode(200, result);
                }
                else
                {
                    _logger.LogWarning($"Login failed for user {username} with status {result._status}");
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during login for user {username}");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
    }

    [HttpPut("Create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateRequest request)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("creating user attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("creating user failed due to invalid data");
                return StatusCode(400, "Invalid data");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {request.email}");
                var result = await _service.CreateAccount(request);
                if (result._status == 200)
                {
                    _logger.LogInformation($"creating user successful for user {request.email}");
                    return StatusCode(200, result);
                }
                else
                {
                    _logger.LogWarning($"creating user failed for user {request.email} with status {result._status}");
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during creating user for user {request.email}");
                Console.WriteLine("Something went wrong creating Account" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateRequest updateRequest)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("updating user attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("updating user failed due to invalid data");
                return StatusCode(400, "Invalid data");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {updateRequest.Email}");
                var result = await _service.UpdateAccount(updateRequest);
                if (result._status == 200)
                {
                    _logger.LogInformation($"updating user successful for user {updateRequest.Email}");
                    return StatusCode(200, result);
                }
                else
                {
                    _logger.LogWarning($"updating user failed for user {updateRequest.Email} with status {result._status}");
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during updating user for user {updateRequest.Email}");
                Console.WriteLine("Something went wrong createing the logging" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteRequest request)
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("deleting user attempt started");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("deleting user failed due to invalid data");
                return StatusCode(400, "Empty username or password");
            }
            try
            {
                _logger.LogInformation($"Calling login service for user {request.email}");
                var result = await _service.DeleteAccount(request);
                if (result._status == 200)
                {
                    _logger.LogInformation($"deleting user successful for user {request.email}");
                    return StatusCode(200, result);
                }
                else
                {
                    _logger.LogWarning($"deleting user failed for user {request.email} with status {result._status}");
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during deleting user for user {request.email}");
                Console.WriteLine("Something went wrong createing the logging" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
    }

    [HttpGet("GEtAuthenticated")]
    public async Task<IActionResult> GetAuthenticated()
    {
        using (LogContext.PushProperty("RequestId", Guid.NewGuid().ToString()))
        {
            _logger.LogInformation("Getting authenticated attempt started");
            try
            {
                _logger.LogInformation($"Calling login service for user ");
                var result = await _service.GetAuthenticated();
                if (result._status == 200)
                {
                    _logger.LogInformation($"Getting authenticated successful for user ");
                    return StatusCode(200, result);
                }
                else
                {
                    _logger.LogWarning($"Getting authenticated failed for user  with status {result._status}");
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"An error occurred during getting authenticated for user ");
                Console.WriteLine("Something went wrong getting authenticated" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
    }
}
