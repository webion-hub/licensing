using Microsoft.EntityFrameworkCore;
using Webion.Licensing.Storage.Contexts;

namespace Webion.Licensing.Tool;

public static class Db
{
    private static DbContextOptions<LicensingDbContext>? _options;

    public static void Configure(Action<DbContextOptionsBuilder<LicensingDbContext>> config)
    {
        var builder = new DbContextOptionsBuilder<LicensingDbContext>();
        config(builder);
        _options = builder.Options;
    }


    public static LicensingDbContext GetContext()
    {
        if (_options is null)
            throw new InvalidOperationException("DbContext options must be set using the Configure() method");

        return new LicensingDbContext(_options);
    }
}