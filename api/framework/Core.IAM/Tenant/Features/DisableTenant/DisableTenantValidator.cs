using FluentValidation;

namespace Core.IAM.Tenant.Features.DisableTenant;
public sealed class DisableTenantValidator : AbstractValidator<DisableTenantCommand>
{
    public DisableTenantValidator() =>
       RuleFor(t => t.TenantId)
           .NotEmpty();
}
