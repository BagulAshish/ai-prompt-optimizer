using AiPromptOptimizer.Infrastructure.Interfaces;
using Google.GenAI;
using Microsoft.Extensions.Configuration;

namespace AiPromptOptimizer.Infrastructure.Services;

public class AiInfrastructureService : IAiInfrastructureService
{
    private readonly Client _client;
    private readonly string _model;

    public AiInfrastructureService(IConfiguration configuration)
    {
        var apiKey = configuration["AiAssistant:ApiKey"];
        _model = configuration["AiAssistant:Model"] ?? "";
        _client = new Client(apiKey: apiKey);
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var response = await _client.Models.GenerateContentAsync(
            model: _model,
            contents: prompt);

        return response.Candidates[0].Content.Parts[0].Text;
    }
}