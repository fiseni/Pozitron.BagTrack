using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Setup;

public class CurrentUser : ICurrentUser
{
    public string? UserId { get; }

    public string? Username { get; }

    public string? Email { get; }

    public bool IsTestUser { get; } = false;
}
