namespace Webion.Licensing.Tool.Config.Commands;

internal sealed class ConfigReadCommand : Command
{
    private readonly Option<string> config = new("--config");
    private readonly Argument<FileInfo> input = new();

    public ConfigReadCommand() : base(
        name: "read",
        description: "Read the license with config"
    )
    {
        config.IsRequired = true;
        config.AddAlias("-c");

        this.AddOption(config);
        this.AddArgument(input);
        this.SetHandler(HandleAsync, input, config);
    }

    public async Task HandleAsync(FileInfo input, string config)
    {
        using var ctx = Db.GetContext();
        using var stream = input.OpenRead();
        using var reader = new StreamReader(stream);
        var configs = new LicenseConfigsRepository(ctx);
        var content = await reader.ReadToEndAsync();

        var c = await configs.GetAsync(config, default);
        if (c is null)
            return;


        var license = License.Deserialize(
            license: content,
            publicKey: c.PublicKey,
            appCode: c.AppCode
        );

        Console.WriteLine(license);
    }
}