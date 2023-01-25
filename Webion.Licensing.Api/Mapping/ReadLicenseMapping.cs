namespace Webion.Licensing.Api.Mappings;

public static class ReadLicenseMapping
{
    public static ReadLicenseCommand ToCommand(this ReadLicenseRequest req, Stream licenseStream) => new()
    {
        LicenseStream = licenseStream,
        AppCode = req.AppCode,
        PublicKey = req.PublicKey,
    };

    public static ReadLicenseResponse ToResponse(this ReadLicenseResult result) => new()
    {
        SerialNumber = result.License.SerialNumber,
        IssueDate = result.License.IssueDate,
        ExpirationDate = result.License.ExpirationDate,
        Properties = result.License.Properties,
    };
}