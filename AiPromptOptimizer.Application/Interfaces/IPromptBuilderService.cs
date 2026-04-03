using AiPromptOptimizer.Application.DTOs.Prompt;

namespace AiPromptOptimizer.Application.Interfaces;

public interface IPromptBuilderService
{
    string BuildPrompt(PromptRequest request);
}