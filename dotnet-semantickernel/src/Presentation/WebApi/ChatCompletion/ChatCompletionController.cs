using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelMicroservice.Presentation.WebApi.Common;
using static CSharpFunctionalExtensions.Result;

namespace SemanticKernelMicroservice.Presentation.WebApi.ChatCompletion;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("[controller]")]
[ApiVersion("1.0")]
public class ChatCompletionController : ApiControllerBase
{
    private IChatCompletionService _chatService;

    public ChatCompletionController(IConfiguration configuration, IChatCompletionService chatService)
    {
        _chatService = chatService;
    }

    /// <summary>
    /// Add Forecast Command
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     HttpPost Body
    ///     {
    ///        "Key": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "Date": "2023-06-08T23:32:05.256Z",
    ///        "TemperatureC": 0,
    ///        "Zipcodes": [ 92602, 92673 ]
    ///     }
    ///
    ///     "version":  1.0
    /// </remarks>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost(Name = "AddChatCompletionCommand")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> AddChatCompletionCommand()//AddChatCompletionCommand command, IConfiguration configuration)
    {

        //var kernel = Kernel.CreateBuilder().AddOpenAIChatCompletion(configuration.GetValue<string>("AI:OpenAI:Model") ?? "gtp-3.5-turbo",
        //    configuration.GetValue<string>("AI:OpenAI:ApiKey") ?? string.Empty).Build();

        //var chatService = kernel.GetRequiredService<IChatCompletionService>();
        Microsoft.SemanticKernel.ChatCompletion.ChatHistory chat = new();
        chat.AddUserMessage("My question");
        var response = await _chatService.GetChatMessageContentAsync(chat);

        //await Mediator.Send(command);

        //return CreatedAtAction(nameof(Get), new { command.Key }, command);
        return Ok();
    }
}