namespace PozitronDev.BagTrack.Domain.Bags;

public class Device
{
    public string Id { get; private set; }
    public string Carousel { get; private set; }

    public Device(string id, string carousel)
    {
        Id = id;
        Carousel = carousel;
    }
}
