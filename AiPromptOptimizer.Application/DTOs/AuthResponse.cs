namespace AiPromptOptimizer.Application.DTOs;

public class AuthResponse
{
    public required string Token { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}