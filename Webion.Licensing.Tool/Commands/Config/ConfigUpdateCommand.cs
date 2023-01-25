namespace Webion.Licensing.Tool.Config.Commands;

internal sealed class ConfigUpdateCommand : Command
{
    private readonly Option<string> privateKey = new("--private-key");
    private readonly Option<string> publicKey = new("--public-key");
    private readonly Option<string> appCode = new("--app-code");
    private readonly Option<TimeSpan> duration = new("--duration");
    private readonly Option<List<string>> property = new("--property");
    private readonly Option<bool> useHw = new("--use-hw");
    private readonly Argument<string> name = new();

    public ConfigUpdateCommand() : base(
        name: "update",
        description: "Update a saved config"
    )
    {
        privateKey.IsRequired = true;
        privateKey.AddAlias("-k");
        publicKey.IsRequired = true;
        publicKey.AddAlias("-pk");
        appCode.IsRequired = true;
        appCode.AddAlias("-c");
        duration.IsRequired = true;
        duration.AddAlias("-d");
        useHw.SetDefaultValue(false);
        property.AddAlias("-p");

        this.AddOption(privateKey);
        this.AddOption(publicKey);
        this.AddOption(appCode);
        this.AddOption(duration);
        this.AddOption(useHw);
        this.AddOption(property);
        this.AddArgument(name);
        this.SetHandler(HandleAsync, name, privateKey, publicKey, appCode, duration, useHw, property);
    }

    public async Task HandleAsync(string name, string privateKey, string publicKey, string appCode, TimeSpan duration, bool useHw, List<string> properties)
    {
        using var ctx = Db.GetContext();
        var configs = new LicenseConfigsRepository(ctx);

        var config = new LicenseConfig
        {
            Name = name,
            PrivateKey = privateKey,
            PublicKey = publicKey,
            AppCode = appCode,
            Duration = duration,
            MatchHardware = useHw,
            Properties = properties
                .Select(LicenseProperty.Parse)
                .ToList(),
        };

        await configs.UpdateAsync(name, config, default);
    }
}