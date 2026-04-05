using AiPromptOptimizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPromptOptimizer.Infrastructure.Persistence.Configuration;

public class UserChatMessageConfiguration : IEntityTypeConfiguration<UserChatMessage>
{
    public void Configure(EntityTypeBuilder<UserChatMessage> builder)
    {
        builder.ToTable("user_chat_messages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(x => x.UserChatId)
            .HasColumnName("user_chat_id")
            .IsRequired();

        builder.Property(x => x.SequenceNumber)
            .HasColumnName("sequence_number")
            .IsRequired();

        builder.Property(x => x.Role)
            .HasColumnName("role")
            .HasConversion<string>();

        builder.Property(x => x.Content)
            .HasColumnName("content")
            .HasColumnType("text");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.HasOne(c => c.UserChat)
            .WithMany(c => c.UserChatMessages)
            .HasForeignKey(x => x.UserChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserChatId);

        builder.HasIndex(x => new { x.UserChatId, x.SequenceNumber })
            .IsUnique();
    }
}