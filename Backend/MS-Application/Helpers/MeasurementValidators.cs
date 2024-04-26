using FluentValidation;
using Shared;

namespace MS_Application.Helpers;

public class MeasurementValidators : AbstractValidator<Measurement>
{
    public MeasurementValidators()
    {
        CascadeMode = CascadeMode.Stop;
            
        RuleFor(m => m.PatientSSN)
            .NotNull().WithMessage("SSN is null")
            .NotEmpty().WithMessage("SSN is required")
            .MaximumLength(10).WithMessage("SSN may not be longer than 10 numbers")
            .Matches(@"^\d+$").WithMessage("SSN may only contain numbers");
        
        RuleFor(m => m.Systolic)
            .NotNull().WithMessage("Systolic is null")
            .GreaterThan(0).WithMessage("Systolic must be greater than 0");
        
        RuleFor(m => m.Diastolic)
            .NotNull().WithMessage("Diastolic is null")
            .GreaterThan(0).WithMessage("Diastolic must be greater than 0");
    }
}