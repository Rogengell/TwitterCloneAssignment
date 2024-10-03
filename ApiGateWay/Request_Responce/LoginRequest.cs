using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWay.Request_Responce
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set;}
        public string? ReplyTo { get; set; } 


        public LoginRequest(string email, string password, string replyTo)
        {
            Email = email;
            Password = password;
            ReplyTo = replyTo;
        }
    }
}