using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Setup;

public class CurrentUser : ICurrentUser
{
    public string? UserId { get; } = null;

    public string? Username { get; } = null;

    public string? Email { get; } = null;

    public bool IsTestUser { get; } = false;
}
