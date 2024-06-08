using SemanticKernelMicroservice.Core.Domain.ChatCompletion;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;
using SemanticKernelMicroservice.Core.Domain.Subject;

namespace SemanticKernelMicroservice.Infrastructure.SqlServer.Persistence.Configurations;

public class AuthorsConfig : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.ToTable("Authors");
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key);
    }
}