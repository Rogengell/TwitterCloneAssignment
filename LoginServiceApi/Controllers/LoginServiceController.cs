using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFramework.Data;
using LoginServiceApi.Request_Responce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LoginServiceController : Controller
{
    private readonly LoginServiceApi.Service.ILoginService _service;
    public LoginServiceController(LoginServiceApi.Service.ILoginService service)
    {
        _service = service;
    }

    [HttpPost("Login")]
    public async Task<GeneralResponce> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponce(400, "Invalid data");
        }
        Console.WriteLine("Login");
        try
        {
            GeneralResponce result = await _service.Login(loginRequest);
            if (result._status == 200)
            {
                System.Console.WriteLine("200");
                return result;
            }
            else
            {
                System.Console.WriteLine(result._status);
                return result;
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong Login" + ex.Message);
            return new GeneralResponce(400, ex.Message);
        }
    }

    [HttpPut("Create")]
    public async Task<GeneralResponce> CreateAccount([FromBody] CreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponce(400, "Invalid data");
        }
        try
        {
            GeneralResponce result = await _service.CreateAccount(request);
            if (result._status == 200)
            {
                return result;
            }
            else
            {
                return result;
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong creating Account" + ex.Message);
            return new GeneralResponce(400, ex.Message);
        }
    }

    [HttpPost("Update")]
    public async Task<GeneralResponce> UpdateAccount([FromBody] UpdateRequest updateRequest)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponce(400, "Invalid data");
        }
        try
        {
            GeneralResponce result = await _service.UpdateAccount(updateRequest);
            if (result._status == 200)
            {
                return result;
            }
            else
            {
                return result;
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong createing the logging" + ex.Message);
            return new GeneralResponce(400, ex.Message);
        }
    }

    [HttpPost("Delete")]
    public async Task<GeneralResponce> DeleteAccount([FromBody] DeleteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return new GeneralResponce(400, "Invalid data");
        }
        try
        {
            GeneralResponce result = await _service.DeleteAccount(request);
            if (result._status == 200)
            {
                return result;
            }
            else
            {
                return result;
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong createing the logging" + ex.Message);
            return new GeneralResponce(400, ex.Message);
        }
    }
}
