namespace Webion.Licensing.Commands;

internal sealed class ConfigDeleteCommand : Command
{
    private readonly Argument<string> name = new();

    public ConfigDeleteCommand() : base(
        name: "delete",
        description: "delete a saved config"
    )
    {
        this.AddArgument(name);
        this.SetHandler(HandleAsync, name);
    }

    public async Task HandleAsync(string name)
    {
        using var configs = new LicenseConfigsRepository();
        await configs.DeleteAsync(
            name: name,
            cancellationToken: default
        );
    }
}