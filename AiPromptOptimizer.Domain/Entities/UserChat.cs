using AiPromptOptimizer.Domain.Enums;

namespace AiPromptOptimizer.Domain.Entities;

public class UserChat
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public PromptCategory PromptCategory { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<UserChatMessage> UserChatMessages { get; set; } = new List<UserChatMessage>();
}