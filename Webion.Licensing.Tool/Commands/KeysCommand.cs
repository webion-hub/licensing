using ThinkSharp.Licensing;

namespace Webion.Licensing.Tool.Commands;

internal sealed class KeysCommand : Command
{  
    public KeysCommand() : base(
        name: "keys",
        description: "Get a couple of keys"
    )
    {
        this.SetHandler(Handle);
    }

    public void Handle()
    {
        var keys = Lic.KeyGenerator.GenerateRsaKeyPair();
        
        Console.WriteLine($"-----BEGIN RSA PUBLIC KEY-----\n{keys.PublicKey}\n-----END RSA PUBLIC KEY-----\n");
        Console.WriteLine($"-----BEGIN RSA PRIVATE KEY-----\n{keys.PrivateKey}\n-----END RSA PRIVATE KEY-----");
    }
}