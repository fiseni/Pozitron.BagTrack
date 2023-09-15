using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Domain;

public class Device : IAggregateRoot
{
    public string Id { get; private set; }
    public string Carousel { get; private set; }

    public Device(string id, string carousel)
    {
        Id = id;
        Carousel = carousel;
    }
}
