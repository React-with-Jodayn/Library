using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace Library.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}