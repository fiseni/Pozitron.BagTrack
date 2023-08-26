namespace PozitronDev.SharedKernel.Data;

public class DbConnectionInfo
{
    public bool IsKeyVaultEnabled { get; }
    public string ConnectionString { get; }

    public DbConnectionInfo(string? localConnectionString, string? azureKVConnectionString, bool isKeyVaultEnabled)
    {
        IsKeyVaultEnabled = isKeyVaultEnabled;

        if (!string.IsNullOrEmpty(localConnectionString))
        {
            ConnectionString = localConnectionString;
        }
        else if (IsKeyVaultEnabled && !string.IsNullOrEmpty(azureKVConnectionString))
        {
            ConnectionString = azureKVConnectionString;
        }
        else
        {
            throw new ArgumentException("No Connection String provided");
        }
    }
}
