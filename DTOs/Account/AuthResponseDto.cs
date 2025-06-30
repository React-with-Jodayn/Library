using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DTOs.Account
{
    public class AuthResponseDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }

    }
}