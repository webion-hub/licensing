using System.ComponentModel.DataAnnotations;

namespace Webion.Licensing.Contracts.Requests;

public sealed class ReadLicenseRequest
{
    [Required] public string PublicKey { get; init; } = null!;
    [Required] public string AppCode { get; init; } = null!;
}