namespace SemanticKernelMicroservice.Core.Application.ChatCompletions.Commands.Remove;

public class RemoveChatCompletionCommandValidator : AbstractValidator<RemoveChatCompletionCommand>
{
    public RemoveChatCompletionCommandValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
    }
}