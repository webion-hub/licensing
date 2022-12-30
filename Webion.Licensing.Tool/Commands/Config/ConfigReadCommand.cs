namespace Webion.Licensing.Commands;

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
        using var repository = new LicenseConfigsRepository();
        using var stream = input.OpenRead();
        using var reader = new StreamReader(stream);

        var content = await reader.ReadToEndAsync();
        var c = await repository.GetAsync(config, default);
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