using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateWay.Service;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var result = await _service.GetAllUser();
                if (result._status == 200)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetAllUser" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(string searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Invalid data");
            }
            try
            {
                var result = await _service.GetUser(searchRequest);
                if (result._status == 200)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetUser" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost("GetUserByTag")]
        public async Task<IActionResult> GetUserByTag(string searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Invalid data");
            }
            try
            {
                var result = await _service.GetUserByTag(searchRequest);
                if (result._status == 200)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetUserByTag" + ex.Message);
                return StatusCode(400, ex.Message);
            }
        }
    }
}