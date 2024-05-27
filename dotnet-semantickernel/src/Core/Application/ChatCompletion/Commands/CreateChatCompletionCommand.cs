//using FluentValidation.Results;
//using SemanticKernelMicroservice.Core.Application.Common.Exceptions;
//using SemanticKernelMicroservice.Core.Application.Common.Interfaces;

//namespace SemanticKernelMicroservice.Core.Application.ChatCompletions.Commands.Add;

//public class AddChatCompletionCommand : IRequest
//{
//    public string? Message { get; set; }
//}

//public class AddChatCompletionCommandHandler : IRequestHandler<AddChatCompletionCommand>
//{
//    private readonly ISemanticKernelMicroserviceContext _context;

//    public AddChatCompletionCommandHandler(ISemanticKernelMicroserviceContext context)
//    {
//        _context = context;
//    }

//    public async Task Handle(AddChatCompletionCommand request, CancellationToken cancellationToken)
//    {
//        var weatherChatCompletion = _context.ChatCompletions.Find(request.Key);
//        GuardAgainstWeatherChatCompletionNotFound(weatherChatCompletion);

//        var weatherChatCompletionValue = ChatCompletionValue.Create(request.Key, request.Date, (int) request.TemperatureF, request.Zipcodes);

//        if (weatherChatCompletionValue.IsFailure) 
//            throw new Exception(weatherChatCompletionValue.Error);

//        _context.ChatCompletions.Add(new ChatCompletion(weatherChatCompletionValue.Value));

//        await _context.SaveChangesAsync(CancellationToken.None);
//    }

//    private static void GuardAgainstWeatherChatCompletionNotFound(ChatCompletion? weatherChatCompletion)
//    {
//        if (weatherChatCompletion != null)
//            throw new CustomValidationException(new List<ValidationFailure>
//            {
//                new("Key", "A Weather ChatCompletion with this Key already exists")
//            });
//    }
//}