namespace PozitronDev.BagTrack.Setup;

public class KeyVaultSettings
{
    public const string CONFIG_NAME = "KeyVault";

    public static KeyVaultSettings Instance { get; } = new KeyVaultSettings();
    private KeyVaultSettings() { }

    public virtual string? AzureKeyVault { get; set; }
    public virtual bool DisableAzureKeyVault { get; set; } = false;
}

