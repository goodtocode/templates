﻿using SemanticKernelMicroservice.Core.Domain.ChatCompletion;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;

namespace SemanticKernelMicroservice.Infrastructure.SqlServer.Persistence.Configurations;

public class ChatMessagesConfig : IEntityTypeConfiguration<ChatMessageEntity>
{
    public void Configure(EntityTypeBuilder<ChatMessageEntity> builder)
    {
        builder.ToTable("ChatMessages");
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key);
        builder.Property(x => x.Timestamp);
    }
}