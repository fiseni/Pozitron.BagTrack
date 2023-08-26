namespace PozitronDev.SharedKernel.Contracts;

public interface IEntity<out TId>
{
    public TId Id { get; }
}
