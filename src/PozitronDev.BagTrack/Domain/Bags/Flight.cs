namespace PozitronDev.BagTrack.Domain.Bags;

public class Flight
{
    public Guid Id { get; private set; }
    public string AirlineIATA { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public DateTime OriginDate { get; private set; }
    public string? ActiveCarousel { get; private set; }
    public string? AllocatedCarousel { get; private set; }
    public DateTime? Start { get; private set; }
    public DateTime? Stop { get; private set; }
    public DateTime? FirstBag { get; private set; }
    public DateTime? LastBag { get; private set; }
    public string? Agent { get; private set; }

    private Flight() { }
    public Flight(
        string airlineIATA,
        string number,
        DateTime originDate,
        string? carousel,
        DateTime? start,
        DateTime? stop,
        DateTime? firstBag,
        DateTime? lastBag,
        string? agent)
    {
        AirlineIATA = airlineIATA;
        Number = number;
        OriginDate = originDate;
        ActiveCarousel = carousel;
        AllocatedCarousel = carousel;
        Start = start;
        Stop = stop;
        FirstBag = firstBag;
        LastBag = lastBag;
        Agent = agent;
    }

    public void UpdateCarousel(string? carousel, DateTime? start, DateTime? stop, DateTime? firstBag, DateTime? lastBag)
    {
        ActiveCarousel = carousel;

        if (!string.IsNullOrEmpty(carousel))
        {
            AllocatedCarousel = carousel;
        }

        Start = start;
        Stop = stop;

        FirstBag ??= firstBag;
        LastBag ??= lastBag;
    }
}
