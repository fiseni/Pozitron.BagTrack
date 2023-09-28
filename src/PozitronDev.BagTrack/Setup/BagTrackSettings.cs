using System.ComponentModel.DataAnnotations;

namespace PozitronDev.BagTrack.Setup;

public class BagTrackSettings
{
    public const string SECTION_NAME = "BagTrack";

    [Required]
    public string ConnectionString { get; set; } = default!;

    public string ApiKey { get; set; } = default!;
}
