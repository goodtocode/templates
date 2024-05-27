using System.ComponentModel.DataAnnotations;

namespace SemanticKernelMicroservice.Infrastructure.Common;

/// <summary>
/// Azure OpenAI settings.
/// </summary>
public sealed class AzureOpenAI
{
    [Required]
    public string ChatDeploymentName { get; set; } = string.Empty;

    [Required]
    public string Endpoint { get; set; } = string.Empty;

    [Required]
    public string ApiKey { get; set; } = string.Empty;
}