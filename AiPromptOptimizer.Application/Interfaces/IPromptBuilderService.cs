using AiPromptOptimizer.Application.DTOs;

namespace AiPromptOptimizer.Application.Interfaces;

public interface IPromptBuilderService
{
    string BuildInitialPrompt(ChatRequest request);

    string BuildRefinementPrompt(ChatRequest request);
}