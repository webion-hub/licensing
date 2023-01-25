namespace Webion.Licensing.Tool.Config.Commands;

internal sealed class ConfigGenCommand : Command
{
    private readonly Option<string> config = new("--config");
    private readonly Argument<FileInfo> output = new();

    public ConfigGenCommand() : base(
        name: "gen",
        description: "gen the license with config"
    )
    {
        config.IsRequired = true;
        config.AddAlias("-c");

        this.AddOption(config);
        this.AddArgument(output);
        this.SetHandler(HandleAsync, output, config);
    }

    public async Task HandleAsync(FileInfo output, string config)
    {
        using var stream = output.OpenWrite();
        using var ctx = Db.GetContext();
        var configs = new LicenseConfigsRepository(ctx);

        var c = await configs.GetAsync(config, default);
        if (c is null)
            return;

        var l = License.Generate(c);

        using var writer = new StreamWriter(stream);
        writer.WriteLine(l);
        Console.WriteLine(l);
    }
}