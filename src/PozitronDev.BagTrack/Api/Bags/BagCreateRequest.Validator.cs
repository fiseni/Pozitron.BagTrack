﻿using FluentValidation;

namespace PozitronDev.BagTrack.Api.Bags;

public class BagCreateDtoValidator : AbstractValidator<BagCreateRequest>
{
    public BagCreateDtoValidator()
    {
        RuleFor(x => x.BagTagId).NotEmpty().MaximumLength(10);
        RuleFor(x => x.DeviceId).NotEmpty().MaximumLength(6);
        RuleFor(x => x.IsResponseNeeded).MaximumLength(1).When(x => x.IsResponseNeeded is not null);
        RuleFor(x => x.JulianDate).MaximumLength(3).When(x => x.JulianDate is not null);
    }
}
