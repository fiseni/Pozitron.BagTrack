namespace PozitronDev.BagTrack.Domain.Contracts;

public interface IDataCache
{
    public string? GetCarousel(string deviceId);
    public string? GetAirlineIATA(string airlineBagCode);
}
