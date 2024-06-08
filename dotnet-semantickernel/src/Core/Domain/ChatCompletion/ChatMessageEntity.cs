using Goodtocode.Domain.Types;

namespace SemanticKernelMicroservice.Core.Domain.ChatCompletion
{
    public class ChatMessageEntity : DomainEntity<ChatMessageEntity>
    {
        public Guid ChatSessionKey { get; set; } = Guid.Empty;
        public string Message { get; set; } = null!;
        public string Response { get; set; } = null!;
        public DateTime Timestamp { get; set; }

        public virtual ChatSessionEntity ChatSession { get; set; } = new();
    }
}
