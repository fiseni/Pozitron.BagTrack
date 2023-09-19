namespace PozitronDev.BagTrack.Domain.Bags;

public class Flight : BaseEntity
{
    public string AirlineIATA { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public string NumberIATA { get; private set; } = default!;
    public DateTime Date { get; private set; }
    public string? ActiveCarousel { get; private set; }
    public string? AllocatedCarousel { get; private set; }
    public DateTime? Start { get; private set; }
    public DateTime? Stop { get; private set; }

    private Flight() { }
    public Flight(string airlineIATA, string number, DateTime date, string? carousel, DateTime? start, DateTime? stop)
    {
        AirlineIATA = airlineIATA;
        Number = number;
        NumberIATA = $"{airlineIATA.Trim()}{Number.Trim()}";
        Date = date;
        ActiveCarousel = carousel;
        AllocatedCarousel = carousel;
        Start = start;
        Stop = stop;
    }

    public void UpdateCarousel(string? carousel, DateTime? start, DateTime? stop)
    {
        ActiveCarousel = carousel;

        if (carousel is not null)
        {
            AllocatedCarousel = carousel;
        }

        Start = start;
        Stop = stop;
    }
}
