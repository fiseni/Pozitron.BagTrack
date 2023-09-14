namespace PozitronDev.SharedKernel.Data;

public class AuditableEntity : IAuditableEntity
{
    public DateTime? AuditCreatedAt { get; set; }
    public string? AuditCreatedBy { get; set; }
    public DateTime? AuditModifiedAt { get; set; }
    public string? AuditModifiedBy { get; set; }

    public void UpdateCreateInfo(DateTime now, ICurrentUser currentUser)
    {
        AuditCreatedAt = now;
        AuditCreatedBy = currentUser?.UserId;
    }

    public void UpdateModifyInfo(DateTime now, ICurrentUser currentUser)
    {
        AuditModifiedAt = now;
        AuditModifiedBy = currentUser?.UserId;
    }
}
