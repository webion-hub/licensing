namespace Webion.Licensing.Extensions;

internal static class BuilderPropertiesExtension
{
    public static IBuilder_Properties WithProperties(this IBuilder_Properties signer,
        IEnumerable<LicenseProperty> properties
    )
    {
        return properties.Aggregate(signer, (lic, p) =>
            lic.WithProperty(p.Key, p.Value)
        );
    }
}