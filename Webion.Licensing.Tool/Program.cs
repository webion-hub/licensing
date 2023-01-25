using Microsoft.EntityFrameworkCore;
using Webion.Licensing.Tool.Commands;

Db.Configure(options =>
{
    var docs = Environment.GetFolderPath(
        Environment.SpecialFolder.MyDocuments
    );

    var path = $"{docs}/wl";
    var conn = $"Filename={path}/db.sqlite";

    Directory.CreateDirectory(path);
    options.UseSqlite(conn);
});


using (var ctx = Db.GetContext())
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