using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

public class BaggageClaimMessageHandler : IMessageHandler
{
    public async Task<bool> HandleAsync(BagTrackDbContext dbContext, string data, CancellationToken cancellationToken)
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
            var newFlight = new Flight(
                flightDto.Airline!, 
                flightDto.FlightNumber!, 
                flightDto.OriginDate!.Value, 
                flightDto.Carousel, 
                flightDto.Start, 
                flightDto.Stop,
                flightDto.FirstBag,
                flightDto.LastBag,
                flightDto.Agent
            );

            dbContext.Flights.Add(newFlight);
        }
        else
        {
            flight.UpdateCarousel(flightDto.Carousel, flightDto.Start, flightDto.Stop, flightDto.FirstBag, flightDto.LastBag);
        }

        return true;
    }

    private static FlightDto ExtractData(string xmlString)
    {
        var document = XDocument.Parse(xmlString);

        var flightDto = new FlightDto();

        var flightLeg = document.Root?.Element(XmlDoc.FlightLeg);
        if (flightLeg is null) return flightDto;

        // LegIdentifier section
        var legIdentifier = flightLeg?.Element(XmlDoc.LegIdentifier);
        flightDto.Airline = legIdentifier?.Element(XmlDoc.Airline)?.Value;
        flightDto.FlightNumber = legIdentifier?.Element(XmlDoc.FlightNumber)?.Value;
        if (DateTime.TryParse(legIdentifier?.Element(XmlDoc.OriginDate)?.Value, out var date))
        {
            flightDto.OriginDate = date;
        }

        // LegData section
        var legData = flightLeg?.Element(XmlDoc.LegData);
        flightDto.Agent = legData?.Element(XmlDoc.AircraftInfo)?.Element(XmlDoc.AgentInfo)?.Value;
        var firstBag = legData?.Elements(XmlDoc.OperationTime)?.FirstOrDefault(x => x.Attribute("OperationQualifier")?.Value == "FBG")?.Value;
        var lastBag = legData?.Elements(XmlDoc.OperationTime)?.FirstOrDefault(x=> x.Attribute("OperationQualifier")?.Value == "LBG")?.Value;
        if (DateTimeOffset.TryParse(firstBag, out var firstBagDate))
        {
            flightDto.FirstBag = firstBagDate.UtcDateTime;
        }
        if (DateTimeOffset.TryParse(lastBag, out var lastBagDate))
        {
            flightDto.LastBag = lastBagDate.UtcDateTime;
        }

        // TPA_Extension section
        var resourceAllocations = flightLeg?.Element(XmlDoc.TPA_Extension)?.Element(XmlDoc.ResourceAllocations);
        if (resourceAllocations is null) return flightDto;
        foreach (var resourceAllocation in resourceAllocations.Elements(XmlDoc.ResourceAllocation))
        {
            var resource = resourceAllocation?.Element(XmlDoc.Resource);

            if (resource is null) continue;

            var resourceType = resource?.Element(XmlDoc.ResourceType)?.Value;

            if (resourceType is not null && resourceType.Equals("BAGGAGECLAIM"))
            {
                flightDto.Carousel = resource?.Element(XmlDoc.Code)?.Value;

                var timeSlot = resourceAllocation?.Element(XmlDoc.TimeSlot);

                if (timeSlot is null) break;

                if (DateTimeOffset.TryParse(timeSlot?.Element(XmlDoc.Start)?.Value, out var start))
                {
                    flightDto.Start = start.UtcDateTime;
                }
                if (DateTimeOffset.TryParse(timeSlot?.Element(XmlDoc.Stop)?.Value, out var stop))
                {
                    flightDto.Stop = stop.UtcDateTime;
                }

                break;
            }
        }

        return flightDto;
    }

    private static class XmlDoc
    {
        public static readonly XNamespace Aidx = "http://www.iata.org/IATA/2007/00";
        public static readonly XNamespace Aip = "http://www.sita.aero/aip/XMLSchema";

        public static readonly XName FlightLeg = Aidx + "FlightLeg";
        public static readonly XName LegIdentifier = Aidx + "LegIdentifier";
        public static readonly XName LegData = Aidx + "LegData";
        public static readonly XName Airline = Aidx + "Airline";
        public static readonly XName FlightNumber = Aidx + "FlightNumber";
        public static readonly XName OriginDate = Aidx + "OriginDate";
        public static readonly XName OperationTime = Aidx + "OperationTime";
        public static readonly XName AircraftInfo = Aidx + "AircraftInfo";
        public static readonly XName AgentInfo = Aidx + "AgentInfo";
        public static readonly XName TPA_Extension = Aidx + "TPA_Extension";
        public static readonly XName ResourceAllocations = Aip + "ResourceAllocations";
        public static readonly XName ResourceAllocation = Aip + "ResourceAllocation";
        public static readonly XName Resource = Aip + "Resource";
        public static readonly XName ResourceType = Aip + "ResourceType";
        public static readonly XName Code = Aip + "Code";
        public static readonly XName TimeSlot = Aip + "TimeSlot";
        public static readonly XName Start = Aip + "Start";
        public static readonly XName Stop = Aip + "Stop";
    }

    public record FlightDto
    {
        public string? Airline { get; set; }
        public string? FlightNumber { get; set; }
        public DateTime? OriginDate { get; set; }
        public string? Carousel { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Stop { get; set; }
        public DateTime? FirstBag { get; set; }
        public DateTime? LastBag { get; set; }
        public string? Agent { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Airline) &&
                !string.IsNullOrEmpty(FlightNumber) &&
                OriginDate.HasValue;
        }
    }
}
