using PozitronDev.SharedKernel.Data;
using System.ComponentModel.DataAnnotations;

namespace PozitronDev.BagTrack.Setup;

public class BagTrackSettings
{
    public const string CONFIG_NAME = "BagTrack";

    public static BagTrackSettings Instance { get; } = new BagTrackSettings();
    private BagTrackSettings() { }

    [Required]
    public string ConnectionString { get; set; } = default!;
}
