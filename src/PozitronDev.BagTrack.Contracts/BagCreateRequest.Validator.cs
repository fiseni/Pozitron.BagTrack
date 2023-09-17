using FluentValidation;

namespace PozitronDev.BagTrack.Contracts;

public class BagCreateRequestValidator : AbstractValidator<BagCreateRequest>
{
    public BagCreateRequestValidator()
    {
        RuleFor(x => x.BagTagId).NotEmpty().Length(10);
        RuleFor(x => x.DeviceId).NotEmpty().MaximumLength(6);
        RuleFor(x => x.IsResponseNeeded).MaximumLength(1).When(x => x.IsResponseNeeded is not null);
        //RuleFor(x => x.JulianDate).MaximumLength(3).When(x => x.JulianDate is not null);
    }
}
