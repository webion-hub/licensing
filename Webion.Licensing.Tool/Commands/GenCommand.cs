namespace Webion.Licensing.Tool.Commands;

internal sealed class GenCommand : Command
{
    private readonly Option<FileInfo> output = new("--out");
    private readonly Option<string> privateKey = new("--private-key");
    private readonly Option<string> appCode = new("--app-code");
    private readonly Option<TimeSpan> duration = new("--duration");
    private readonly Option<List<string>> property = new("--property");
    private readonly Option<bool> useHw = new("--use-hw");
    private readonly Option<string> config = new("--config");

    public GenCommand() : base(
        name: "gen",
        description: "gen the license with options"
    )
    {
        output.IsRequired = true;
        output.AddAlias("-o");
        privateKey.IsRequired = true;
        privateKey.AddAlias("-k");
        appCode.IsRequired = true;
        appCode.AddAlias("-c");
        duration.IsRequired = true;
        duration.AddAlias("-d");
        useHw.SetDefaultValue(false);
        property.AddAlias("-p");

        this.AddOption(output);
        this.AddOption(privateKey);
        this.AddOption(appCode);
        this.AddOption(duration);
        this.AddOption(useHw);
        this.AddOption(property);
        this.SetHandler(Handle, output, privateKey, appCode, duration, useHw, property);
    }

    public void Handle(FileInfo output, string privateKey, string appCode, TimeSpan duration, bool useHw, List<string> properties)
    {
        using var stream = output.OpenWrite();
        using var writer = new StreamWriter(stream);

        var license = License.Generate(
            privateKey: privateKey,
            appCode: appCode,
            duration: duration,
            matchHardware: useHw,
            properties: properties.Select(LicenseProperty.Parse)
        );

        writer.WriteLine(license);
        Console.WriteLine(license);
    }
}