namespace SemanticKernelMicroservice.Core.Application.TextGeneration.Commands;

public class AddTextGenerationCommandValidator : AbstractValidator<CreateTextGenerationCommand>
{
    public AddTextGenerationCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
    }
}