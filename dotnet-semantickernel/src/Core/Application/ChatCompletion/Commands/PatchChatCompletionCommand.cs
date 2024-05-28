//using SemanticKernelMicroservice.Core.Application.Common.Exceptions;
//using SemanticKernelMicroservice.Core.Application.Common.Interfaces;

//namespace SemanticKernelMicroservice.Core.Application.ChatCompletions.Commands.Patch;

//public class PatchChatCompletionCommand : IRequest
//{
//    public Guid Key { get; set; }
//    public DateTime? Date { get; set; }
//    public int? TemperatureC { get; set; }
//    public List<int>? Zipcodes { get; set; }
//}

//public class PatchWeatherChatCompletionCommandHandler : IRequestHandler<PatchChatCompletionCommand>
//{
//    private readonly ISemanticKernelMicroserviceContext _context;

//    public PatchWeatherChatCompletionCommandHandler(ISemanticKernelMicroserviceContext context)
//    {
//        _context = context;
//    }

//    public async Task Handle(PatchChatCompletionCommand request, CancellationToken cancellationToken)
//    {
//        var weatherChatCompletion = _context.ChatCompletions.Find(request.Key);

//        if (weatherChatCompletion == null)
//            throw new CustomNotFoundException();
        
//        if (request.Date != null)
//            weatherChatCompletion.UpdateDate((DateTime) request.Date);

//        if (request.TemperatureC != null)
//            weatherChatCompletion.UpdateTemperatureF((int) request.TemperatureC);

//        if (request.Zipcodes != null)
//            weatherChatCompletion.UpdateZipcodes(request.Zipcodes);

//        _context.ChatCompletions.Update(weatherChatCompletion);
//        await _context.SaveChangesAsync(CancellationToken.None);
//    }
//}