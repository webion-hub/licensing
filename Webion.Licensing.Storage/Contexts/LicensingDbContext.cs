namespace Webion.Licensing.Storage.Contexts;

public sealed class LicensingDbContext : DbContext
{
    public DbSet<LicenseConfigDbo> LicenseConfigs { get; set; } = null!;
    public DbSet<LicensePropertyDbo> LicenseProperties { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var docs = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments
        );

        var path = $"{docs}/wl";
        var conn = $"Filename={path}/db.sqlite";

        Directory.CreateDirectory(path);
        builder.UseSqlite(conn);
    }
}