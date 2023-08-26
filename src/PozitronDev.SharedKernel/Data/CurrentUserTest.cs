namespace PozitronDev.SharedKernel.Data;

public class CurrentUserTest : ICurrentUser
{
    public string? UserId { get; } = "111";
    public string? Username { get; } = "TestUser";
    public string? Email { get; } = "test@local.com";
    public bool IsTestUser { get; } = true;
}
