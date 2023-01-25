namespace Webion.Licensing.Api.Mappings;

public static class GenerateKeysMapping
{
    public static GenerateKeysResponse ToResponse(this GenerateKeysResult result) => new()
    {
        PrivateKey = result.PrivateKey,
        PublicKey = result.PublicKey,
    };
}