﻿using Goodtocode.Domain.Types;

namespace SemanticKernelMicroservice.Core.Domain.ChatCompletion
{
    public class ChatSessionEntity : DomainEntity<ChatSessionEntity>
    {
        public DateTime Timestamp { get; set; }
        public ICollection<ChatMessageEntity>? Messages { get; set; }
    }
}