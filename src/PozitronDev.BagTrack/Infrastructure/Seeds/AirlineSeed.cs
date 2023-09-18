namespace PozitronDev.BagTrack.Infrastructure.Seeds;

public class AirlineSeed
{
    public static List<Airline> Get()
    {
        var airlines = new List<Airline>()
        {
            new("TK", "0235"),
        };

        return airlines;
    }
}
