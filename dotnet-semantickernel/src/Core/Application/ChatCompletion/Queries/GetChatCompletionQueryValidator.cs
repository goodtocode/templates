namespace SemanticKernelMicroservice.Core.Application.ChatCompletion.Queries;

public class GetChatCompletionQueryValidator : AbstractValidator<GetChatCompletionQuery>
{
    public GetChatCompletionQueryValidator()
    {
        RuleFor(x => x.Question).NotEmpty();
    }
}