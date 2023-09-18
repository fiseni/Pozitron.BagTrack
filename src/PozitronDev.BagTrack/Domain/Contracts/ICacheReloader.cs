namespace PozitronDev.BagTrack.Domain.Contracts;

public interface ICacheReloader
{
    public void ReloadDeviceCache(List<Device> devices);
    public void ReloadAirlineCache(List<Airline> airlines);
}
