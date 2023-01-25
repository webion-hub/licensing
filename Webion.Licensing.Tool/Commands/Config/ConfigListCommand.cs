using System.Text.Json;
using System.Text.Json.Serialization;
using Humanizer;

namespace Webion.Licensing.Tool.Config.Commands;

internal sealed class ConfigListCommand : Command
{
    private readonly Option<bool> verbose = new("--verbose");

    public ConfigListCommand() : base(
        name: "list",
        description: "List all saved configs"
    )
    {
        AddOption(verbose);
        this.SetHandler(HandleAsync, verbose);
    }

    public async Task HandleAsync(bool verbose)
    {
        using var ctx = Db.GetContext();
        var configs = new LicenseConfigsRepository(ctx);
        var saved = await configs.ListAllAsync(default);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        foreach (var s in saved)
        {
            var value = new
            {
                s.Name,
                s.AppCode,
                Duration = s.Duration.Humanize(),
                s.MatchHardware,
                s.Properties,
                PublicKey = Format(s.PublicKey, verbose),
                PrivateKey = Format(s.PrivateKey, verbose),
            };

            Console.WriteLine(JsonSerializer.Serialize(value, options));
        }
    }

    
    private static string Format(string s, bool verbose)
    {
        return verbose
            ? s
            : s[..32] + "...";
    }
}