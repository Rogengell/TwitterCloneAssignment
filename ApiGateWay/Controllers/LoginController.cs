using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Request_Responce;
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
