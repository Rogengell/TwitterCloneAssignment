using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWay.Request_Responce
{
    public class CreateRequest
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? email { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? password { get; set; }
        [Required]
        public string? ReplyTo { get; set; } 

        public CreateRequest(string email, string password, string replyTo)
        {
            this.email = email;
            this.password = password;
            this.ReplyTo = replyTo;
        }
    }
}