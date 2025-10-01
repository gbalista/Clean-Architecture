using FluentValidation;

namespace Clinical.Application.MedicalRecords.Create.v1;
public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
{
    public CreateMedicalRecordCommandValidator()
    {
        RuleFor(p => p.Title).NotEmpty().MinimumLength(2).MaximumLength(75);        
    }
}
