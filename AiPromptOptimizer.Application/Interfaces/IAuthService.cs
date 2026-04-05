using AiPromptOptimizer.Application.DTOs;

namespace AiPromptOptimizer.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}