using System.ComponentModel.DataAnnotations.Schema;

namespace Webion.Licensing.Storage.Data;

public sealed class LicensePropertyDbo
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string LicenseConfigName { get; set; } = null!;
    public LicenseConfigDbo LicenseConfig { get; set; } = null!;

    [Required] public required string Key { get; set; }
    [Required] public required string Value { get; set; }
}