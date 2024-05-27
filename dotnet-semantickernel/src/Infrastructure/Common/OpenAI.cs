using System.ComponentModel.DataAnnotations;

namespace SemanticKernelMicroservice.Infrastructure.Common;

/// <summary>
/// OpenAI settings.
/// </summary>
public sealed class OpenAI
{
    [Required]
    public string ChatModelId { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}
