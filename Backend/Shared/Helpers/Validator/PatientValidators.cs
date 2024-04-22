using FluentValidation;

namespace Shared.Helpers.Validator;

public class PatientValidators : AbstractValidator<Patient>
{
    public PatientValidators()
    {
        RuleFor(x => x.Ssn)
            .NotEmpty().WithMessage("First SSN is required")
            .MaximumLength(10).WithMessage("First name cannot be longer than 10 characters");

        RuleFor(x => x.Mail)
            .NotEmpty().WithMessage("Mail is required")
            .MaximumLength(128).WithMessage("Last name cannot be longer than 128 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(128).WithMessage("Name cannot be longer than 128 characters");
    }
}