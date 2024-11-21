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

    [Route("[controller]")]
    [ApiController]
    public class FaucetController : ControllerBase
    {
        private string _jwtKey;
        private string _jwtIssuer;

        public FaucetController(Settings settings)
        {
            _jwtKey = settings.JwtKey;
            _jwtIssuer = settings.JwtIssuer;
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