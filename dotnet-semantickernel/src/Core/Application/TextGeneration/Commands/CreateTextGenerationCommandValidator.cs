namespace SemanticKernelMicroservice.Core.Application.TextGenerations.Commands.Add;

public class AddTextGenerationCommandValidator : AbstractValidator<CreateTextGenerationCommand>
{
    public AddTextGenerationCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
    }
}