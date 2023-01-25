namespace Webion.Licensing.Infrastructure.Handlers;

internal sealed class ReadLicenseHandler : IRequestHandler<ReadLicenseCommand, ReadLicenseResult>
{
    public Task<ReadLicenseResult> Handle(ReadLicenseCommand request, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(request.LicenseStream);
        var content = reader.ReadToEnd();
        var license = License.Deserialize(
            license: content,
            publicKey: request.PublicKey,
            appCode: request.AppCode
        );

        var result = new ReadLicenseResult
        {
            License = license,
        };

        return Task.FromResult(result);
    }
}