namespace Webion.Licensing.Application.Results;

public sealed class ReadLicenseResult
{
    public required BaseLicenseTerms License { get; init; }
}