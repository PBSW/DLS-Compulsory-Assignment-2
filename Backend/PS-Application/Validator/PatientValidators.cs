using FluentValidation;
using Shared;

namespace PS_Application.Validator;

public class PatientValidators : AbstractValidator<Patient>
{
    public PatientValidators()
    {
        CascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Ssn)
            .NotNull().WithMessage("SSN is null")
            .NotEmpty().WithMessage("SSN is required")
            .MaximumLength(10).WithMessage("SSN may not be longer than 10 numbers")
            .Matches(@"^\d+$").WithMessage("SSN may only contain numbers");

        RuleFor(x => x.Mail)
            .NotNull().WithMessage("Mail is null")
            .NotEmpty().WithMessage("Mail is required")
            .MaximumLength(128).WithMessage("Last name cannot be longer than 128 characters")
            .EmailAddress().WithMessage("Incorrect Mail formatting");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is null")
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(128).WithMessage("Name cannot be longer than 128 characters");
    }
}