namespace AiPromptOptimizer.Application.DTOs;

public class ChatResponse
{
    public required string ImprovedPrompt { get; set; }

    public List<string> Issues { get; set; } = new();

    public List<string> Suggestions { get; set; } = new();

    public List<string> Changes { get; set; } = new();
}