using System.Text.Json;
using AiPromptOptimizer.Application.DTOs.Prompt;
using AiPromptOptimizer.Application.Interfaces;
using AiPromptOptimizer.Infrastructure.Interfaces;

namespace AiPromptOptimizer.Application.Services;

public class PromptService : IPromptService
{
    private readonly IAiInfrastructureService _aiInfrastructureService;
    private readonly IPromptBuilderService _promptBuilderService;

    public PromptService(IAiInfrastructureService aiInfrastructureService,
        IPromptBuilderService promptBuilderService)
    {
        _aiInfrastructureService = aiInfrastructureService;
        _promptBuilderService = promptBuilderService;
    }

    public async Task<PromptResponse> GetImprovedPromptAsync(PromptRequest request)
    {
        var finalPrompt = _promptBuilderService.BuildPrompt(request);
        var improvedPrompt = await _aiInfrastructureService.GenerateAsync(finalPrompt);

        try
        {
            var parsedResponse = JsonSerializer.Deserialize<PromptResponse>(improvedPrompt);

            if (parsedResponse != null)
            {
                return parsedResponse;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new PromptResponse()
        {
            ImprovedPrompt = improvedPrompt,
            Issues = new(),
            Suggestions = new()
        };
    }
}