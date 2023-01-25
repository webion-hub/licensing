namespace Webion.Licensing.Storage.Contexts;

public sealed class LicensingDbContext : DbContext
{
    public LicensingDbContext(DbContextOptions<LicensingDbContext> options)
        : base(options)
    {}

    public DbSet<LicenseConfigDbo> LicenseConfigs { get; set; } = null!;
    public DbSet<LicensePropertyDbo> LicenseProperties { get; set; } = null!;
}