using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceApi
{
    public class Settings
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
    }
}