namespace PozitronDev.BagTrack.Domain.Bags;

public class Airline : IAggregateRoot
{
    public int Id { get; private set; }
    public string IATA { get; private set; } = default!;
    public string BagCode { get; private set; } = default!;

    private Airline() { }
    public Airline(string iata, string bagCode)
    {
        IATA = iata;
        BagCode = bagCode;
    }
}
