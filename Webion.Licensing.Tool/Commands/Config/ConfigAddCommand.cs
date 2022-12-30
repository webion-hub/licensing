namespace Webion.Licensing.Commands;

internal sealed class ConfigAddCommand : Command
{
    private readonly Option<string> privateKey = new("--private-key");
    private readonly Option<string> publicKey = new("--public-key");
    private readonly Option<string> appCode = new("--app-code");
    private readonly Option<TimeSpan> duration = new("--duration");
    private readonly Option<List<string>> property = new("--property");
    private readonly Option<bool> useHw = new("--use-hw");
    private readonly Argument<string> name = new();

    public ConfigAddCommand() : base(
        name: "add",
        description: "add a new saved config"
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
        using var configs = new LicenseConfigsRepository();
        var config = new Model.LicenseConfig
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

        await configs.AddAsync(config, default);
    }
}