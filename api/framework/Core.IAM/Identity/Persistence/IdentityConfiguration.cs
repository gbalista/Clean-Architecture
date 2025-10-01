using Finbuckle.MultiTenant;
using Core.IAM.Identity.RoleClaims;
using Core.IAM.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.IAM.Identity.Roles.Dtos;
using Core.IAM.Identity.Audit.Dtos;

namespace Core.IAM.Identity.Persistence;

public class AuditTrailConfig : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder
            .ToTable("AuditTrails", IdentityConstants.SchemaName)
            .IsMultiTenant();

        builder.HasKey(a => a.Id);
    }
}

public class ApplicationUserConfig : IEntityTypeConfiguration<ArcUser>
{
    public void Configure(EntityTypeBuilder<ArcUser> builder)
    {
        builder
            .ToTable("Users", IdentityConstants.SchemaName)
            .IsMultiTenant();

        builder
            .Property(u => u.ObjectId)
                .HasMaxLength(256);
    }
}

public class ApplicationRoleConfig : IEntityTypeConfiguration<ArcRole>
{
    public void Configure(EntityTypeBuilder<ArcRole> builder) =>
        builder
            .ToTable("Roles", IdentityConstants.SchemaName)
            .IsMultiTenant()
                .AdjustUniqueIndexes();
}

public class ApplicationRoleClaimConfig : IEntityTypeConfiguration<ArcRoleClaim>
{
    public void Configure(EntityTypeBuilder<ArcRoleClaim> builder) =>
        builder
            .ToTable("RoleClaims", IdentityConstants.SchemaName)
            .IsMultiTenant();
}

public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) =>
        builder
            .ToTable("UserRoles", IdentityConstants.SchemaName)
            .IsMultiTenant();
}

public class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) =>
        builder
            .ToTable("UserClaims", IdentityConstants.SchemaName)
            .IsMultiTenant();
}

public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) =>
        builder
            .ToTable("UserLogins", IdentityConstants.SchemaName)
            .IsMultiTenant();
}

public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) =>
        builder
            .ToTable("UserTokens", IdentityConstants.SchemaName)
            .IsMultiTenant();
}
