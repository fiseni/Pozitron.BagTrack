namespace PozitronDev.BagTrack.Api.Bags;

public static class BagMapper
{
    public static BagDto MapToBagDto(this Bag bag)
    {
        var bagDto = new BagDto
        {
            BagTrackId = bag.BagTrackId,
            DeviceId = bag.DeviceId,
            Date = bag.Date,
            Carousel = bag.Carousel,
            Flight = bag.Flight,
            Airline = bag.Airline,
        };

        return bagDto;
    }
}
