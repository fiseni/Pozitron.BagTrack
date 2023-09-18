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
    public Bag(IDateTime dateTime,
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

        SetDate(dateTime, julianDate);
    }

    private void SetDate(IDateTime dateTime, string? julianDate)
    {
        if (int.TryParse(julianDate, out var julianDateInt))
        {
            var dt = new DateTime(dateTime.UtcNow.Year, 1, 1);

            if (julianDateInt > 1)
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
