using Webion.Licensing.Commands;
using Webion.Licensing.Storage.Contexts;

using (var ctx = new LicensingDbContext())
{
    await ctx.Database.EnsureCreatedAsync();
}

var rootCommand = new RootCommand();
var configListCommand = new ConfigCommand();
var readCommand = new ReadCommand();
var genCommand = new GenCommand();
var keysCommand = new KeysCommand();

rootCommand.AddCommand(readCommand);
rootCommand.AddCommand(configListCommand);
rootCommand.AddCommand(genCommand);
rootCommand.AddCommand(keysCommand);

return await rootCommand.InvokeAsync(args);