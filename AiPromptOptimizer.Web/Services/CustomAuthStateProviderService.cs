using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace AiPromptOptimizer.Web.Services;

public class CustomAuthStateProviderService : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public CustomAuthStateProviderService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine("GetAuthenticationStateAsync called");
        var token = await _localStorage.GetItemAsync<string>("authToken");
        Console.WriteLine($"Token: {token}");

        if (string.IsNullOrWhiteSpace(token))
        {
            Console.WriteLine("Token missing");

            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity()));
        }

        Console.WriteLine("User authenticated");

        var claims = ParseClaimsFromJwt(token);

        var identity = new ClaimsIdentity(claims, "jwt");

        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);

        var identity = new ClaimsIdentity(claims, "jwt");

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var anonymous = new ClaimsPrincipal(
            new ClaimsIdentity());

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(anonymous)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();

        var token = handler.ReadJwtToken(jwt);

        return token.Claims;
    }

}