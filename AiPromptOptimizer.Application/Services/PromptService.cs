using System.Text.Json;
using AiPromptOptimizer.Application.DTOs;
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

    public async Task<ChatResponse> GetImprovedPromptAsync(ChatRequest request)
    {
        var finalPrompt = request.Messages.Count == 1
            ? _promptBuilderService.BuildInitialPrompt(request)
            : _promptBuilderService.BuildRefinementPrompt(request);

        var improvedPrompt = await _aiInfrastructureService.GenerateAsync(finalPrompt);

        try
        {
            var parsedResponse = JsonSerializer.Deserialize<ChatResponse>(improvedPrompt);

            if (parsedResponse != null)
            {
                return parsedResponse;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new ChatResponse()
        {
            ImprovedPrompt = improvedPrompt,
            Issues = new(),
            Suggestions = new()
        };
    }
}