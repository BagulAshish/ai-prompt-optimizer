using AiPromptOptimizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPromptOptimizer.Infrastructure.Persistence.Configuration;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.ToTable("user_chats");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

        builder.Property(x => x.PromptCategory)
            .HasColumnName("prompt_category")
            .HasConversion<string>();

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");
    }
}