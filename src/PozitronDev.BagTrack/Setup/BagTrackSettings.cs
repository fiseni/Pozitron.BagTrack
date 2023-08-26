using PozitronDev.SharedKernel.Data;

namespace PozitronDev.BagTrack.Setup;

public class BagTrackSettings
{
    public const string CONFIG_NAME = "BagTrack";

    public static BagTrackSettings Instance { get; } = new BagTrackSettings();
    private BagTrackSettings() { }

    public string? ConnectionString { get; set; }
    public string? DbSecretName { get; set; }

    public DbConnectionInfo GetDbConnectionInfo(IConfiguration configuration)
    {
        var connectionStringFromAzureKeyVault = !string.IsNullOrWhiteSpace(DbSecretName) ? configuration.GetSection(DbSecretName).Get<string>() : string.Empty;
        var disableAzureKeyVault = configuration.GetSection($"{KeyVaultSettings.CONFIG_NAME}:{nameof(KeyVaultSettings.DisableAzureKeyVault)}").Get<bool>();

        return new DbConnectionInfo(ConnectionString, connectionStringFromAzureKeyVault, !disableAzureKeyVault);
    }
}
