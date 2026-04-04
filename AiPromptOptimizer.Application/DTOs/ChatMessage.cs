namespace AiPromptOptimizer.Application.DTOs;

public class ChatMessage
{
    public required string Role { get; set; }

    public required string Content { get; set; }
}