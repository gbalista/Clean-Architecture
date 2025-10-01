using FluentValidation;

namespace Clinical.Application.Patients.Update.v1;
public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    public UpdatePatientCommandValidator()
    {
        RuleFor(b => b.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(b => b.Notes).MaximumLength(1000);
    }
}
