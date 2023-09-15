namespace PozitronDev.BagTrack.Infrastructure;

public interface ICacheReloader<T>
{
    public void ReloadCache(List<T> items);
}
