namespace PozitronDev.SharedKernel.Contracts;

public interface IDateTime
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
    DateTime Today { get; }
}
