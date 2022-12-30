namespace Webion.Licensing.Commands;

internal sealed class ConfigCommand : Command
{
    public ConfigCommand() : base(
        name: "config",
        description: "Handle license configurations"
    )
    {
        var listCommand = new ConfigListCommand();
        this.AddCommand(listCommand);
        this.AddCommand(new ConfigAddCommand());
        this.AddCommand(new ConfigDeleteCommand());
        this.AddCommand(new ConfigUpdateCommand());
        this.AddCommand(new ConfigGenCommand());
        this.AddCommand(new ConfigReadCommand());
    }
}