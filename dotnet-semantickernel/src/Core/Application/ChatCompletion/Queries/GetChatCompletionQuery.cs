namespace SemanticKernelMicroservice.Core.Application.ChatCompletion.Queries;

public class GetChatCompletionQuery : IRequest<string>
{
    public string Question { get; set; }
}

public class GetChatCompletionQueryHandler : IRequestHandler<GetChatCompletionQuery, string>
{
    //public GetChatCompletionQueryHandler(IKernel kernel)
    //{

    //}

    public async Task<string> Handle(GetChatCompletionQuery request,
                                CancellationToken cancellationToken)
    {
        //var chatService = kernel.GetRequiredService<IChatCompletionService>();
        //ChatHistory chat = new();
        //chat.AddUserMessage(request.Question);
        //var response = await chatService.GetChatMessageContentAsync(chat);
        //return chat;

        return null;
    }
}