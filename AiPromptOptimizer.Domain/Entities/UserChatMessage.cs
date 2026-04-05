using AiPromptOptimizer.Domain.Enums;

namespace AiPromptOptimizer.Domain.Entities;

public class UserChatMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserChatId { get; set; }

    public int SequenceNumber { get; set; }

    public Role Role { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public UserChat UserChat { get; set; } = null!;
}