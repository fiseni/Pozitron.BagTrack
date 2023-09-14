using PozitronDev.SharedKernel.Data;

namespace PozitronDev.BagTrack.Domain;

public class Bag : BaseEntity
{
    public string BagTrackId { get; private set; } = default!;
    public string DeviceId { get; private set; } = default!;
    public bool IsResponseNeeded { get; private set; }
    public string? JulianDate { get; private set; }

    private Bag() { }
    public Bag(string bagTrackId, string deviceId, string? isResponseNeeded, string? julianDate)
    {
        BagTrackId = bagTrackId;
        DeviceId = deviceId;
        IsResponseNeeded = isResponseNeeded is not null && isResponseNeeded.Equals("y", StringComparison.OrdinalIgnoreCase);
        JulianDate = julianDate;
    }
}
