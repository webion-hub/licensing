namespace Webion.Licensing;

public static class License
{
    public static string Generate(
        string privateKey,
        string appCode,
        TimeSpan duration,
        bool matchHardware,
        IEnumerable<LicenseProperty> properties
    )
    {
        return Generate(new LicenseConfig
        {
            Name = string.Empty,
            PublicKey = string.Empty,
            PrivateKey = privateKey,
            AppCode = appCode,
            Duration = duration,
            MatchHardware = matchHardware,
            Properties = properties.ToList(),
        });
    }

    public static string Generate(LicenseConfig config)
    {
        return Lic.Builder
            .WithRsaPrivateKey(config.PrivateKey)
            .WithoutHardwareIdentifier()
            .WithSerialNumber(SerialNumber.Create(config.AppCode))
            .ExpiresOn(DateTime.UtcNow + config.Duration)
            .WithProperties(config.Properties)
            .SignAndCreate()
            .Serialize();
    }


    public static BaseLicenseTerms? Deserialize(string license, string publicKey, string appCode)
    {
        try
        {
            var result = Lic.Verifier
                .WithRsaPublicKey(publicKey)
                .WithApplicationCode(appCode)
                .LoadAndVerify(license);

            return new BaseLicenseTerms
            {
                SerialNumber = result.SerialNumber,
                ExpirationDate = result.ExpirationDate,
                IssueDate = result.IssueDate,
                Properties = result.Properties
            };
        }
        catch
        {
            return null;
        }
    }
}
