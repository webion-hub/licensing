namespace Webion.Licensing.Storage.Mappings;

internal static class LicensePropertyMapping
{
    public static LicenseProperty ToDomain(this LicensePropertyDbo dbo) => new()
    {
        Key = dbo.Key,
        Value = dbo.Value,
    };
    
    
    public static LicensePropertyDbo ToDbo(this LicenseProperty dbo) => new()
    {
        Key = dbo.Key,
        Value = dbo.Value,
    };
}