namespace PozitronDev.SharedKernel.Contracts;

public interface IAuditableEntity
{
    DateTime? AuditCreatedTime { get; set; }
    string? AuditCreatedByUserId { get; set; }
    string? AuditCreatedByUsername { get; set; }
    DateTime? AuditModifiedTime { get; set; }
    string? AuditModifiedByUserId { get; set; }
    string? AuditModifiedByUsername { get; set; }

    void UpdateCreateInfo(DateTime now, ICurrentUser currentUser);
    void UpdateModifyInfo(DateTime now, ICurrentUser currentUser);
}
