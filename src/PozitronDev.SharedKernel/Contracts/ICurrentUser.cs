namespace PozitronDev.SharedKernel.Contracts;

public interface ICurrentUser
{
    string? UserId { get; }
    string? Username { get; }
    string? Email { get; }
    bool IsTestUser { get; }
}
