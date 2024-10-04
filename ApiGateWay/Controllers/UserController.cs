using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateWay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            //TODO: get all users api
            return StatusCode(200, "Success");
        }

        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser(string searchRequest)
        {
            //TODO: get user api
            return StatusCode(200, "Success");
        }

        [HttpPost("GetUserByTag")]
        public async Task<IActionResult> GetUserByTag(string searchRequest)
        {
            //TODO: get user by tag api
            return StatusCode(200, "Success");
        }
    }
}