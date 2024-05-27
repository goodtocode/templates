using SemanticKernelMicroservice.Core.Application.ChatCompletion.Queries;
using SemanticKernelMicroservice.Presentation.WebApi.Common;

namespace SemanticKernelMicroservice.Presentation.WebApi.ChatCompletion;

/// <summary>
/// Chat completion endpoints to create a chat, continue a chat, delete a chat and retrieve chat history
/// </summary>
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("[controller]")]
[ApiVersion("1.0")]
public class ChatCompletionController : ApiControllerBase
{
    /// <summary>Get Chat Completion session with history</summary>
    /// <remarks>
    /// Sample request:
    ///
    ///        "Key": 60fb5e99-3a78-43df-a512-7d8ff498499e
    ///        "api-version":  1.0
    /// 
    /// </remarks>
    /// <returns>WeatherChatCompletionVm</returns>
    [HttpGet("{key}", Name = "GetChatCompletionQuery")]
    [ProducesResponseType(typeof(ChatCompletionVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GetChatCompletionQuery> Get(Guid key)
    {
        return await Mediator.Send(new GetChatCompletionQuery
        {
            Key = key
        });
    }

    /// <summary>
    /// Creates new Chat Completion session with empty history
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     HttpPost Body
    ///     {
    ///        "Message": "Hi, I'm Robert. What is the weather today?",
    ///     }
    ///
    ///     "version":  1.0
    /// </remarks>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateChatCompletionCommand")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post(CreateChatCompletionCommand command)
    {
        await Mediator.Send(command);
        return CreatedAtAction(nameof(Post), new { command.Key }, command);
    }

    /// <summary>
    /// Update ChatCompletion Command
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     HttpPut Body
    ///     {
    ///        "Key": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "Message": "Hi, I'm Robert. What is the weather today?",
    ///        "Response": "The weather is 75 degrees Fahrenheit and sunny.",
    ///     }
    ///
    ///     "version":  1.0
    /// </remarks>
    /// <param name="command"></param>
    /// <returns>NoContent</returns>
    [HttpPut(Name = "UpdateChatCompletionCommand")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(UpdateChatCompletionCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Patch ChatCompletion Command
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     HttpPatch Body
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
    /// <returns>NoContent</returns>
    [HttpPatch(Name = "PatchChatCompletionCommand")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Patch(PatchChatCompletionCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>Remove ChatCompletion Command</summary>
    /// <remarks>
    /// Sample request:
    ///
    ///        "Key": 60fb5e99-3a78-43df-a512-7d8ff498499e
    ///        "api-version":  1.0
    /// 
    /// </remarks>
    /// <returns>NoContent</returns>
    [HttpDelete("{key}", Name = "RemoveChatCompletionCommand")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(Guid key)
    {
        await Mediator.Send(new RemoveChatCompletionCommand() { Key = key });

        return NoContent();
    }
}