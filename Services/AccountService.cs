using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Account;
using Library.Interfaces.Services;
using Library.Models;
using Microsoft.AspNetCore.Identity;

namespace Library.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<User> userManager, ILogger<AccountService> logger, ITokenService tokenService)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var userCount = _userManager.Users.Count();

                string role = (userCount == 0) ? "Admin" : "User";
                var user = new User
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };
                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if (roleResult.Succeeded)
                    {
                        return new AuthResponseDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        }; // كل شيء تمام                
                    }
                    else
                    {
                        return new AuthResponseDto
                        {
                            Errors = roleResult.Errors.Select(e => e.Description).ToList()
                        }; ; // فشل في الإضافة للدور
                    }
                }
                else
                {
                    return new AuthResponseDto
                    {
                        Errors = createdUser.Errors.Select(e => e.Description).ToList()
                    }; // فشل الإنشاء، رجّع الخطأ نفسه
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user");
                return new AuthResponseDto
                {
                    Errors = new List<string> { "An unexpected error occurred while registering the user." }
                }; ;
            }
        }
    }
}