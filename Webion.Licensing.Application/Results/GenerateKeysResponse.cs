namespace Webion.Licensing.Application.Results;

public sealed class GenerateKeysResult
{
    public required string PublicKey { get; init; }
    public required string PrivateKey { get; init; }
}