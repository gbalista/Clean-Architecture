using System.Collections.ObjectModel;

namespace Core.IAM.Identity.Permissions;

public static class ArcPermissions
{
    private static readonly List<ArcPermission> AllPermissions = new()
    {
        // tenants
        new("View Tenants", ArcActions.View, ArcResources.Tenants, IsRoot: true),
        new("Create Tenants", ArcActions.Create, ArcResources.Tenants, IsRoot: true),
        new("Update Tenants", ArcActions.Update, ArcResources.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", ArcActions.UpgradeSubscription, ArcResources.Tenants, IsRoot: true),

        // identity
        new("View Users", ArcActions.View, ArcResources.Users),
        new("Search Users", ArcActions.Search, ArcResources.Users),
        new("Create Users", ArcActions.Create, ArcResources.Users),
        new("Update Users", ArcActions.Update, ArcResources.Users),
        new("Delete Users", ArcActions.Delete, ArcResources.Users),
        new("Export Users", ArcActions.Export, ArcResources.Users),
        new("View UserRoles", ArcActions.View, ArcResources.UserRoles),
        new("Update UserRoles", ArcActions.Update, ArcResources.UserRoles),
        new("View Roles", ArcActions.View, ArcResources.Roles),
        new("Create Roles", ArcActions.Create, ArcResources.Roles),
        new("Update Roles", ArcActions.Update, ArcResources.Roles),
        new("Delete Roles", ArcActions.Delete, ArcResources.Roles),
        new("View RoleClaims", ArcActions.View, ArcResources.RoleClaims),
        new("Update RoleClaims", ArcActions.Update, ArcResources.RoleClaims),

        new("View Hangfire", ArcActions.View, ArcResources.Hangfire),
        new("View Dashboard", ArcActions.View, ArcResources.Dashboard),

        // audit
        new("View Audit Trails", ArcActions.View, ArcResources.AuditTrails),
    };

    public static IReadOnlyList<ArcPermission> All => AllPermissions.AsReadOnly();
    public static IReadOnlyList<ArcPermission> Root => AllPermissions.Where(p => p.IsRoot).ToList().AsReadOnly();
    public static IReadOnlyList<ArcPermission> Admin => AllPermissions.Where(p => !p.IsRoot).ToList().AsReadOnly();
    public static IReadOnlyList<ArcPermission> Basic => AllPermissions.Where(p => p.IsBasic).ToList().AsReadOnly();

    // 🔹 Método para adicionar permissões dinamicamente
    public static void AddIfNotExists(params ArcPermission[] permissions)
    {
        foreach (var permission in permissions)
        {
            if (!AllPermissions.Any(p => p.Name == permission.Name))
            {
                AllPermissions.Add(permission);
            }
        }
    }
}

public record ArcPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource)
    {
        return $"Permissions.{resource}.{action}";
    }
}




