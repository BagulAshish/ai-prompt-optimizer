using AiPromptOptimizer.Web.Models;

namespace AiPromptOptimizer.Web.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error occurred during login: {response.Content}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/auth/register",
            request);

        return response.IsSuccessStatusCode;
    }
}