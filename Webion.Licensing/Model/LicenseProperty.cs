namespace Webion.Licensing.Model;

public sealed class LicenseProperty
{
    public required string Key { get; init; }
    public required string Value { get; init; }

    public static LicenseProperty Parse(string property)
    {
        var splitValues = property.Split(':', StringSplitOptions.RemoveEmptyEntries);
        return new LicenseProperty
        {
            Key = splitValues[0],
            Value = splitValues[1],
        };
    }
}