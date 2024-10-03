using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace ApiGateWay.Request_Responce
{
    public class LoginResponce
    {
        public int _status { get; set; }
        public string _message { get; set;}
        public UsersTable _user { get; set; }


        public LoginResponce(int status, string message, UsersTable user)
        {
            _status = status;
            _message = message;
            _user = user;
        }

        public LoginResponce(int status, string message)
        {
            _status = status;
            _message = message;
        }
    }
}