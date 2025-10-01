using FluentValidation;

namespace Core.IAM.Tenant.Features.UpgradeSubscription;
public class UpgradeSubscriptionValidator : AbstractValidator<UpgradeSubscriptionCommand>
{
    public UpgradeSubscriptionValidator()
    {
        RuleFor(t => t.Tenant).NotEmpty();
        RuleFor(t => t.ExtendedExpiryDate).GreaterThan(DateTime.UtcNow);
    }
}
