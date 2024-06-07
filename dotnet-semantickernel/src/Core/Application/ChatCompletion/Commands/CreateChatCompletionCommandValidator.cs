namespace SemanticKernelMicroservice.Core.Application.ChatCompletion.Commands;

public class AddChatCompletionCommandValidator : AbstractValidator<CreateChatCompletionCommand>
{
    public AddChatCompletionCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
    }
}