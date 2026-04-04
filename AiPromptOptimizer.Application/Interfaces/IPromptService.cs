using AiPromptOptimizer.Application.DTOs;

namespace AiPromptOptimizer.Application.Interfaces;

public interface IPromptService
{
    Task<ChatResponse> GetImprovedPromptAsync(ChatRequest request);
}