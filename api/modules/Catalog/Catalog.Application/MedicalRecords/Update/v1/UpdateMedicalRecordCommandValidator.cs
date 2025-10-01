using FluentValidation;

namespace Clinical.Application.MedicalRecords.Update.v1;
public class UpdateMedicalRecordCommandValidator : AbstractValidator<UpdateMedicalRecordCommand>
{
    public UpdateMedicalRecordCommandValidator()
    {
        RuleFor(p => p.Title).NotEmpty().MinimumLength(2).MaximumLength(75);        
    }
}
