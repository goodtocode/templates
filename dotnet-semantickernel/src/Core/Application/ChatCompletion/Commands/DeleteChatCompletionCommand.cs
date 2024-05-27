using SemanticKernelMicroservice.Core.Application.Common.Exceptions;
using SemanticKernelMicroservice.Core.Application.Common.Interfaces;

namespace SemanticKernelMicroservice.Core.Application.ChatCompletions.Commands.Remove;

public class RemoveChatCompletionCommand : IRequest
{
    public Guid Key { get; set; }
}

public class RemoveChatCompletionCommandHandler : IRequestHandler<RemoveChatCompletionCommand>
{
    private readonly ISemanticKernelMicroserviceContext _context;

    public RemoveChatCompletionCommandHandler(ISemanticKernelMicroserviceContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveChatCompletionCommand request, CancellationToken cancellationToken)
    {
        var weatherChatCompletion = _context.ChatCompletions.Find(request.Key);

        if (weatherChatCompletion == null) throw new CustomNotFoundException();
        _context.ChatCompletions.Remove(weatherChatCompletion);
        await _context.SaveChangesAsync(cancellationToken);
    }
}