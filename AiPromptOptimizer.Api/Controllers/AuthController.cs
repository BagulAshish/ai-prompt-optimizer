using AiPromptOptimizer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiPromptOptimizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password)
    {
        var token = await _authService.RegisterAsync(email, password);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var token = await _authService.LoginAsync(email, password);
        return Ok(token);
    }
}