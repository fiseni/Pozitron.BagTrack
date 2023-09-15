namespace PozitronDev.BagTrack.Setup;

public class KeyVaultSettings
{
    public const string SECTION_NAME = "KeyVault";

    public string? AzureKeyVault { get; set; }
    public bool DisableAzureKeyVault { get; set; } = false;
}

