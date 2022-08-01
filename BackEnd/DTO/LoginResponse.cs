using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTO
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
