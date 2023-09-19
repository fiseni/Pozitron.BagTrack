using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

public class BaggageClaimMessageHandler : IMessageHandler
{
    public async Task<bool> Handle(BagTrackDbContext dbContext, string data, CancellationToken cancellationToken)
    {
        var flightDto = ExtractData(data);

        if (!flightDto.IsValid()) return false;

        var flight = await dbContext.Flights
            .Where(x => x.AirlineIATA == flightDto.Airline)
            .Where(x => x.Number == flightDto.FlightNumber)
            .Where(x => x.OriginDate == flightDto.OriginDate)
            .FirstOrDefaultAsync();

        if (flight is null)
        {
            var newFlight = new Flight(flightDto.Airline!, flightDto.FlightNumber!, flightDto.OriginDate!.Value, flightDto.Carousel, flightDto.Start, flightDto.Stop);
            dbContext.Flights.Add(newFlight);
        }
        else
        {
            flight.UpdateCarousel(flightDto.Carousel, flightDto.Start, flightDto.Stop);
        }

        return true;
    }

    private static FlightDto ExtractData(string xmlString)
    {
        var document = XDocument.Parse(xmlString);
        XNamespace aidx = "http://www.iata.org/IATA/2007/00";
        XNamespace aip = "http://www.sita.aero/aip/XMLSchema";

        var flightDto = new FlightDto();

        var legIdentifier = document.Root?.Element(aidx + "FlightLeg")?.Element(aidx + "LegIdentifier");
        flightDto.Airline = legIdentifier?.Element(aidx + "Airline")?.Value;
        flightDto.FlightNumber = legIdentifier?.Element(aidx + "FlightNumber")?.Value;

        DateTime.TryParse(legIdentifier?.Element(aidx + "OriginDate")?.Value, out var date);
        flightDto.OriginDate = date;

        var resourceAllocations = document.Root?.Element(aidx + "FlightLeg")?.Element(aidx + "TPA_Extension")?.Element(aip + "ResourceAllocations");

        if (resourceAllocations is null) return flightDto;

        foreach (var resourceAllocation in resourceAllocations.Elements(aip + "ResourceAllocation"))
        {
            var resourceType = resourceAllocation?.Element(aip + "Resource")?.Element(aip + "ResourceType")?.Value;

            if (resourceType == "BAGGAGECLAIM")
            {
                flightDto.Carousel = resourceAllocation?.Element(aip + "Resource")?.Element(aip + "Code")?.Value;

                _ = DateTime.TryParse(resourceAllocation?.Element(aip + "TimeSlot")?.Element(aip + "Start")?.Value, out var start);
                _ = DateTime.TryParse(resourceAllocation?.Element(aip + "TimeSlot")?.Element(aip + "Stop")?.Value, out var stop);
                flightDto.Start = start;
                flightDto.Stop = stop;
            }
            break;
        }

        return flightDto;
    }

    public record FlightDto
    {
        public string? Airline { get; set; }
        public string? FlightNumber { get; set; }
        public DateTime? OriginDate { get; set; }
        public string? Carousel { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Airline) &&
                !string.IsNullOrEmpty(FlightNumber) &&
                OriginDate.HasValue;
        }
    }
}
