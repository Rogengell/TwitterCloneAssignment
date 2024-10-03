using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class LoginController : Controller
{
    private readonly ILoginService _service;
    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
        {
            return StatusCode(400,"Empty username or password");
        }
        try
        {
            var result = await _service.Login(username,password);
            if(result._status == 200)
            {
                return StatusCode(200,result);
            }
            else 
            {
                return StatusCode(result._status, result._message);
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong Login" + ex.Message);
            return StatusCode(400,ex.Message);
        }
    }

    [HttpPut("Create")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(400,"Invalid data");
        }
        try
        {
            var result = await _service.CreateAccount(request);
            if(result._status == 200)
            {
                return StatusCode(200,result);
            }
            else 
            {
                return StatusCode(result._status,result._message);
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong creating Account" + ex.Message);
            return StatusCode(400,ex.Message);
        }
    }

    [HttpPost("Update")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateRequest updateRequest)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(400,"Invalid data");
        }
        try
        {
            var result = await _service.UpdateAccount(updateRequest);
            if(result._status == 200)
            {
                return StatusCode(200,result);
            }
            else 
            {
                return StatusCode(result._status,result._message);
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong createing the logging" + ex.Message);
            return StatusCode(400,ex.Message);
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteAccount([FromBody] DeleteRequest request)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(400,"Empty username or password");
        }
        try
        {
            var result = await _service.DeleteAccount(request);
            if(result._status == 200)
            {
                return StatusCode(200,result);
            }
            else 
            {
                return StatusCode(result._status,result._message);
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Something went wrong createing the logging" + ex.Message);
            return StatusCode(400,ex.Message);
        }
    }
}
