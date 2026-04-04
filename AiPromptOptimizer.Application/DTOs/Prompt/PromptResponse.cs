namespace AiPromptOptimizer.Application.DTOs.Prompt;

public class PromptResponse
{
    public required string ImprovedPrompt { get; set; }

    public List<string> Issues { get; set; } = new();

    public List<string> Suggestions { get; set; } = new();
}