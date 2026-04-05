using AiPromptOptimizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiPromptOptimizer.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserChat> UserChats { get; set; }
    public DbSet<UserChatMessage> UserChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}