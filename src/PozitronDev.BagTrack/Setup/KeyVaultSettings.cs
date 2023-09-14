namespace PozitronDev.BagTrack.Setup;

public class KeyVaultSettings
{
    public const string CONFIG_NAME = "KeyVault";

    public static KeyVaultSettings Instance { get; } = new KeyVaultSettings();
    private KeyVaultSettings() { }

    public string? AzureKeyVault { get; set; }
    public bool DisableAzureKeyVault { get; set; } = false;
}

