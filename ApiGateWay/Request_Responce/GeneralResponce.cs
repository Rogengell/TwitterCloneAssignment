using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace ApiGateWay.Request_Responce
{
    public class GeneralResponce
    {
        public int _status { get; set; }
        public string _message { get; set;}
        public UsersTable? _user { get; set; }
        [Required]
        public string? ReplyTo { get; set; } 


        public GeneralResponce(int status, string message, UsersTable user)
        {
            _status = status;
            _message = message;
            _user = user;
        }

        public GeneralResponce(int status, string message)
        {
            _status = status;
            _message = message;
        }
    }
}