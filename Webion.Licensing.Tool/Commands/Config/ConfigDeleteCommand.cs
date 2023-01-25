namespace Webion.Licensing.Tool.Config.Commands;

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
        using var ctx = Db.GetContext();
        var configs = new LicenseConfigsRepository(ctx);
        await configs.DeleteAsync(
            name: name,
            cancellationToken: default
        );
    }
}