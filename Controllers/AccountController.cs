using Library.DTOs.Account;
using Library.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for Account!");
                    return BadRequest(ModelState);
                }
                var result = await _accountService.RegisterAsync(registerDto);
                if (result == null)
                    return BadRequest("Registration failed.");

                if (result.Errors != null && result.Errors.Any())
                    return BadRequest(result.Errors);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}