using System.Collections.Concurrent;

namespace PozitronDev.BagTrack.Domain.Bags;

public class DeviceCache : IDeviceCache, ICacheReloader<Device>
{
    private readonly ConcurrentDictionary<string, string> _devices = new();

    public void ReloadCache(List<Device> devices)
    {
        _devices.Clear();

        foreach (var device in devices)
        {
            _devices.TryAdd(device.Id, device.Carousel);
        }
    }

    public string? GetCarousel(string deviceId)
    {
        var carousel = _devices.GetValueOrDefault(deviceId);
        return carousel;
    }
}
