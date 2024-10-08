using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServiceApi.Request_Responce
{
    public class CreateRequest
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? email { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? password { get; set; }

        public CreateRequest(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}