namespace Webion.Licensing.Storage.Data;

public sealed class LicenseConfigDbo
{
    [Key]
    [Required]
    public required string Name { get; set; }

    [Required] public required string PrivateKey { get; set; }
    [Required] public required string PublicKey { get; set; }
    [Required] public required string AppCode { get; set; }
    [Required] public required TimeSpan Duration { get; set; }
    [Required] public required Boolean MatchHardware { get; set; }

    public ICollection<LicensePropertyDbo> LicenseProperties { get; set; } = new List<LicensePropertyDbo>();
}