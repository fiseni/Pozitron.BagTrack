using PozitronDev.BagTrack.Domain.Contracts;
using PozitronDev.SharedKernel.Contracts;
using PozitronDev.SharedKernel.Data;

namespace PozitronDev.BagTrack.Domain;

public class Bag : BaseEntity, IAggregateRoot
{
    public string BagTrackId { get; private set; } = default!;
    public string DeviceId { get; private set; } = default!;
    public string? Carousel { get; private set; }
    public string? Flight { get; private set; }
    public string? Airline { get; private set; }
    public string? JulianDate { get; private set; }
    public DateTime Date { get; private set; }
    public bool IsResponseNeeded { get; private set; }

    private Bag() { }
    public Bag(IDateTime dateTime, IDeviceCache deviceCache, string bagTrackId, string deviceId, string? isResponseNeeded, string? julianDate)
    {
        BagTrackId = bagTrackId;
        DeviceId = deviceId;
        Carousel = deviceCache.GetCarousel(deviceId);

        IsResponseNeeded = isResponseNeeded is not null && isResponseNeeded.Equals("y", StringComparison.OrdinalIgnoreCase);

        SetDate(dateTime, julianDate);
    }

    private void SetDate(IDateTime dateTime, string? julianDate)
    {
        if (int.TryParse(julianDate, out var julianDateInt))
        {
            var dt = new DateTime(dateTime.UtcNow.Year, 1, 1);

            if (julianDateInt != 0)
            {
                dt = dt.AddDays(julianDateInt - 1);
            }

            JulianDate = julianDateInt.ToString();
            Date = dt;
        }
        else
        {
            JulianDate = null;
            Date = dateTime.UtcNow;
        }
    }
}
