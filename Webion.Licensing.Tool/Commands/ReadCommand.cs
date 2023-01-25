namespace Webion.Licensing.Tool.Commands;

internal sealed class ReadCommand : Command
{
    private readonly Option<string> publicKey = new("--public-key");
    private readonly Option<string> appCode = new("--app-code");
    private readonly Argument<FileInfo> path = new();

    public ReadCommand() : base(
        name: "read",
        description: "Read the provided license"
    )
    {
        publicKey.IsRequired = true;
        publicKey.AddAlias("-k");
        appCode.IsRequired = true;
        appCode.AddAlias("-c");

        this.AddArgument(path);
        this.AddOption(publicKey);
        this.AddOption(appCode);
        this.SetHandler(Handle, path, publicKey, appCode);
    }

    public void Handle(FileInfo file, string publicKey, string appCode)
    {
        using var stream = file.OpenRead();
        using var reader = new StreamReader(stream);
        
        var content = reader.ReadToEnd();
        var license = License.Deserialize(content, publicKey, appCode);

        Console.WriteLine(license);
    }
}