using Microsoft.AspNetCore.Identity;

namespace Core.IAM.Identity.Roles.Dtos;
public class ArcRole : IdentityRole
{
    public string? Description { get; set; }

    public ArcRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }
}
