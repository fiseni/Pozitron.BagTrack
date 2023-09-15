namespace PozitronDev.BagTrack.Domain.Contracts;

public interface ICacheReloader<T>
{
    public void ReloadCache(List<T> items);
}
