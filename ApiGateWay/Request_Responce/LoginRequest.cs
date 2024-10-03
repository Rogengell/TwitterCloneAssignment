using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateWay.Request_Responce
{
    public class LoginRequest
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? userName { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1)]
        public string? password { get; set; }
    }
}