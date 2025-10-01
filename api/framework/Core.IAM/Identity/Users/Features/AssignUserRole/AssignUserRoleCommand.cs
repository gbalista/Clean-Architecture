
using Core.IAM.Identity.Users.Dtos;

namespace Core.IAM.Identity.Users.Features.AssignUserRole;
public class AssignUserRoleCommand
{
    public List<UserRoleDetail> UserRoles { get; set; } = new();
}
