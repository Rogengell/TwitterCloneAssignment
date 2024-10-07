using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceApi.Service;
using Microsoft.AspNetCore.Mvc;
using ApiGateWay.Request_Responce;

namespace UserServiceApi.Controllers
{
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
        public async Task<GeneralResponce> GetAllUser()
        {
            try
            {
                var result = await _service.GetAllUser();
                if (result._status == 200)
                {
                    return new GeneralResponce(200, result._message, result._users);
                }
                else
                {
                    return new GeneralResponce(404, "there is no user in database");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetAllUser" + ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        [HttpPost("GetUser")]
        public async Task<GeneralResponce> GetUser([FromBody] UserRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return new GeneralResponce(400, "Invalid data");
            }
            try
            {
                var result = await _service.GetUser(searchRequest);
                if (result._status == 200)
                {
                    return new GeneralResponce(200, result._message, result._users);
                }
                else
                {
                    return new GeneralResponce(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetUser" + ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }

        [HttpPost("GetUserByTag")]
        public async Task<GeneralResponce> GetUserByTag([FromBody] UserRequest searchRequest)
        {
            if (!ModelState.IsValid)
            {
                return new GeneralResponce(400, "Invalid data");
            }
            try
            {
                var result = await _service.GetUserByTag(searchRequest);
                if (result._status == 200)
                {
                    return new GeneralResponce(200, result._message, result._users);
                }
                else
                {
                    return new GeneralResponce(result._status, result._message);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Something went wrong GetUserByTag" + ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}