namespace Webion.Licensing.Storage.Mappings;

internal static class LicenseConfigMapping
{
    public static LicenseConfig ToDomain(this LicenseConfigDbo dbo) => new()
    {
        Name = dbo.Name,
        PrivateKey = dbo.PrivateKey,
        PublicKey = dbo.PublicKey,
        AppCode = dbo.AppCode,
        Duration = dbo.Duration,
        MatchHardware = dbo.MatchHardware,
        Properties = dbo.LicenseProperties
            .Select(p => p.ToDomain())
            .ToList()
    };
}