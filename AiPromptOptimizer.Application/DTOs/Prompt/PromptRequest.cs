using AiPromptOptimizer.Application.Enums;

namespace AiPromptOptimizer.Application.DTOs.Prompt;

public class PromptRequest
{
    public required string UserPrompt { get; set; }
    public required PromptCategory PromptCategory { get; set; }
}