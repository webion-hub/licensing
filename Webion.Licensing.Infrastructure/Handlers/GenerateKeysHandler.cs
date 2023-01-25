namespace Webion.Licensing.Infrastructure.Handlers;

internal sealed class GenerateKeysHandler : IRequestHandler<GenerateKeysCommand, GenerateKeysResult>
{
    public Task<GenerateKeysResult> Handle(GenerateKeysCommand request, CancellationToken cancellationToken)
    {
        var keys = Lic.KeyGenerator.GenerateRsaKeyPair();
        var result = new GenerateKeysResult
        {
            PublicKey = keys.PublicKey,
            PrivateKey = keys.PrivateKey,
        };

        return Task.FromResult(result);
    }
}