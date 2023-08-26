namespace PozitronDev.SharedKernel.Data;

public class AuditableEntity : IAuditableEntity
{
    public DateTime? AuditCreatedTime { get; set; }
    public string? AuditCreatedByUserId { get; set; }
    public string? AuditCreatedByUsername { get; set; }
    public DateTime? AuditModifiedTime { get; set; }
    public string? AuditModifiedByUserId { get; set; }
    public string? AuditModifiedByUsername { get; set; }

    public void UpdateCreateInfo(DateTime now, ICurrentUser currentUser)
    {
        AuditCreatedTime = now;
        AuditCreatedByUserId = currentUser?.UserId;
        AuditCreatedByUsername = currentUser?.Username;
    }

    public void UpdateModifyInfo(DateTime now, ICurrentUser currentUser)
    {
        AuditModifiedTime = now;
        AuditModifiedByUserId = currentUser?.UserId;
        AuditModifiedByUsername = currentUser?.Username;
    }
}
