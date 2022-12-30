namespace Webion.Licensing.Model;

public sealed class LicenseConfig
{
    public required string Name { get; init; }
    public required string PrivateKey { get; init; }
    public required string PublicKey { get; init; }
    public required string AppCode { get; init; }
    public required TimeSpan Duration { get; init; }
    public required Boolean MatchHardware { get; init; }
    public ICollection<LicenseProperty> Properties { get; init; } = new List<LicenseProperty>();
}