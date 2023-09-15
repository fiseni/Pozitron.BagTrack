namespace PozitronDev.BagTrack.Infrastructure.Seeds;

public class DeviceSeed
{
    public static List<Device> Get()
    {
        var devices = new List<Device>()
        {
            new("100103", "25B"),
            new("100107", "25A"),
            new("100203", "24B"),
            new("100207", "24A"),
            new("100213", "23A"),
            new("100219", "23B"),
            new("100303", "21A"),
            new("100309", "21B"),
            new("100315", "20B"),
            new("100321", "20A"),
            new("100403", "18B"),
            new("100409", "18A"),
            new("100415", "17B"),
            new("100419", "17A"),
            new("100503", "16B"),
            new("100507", "16A"),
            new("100513", "15"),
            new("100601", "12"),
            new("100607", "11B"),
            new("100611", "11A"),
            new("100701", "10B"),
            new("100705", "10A"),
            new("100711", "9B"),
            new("100715", "9A"),
            new("100801", "8B"),
            new("100805", "8A"),
            new("100901", "6B"),
            new("100907", "6A"),
            new("100913", "5B"),
            new("100919", "5A"),
            new("101001", "4B"),
            new("101007", "4A"),
            new("101101", "2B"),
            new("101107", "2A"),
            new("101113", "1B"),
            new("101119", "1A"),
        };

        return devices;
    }
}
