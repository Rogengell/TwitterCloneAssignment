using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceApi.Service;
using Microsoft.AspNetCore.Mvc;
using UserServiceApi.Request_Responce;
using Microsoft.AspNetCore.Authorization;

namespace UserServiceApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserServiceController : ControllerBase
    {
        private readonly IUserService _service;

        public UserServiceController(IUserService service)
        {
            _service = service;
        }
        [HttpGet("GetAllUser")]
        public async Task<GeneralResponse> GetAllUser()
        {
            try
            {
                var result = await _service.GetAllUser();
                if (result._status == 200)
                {
                    return new GeneralResponse(200, result._message, result._users);
                }
                else
                {
                    return new GeneralResponse(404, "there is no user in database");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetAllUser" + ex.Message);
                return new GeneralResponse(400, ex.Message);
            }
        }

        [HttpPost("GetUser")]
        public async Task<GeneralResponse> GetUser([FromBody] UserRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return new GeneralResponse(400, "Invalid data");
            }
            try
            {
                var result = await _service.GetUser(searchRequest);
                if (result._status == 200)
                {
                    return new GeneralResponse(200, result._message, result._users);
                }
                else
                {
                    return new GeneralResponse(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetUser" + ex.Message);
                return new GeneralResponse(400, ex.Message);
            }
        }

        [HttpPost("GetUserByTag")]
        public async Task<GeneralResponse> GetUserByTag([FromBody] UserRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return new GeneralResponse(400, "Invalid data");
            }
            try
            {
                var result = await _service.GetUserByTag(searchRequest);
                if (result._status == 200)
                {
                    return new GeneralResponse(200, result._message, result._users);
                }
                else
                {
                    return new GeneralResponse(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetUserByTag" + ex.Message);
                return new GeneralResponse(400, ex.Message);
            }
        }
    }
}