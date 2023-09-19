namespace PozitronDev.BagTrack.Domain.Bags;

public class Bag : BaseEntity, IAggregateRoot
{
    public string BagTagId { get; private set; } = default!;
    public string DeviceId { get; private set; } = default!;
    public string? Carousel { get; private set; }
    public string? AirlineIATA { get; private set; }
    public string? Flight { get; private set; }
    public string? JulianDate { get; private set; }
    public DateTime Date { get; private set; }
    public bool IsResponseNeeded { get; private set; }

    private Bag() { }
    public Bag(DateTime utcNow,
               string bagTagId,
               string deviceId,
               string? carousel,
               string? airlineIATA,
               string? flight,
               string? isResponseNeeded,
               string? julianDate)
    {
        BagTagId = bagTagId;
        DeviceId = deviceId;
        Carousel = carousel;
        AirlineIATA = airlineIATA;
        Flight = flight;

        IsResponseNeeded = isResponseNeeded is not null && isResponseNeeded.Equals("y", StringComparison.OrdinalIgnoreCase);

        // We'll always set the actual time, won't consider julianDate
        SetDate(utcNow, null);
    }

    private void SetDate(DateTime utcNow, string? julianDate)
    {
        if (julianDate is null || !int.TryParse(julianDate, out var julianDateInt))
        {
            JulianDate = null;
            Date = utcNow;
        }
        else
        {
            var dt = new DateTime(utcNow.Year, 1, 1);

            if (julianDateInt > 1)
            {
                dt = dt.AddDays(julianDateInt - 1);
            }

            JulianDate = julianDate;
            Date = dt;
        }
    }
}
