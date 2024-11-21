using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServiceApi.Request_Responce;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LoginServiceApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FaucetController : ControllerBase
    {
        private string _jwtKey;
        private string _jwtIssuer;

        public FaucetController(string jwtKey, string jwtIssuer)
        {
            _jwtKey = jwtKey;
            _jwtIssuer = jwtIssuer;
        }

        [HttpGet("GetToken")]
        public GeneralResponce GetToken()
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    _jwtIssuer,
                    _jwtIssuer,
                    null,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                System.Console.WriteLine("token: " + token);
                return new GeneralResponce(200, token);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Something went wrong getting token " + ex.Message);
                return new GeneralResponce(400, ex.Message);
            }
        }
    }
}