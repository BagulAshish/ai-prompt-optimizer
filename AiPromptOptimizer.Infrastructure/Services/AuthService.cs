using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AiPromptOptimizer.Application.DTOs;
using AiPromptOptimizer.Application.Interfaces;
using AiPromptOptimizer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AiPromptOptimizer.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<User> userManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception("Registration failed");

        var token = GenerateJwtToken(user);

        return new AuthResponse
        {
            Token = token,
            Email = request.Email,
            Password = request.Password
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            throw new Exception("Invalid credentials");

        var token = GenerateJwtToken(user);

        return new AuthResponse
        {
            Token = token,
            Email = request.Email,
            Password = request.Password
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}