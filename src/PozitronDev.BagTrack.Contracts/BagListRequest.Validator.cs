using FluentValidation;

namespace PozitronDev.BagTrack.Contracts;

public class BagListRequestValidator : AbstractValidator<BagListRequest>
{
    public BagListRequestValidator()
    {
        RuleFor(x => x.BagTagId).Length(10);
        RuleFor(x => x.Airline).MaximumLength(5);
    }
}
