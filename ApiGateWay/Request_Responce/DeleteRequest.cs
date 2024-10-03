using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWay.Request_Responce
{
    public class DeleteRequest
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? email { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? password { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? Id { get; set; }
        [Required]
        public string? ReplyTo { get; set; } 

        public DeleteRequest(string email, string password, int Id, string replyTo)
        {
            this.email = email;
            this.password = password;
            this.Id = Id;
            this.ReplyTo = replyTo;
        }
    }
}