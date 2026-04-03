using AiPromptOptimizer.Application.DTOs.Prompt;

namespace AiPromptOptimizer.Application.Interfaces;

public interface IPromptService
{
    Task<PromptResponse> GetImprovedPromptAsync(PromptRequest request);
}