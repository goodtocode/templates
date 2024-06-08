using SemanticKernelMicroservice.Core.Domain.ChatCompletion;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;
using SemanticKernelMicroservice.Core.Domain.Subject;

namespace SemanticKernelMicroservice.Core.Application.Abstractions;

public interface IChatCompletionContext
{
    DbSet<AuthorEntity> Authors { get; }
    DbSet<ChatMessageEntity> ChatMessages { get; }
    DbSet<ChatSessionEntity> ChatSessions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}