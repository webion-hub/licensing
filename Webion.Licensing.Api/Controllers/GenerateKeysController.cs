namespace Webion.Licensing.Api.Enpdoints;

[ApiController]
[Route("keys")]
public sealed class GenerateKeysController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenerateKeysController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> HandleAsync(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            request: new GenerateKeysCommand(),
            cancellationToken: cancellationToken
        );

        return Ok(result.ToResponse());
    }
}