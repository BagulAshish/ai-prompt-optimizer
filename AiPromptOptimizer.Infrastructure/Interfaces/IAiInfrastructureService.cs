namespace AiPromptOptimizer.Infrastructure.Interfaces;

public interface IAiInfrastructureService
{
    Task<string> GenerateAsync(string prompt);
}