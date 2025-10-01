using Microsoft.AspNetCore.Identity;

namespace Core.IAM.Identity.RoleClaims;
public class ArcRoleClaim : IdentityRoleClaim<string>
{
    public string? CreatedBy { get; init; }
    public DateTimeOffset CreatedOn { get; init; }
}
