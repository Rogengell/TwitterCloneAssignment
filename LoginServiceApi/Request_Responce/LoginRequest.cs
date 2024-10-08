using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServiceApi.Request_Responce
{
    public class LoginRequest
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? Email { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? Password { get; set;}


        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}