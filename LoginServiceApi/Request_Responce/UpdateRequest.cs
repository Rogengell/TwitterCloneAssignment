using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServiceApi.Request_Responce
{
    public class UpdateRequest
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
    }
}