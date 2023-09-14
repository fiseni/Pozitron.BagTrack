namespace PozitronDev.SharedKernel.Contracts;

public interface IAuditableEntity
{
    DateTime? AuditCreatedAt { get; set; }
    string? AuditCreatedBy { get; set; }
    DateTime? AuditModifiedAt { get; set; }
    string? AuditModifiedBy { get; set; }

    void UpdateCreateInfo(DateTime now, ICurrentUser currentUser);
    void UpdateModifyInfo(DateTime now, ICurrentUser currentUser);
}
