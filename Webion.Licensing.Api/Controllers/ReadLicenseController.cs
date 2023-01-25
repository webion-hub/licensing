namespace Webion.Licensing.Api.Controllers;

[ApiController]
[Route("read")]
public sealed class ReadLicenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReadLicenseController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<IActionResult> HandleAsync(
        IFormFile licenseFile,
        [FromQuery] ReadLicenseRequest request,
        CancellationToken cancellationToken
    )
    {
        using var fileStream = licenseFile.OpenReadStream();
        var result = await _mediator.Send(
            request: request.ToCommand(fileStream),
            cancellationToken: cancellationToken
        );

        return Ok(result.ToResponse());
    }
}