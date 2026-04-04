using AiPromptOptimizer.Application.Enums;

namespace AiPromptOptimizer.Application.DTOs;

public class ChatRequest
{
    public IList<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    public PromptCategory PromptCategory { get; set; }
}