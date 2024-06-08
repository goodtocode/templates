using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelMicroservice.Core.Application.Abstractions;
using SemanticKernelMicroservice.Core.Domain.ChatCompletion;
using SemanticKernelMicroservice.Core.Domain.Subject;
using System.Reflection;

namespace SemanticKernelMicroservice.Infrastructure.SqlServer.Persistence
{
    public class ChatCompletionContext : DbContext, IChatCompletionContext
    {
        // Roles: User, Assistant, System
        public DbSet<AuthorEntity> Authors => Set<AuthorEntity>();
        public DbSet<ChatMessageEntity> ChatMessages => Set<ChatMessageEntity>();
        public DbSet<ChatSessionEntity> ChatSessions => Set<ChatSessionEntity>();

        protected ChatCompletionContext() { }

        public ChatCompletionContext(DbContextOptions<ChatCompletionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                x => x.Namespace == $"{GetType().Namespace}.Configurations");
        }
    }
}
