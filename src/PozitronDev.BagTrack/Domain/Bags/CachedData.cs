using System.Collections.Concurrent;

namespace PozitronDev.BagTrack.Domain.Bags;

public class CachedData : IDataCache, ICacheReloader
{
    private readonly ConcurrentDictionary<string, string> _devices = new();
    private readonly ConcurrentDictionary<string, string> _airlines = new();

    public void ReloadDeviceCache(List<Device> devices)
    {
        _devices.Clear();

        foreach (var device in devices)
        {
            _devices.TryAdd(device.Id, device.Carousel);
        }
    }

    public void ReloadAirlineCache(List<Airline> airlines)
    {
        _airlines.Clear();

        foreach (var airline in airlines)
        {
            _airlines.TryAdd(airline.BagCode, airline.IATA);
        }
    }

    public string? GetCarousel(string deviceId)
    {
        var carousel = _devices.GetValueOrDefault(deviceId);
        return carousel;
    }

    public string? GetAirlineIATA(string airlineBagCode)
    {
        var iata = _airlines.GetValueOrDefault(airlineBagCode);
        return iata;
    }
}
