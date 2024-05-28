namespace SemanticKernelMicroservice.Core.Application.ChatCompletions.Commands.Add;

public class AddChatCompletionCommandValidator : AbstractValidator<CreateChatCompletionCommand>
{
    public AddChatCompletionCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
    }
}