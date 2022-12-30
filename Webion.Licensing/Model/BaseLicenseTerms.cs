namespace Webion.Licensing.Model;

public class BaseLicenseTerms
{
    public required string SerialNumber { get; init; }
    public required DateTime IssueDate { get; init; }
    public required DateTime ExpirationDate { get; init; }
    public IDictionary<string, string> Properties { get; init; } = new Dictionary<string, string>();
}