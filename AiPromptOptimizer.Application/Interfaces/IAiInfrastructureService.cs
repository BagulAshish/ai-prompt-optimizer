namespace AiPromptOptimizer.Application.Interfaces;

public interface IAiInfrastructureService
{
    Task<string> GenerateAsync(string prompt);
}