using FluentValidation;

namespace PozitronDev.Extensions.Validations;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Must not be empty!");
    }
}

public class GuidListValidator : AbstractValidator<List<Guid>>
{
    public GuidListValidator()
    {
        RuleForEach(self => self).SetValidator(new GuidValidator());
    }
}

