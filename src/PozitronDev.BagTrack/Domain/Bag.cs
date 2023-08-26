using PozitronDev.SharedKernel.Data;

namespace PozitronDev.BagTrack.Domain;

public class Bag : BaseEntity
{
    public string BagTrackId { get; private set; }
    public string DeviceId { get; private set; }
    public string? IsResponseNeeded { get; private set; }
    public string? JulianDate { get; private set; }

    public Bag(string bagTrackId, string deviceId, string? isResponseNeeded, string? julianDate)
    {
        BagTrackId = bagTrackId;
        DeviceId = deviceId;
        IsResponseNeeded = isResponseNeeded;
        JulianDate = julianDate;
    }
}
