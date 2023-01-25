namespace Webion.Licensing.Application.Commands;

public sealed class ReadLicenseCommand : IRequest<ReadLicenseResult>
{
    public required Stream LicenseStream { get; init; }
    public required string PublicKey { get; init; }
    public required string AppCode { get; init; }
}
