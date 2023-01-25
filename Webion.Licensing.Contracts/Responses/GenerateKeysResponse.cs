namespace Webion.Licensing.Contracts.Responses;

public sealed class GenerateKeysResponse
{
    public required string PublicKey { get; init; }
    public required string PrivateKey { get; init; }
}