using Core.IAM.Auth;
using Core.IAM.Identity.Persistence;
using Core.IAM.Identity.Roles.Endpoints;
using Core.IAM.Identity.Tokens.Endpoints;
using Core.IAM.Identity.Users;
using Core.IAM.Identity.Users.Endpoints;
using Core.IAM.Identity.Users.Services;
using Core.DataAccess.Persistence;
using Core.DataAcess.Persistence;
using Core.IAM.Identity.Users.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Core.Persistence.User;
using Core.IAM.Identity.Audit.Services;
using Core.IAM.Identity.Audit.Interceptors;
using Core.IAM.Identity.Roles.Dtos;
using Core.IAM.Identity.Roles.Services;
using Core.IAM.Identity.Tokens.Services;
using Core.IAM.Identity.Services;

namespace Core.IAM.Identity;
public static class Extensions
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddScoped<CurrentUserMiddleware>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IAuditService, AuditService>();
        services.BindDbContext<IdentityDbContext>();
        services.AddScoped<IDbInitializer, IdentityDbInitializer>();
        services.AddIdentity<ArcUser, ArcRole>(options =>
           {
               options.Password.RequiredLength = IdentityConstants.PasswordLength;
               options.Password.RequireDigit = false;
               options.Password.RequireLowercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.User.RequireUniqueEmail = true;
           })
           .AddEntityFrameworkStores<IdentityDbContext>()
           .AddDefaultTokenProviders();

        services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
        return services;
    }

    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var users = app.MapGroup("api/users").WithTags("users");
        users.MapUserEndpoints();

        var tokens = app.MapGroup("api/token").WithTags("token");
        tokens.MapTokenEndpoints();

        var roles = app.MapGroup("api/roles").WithTags("roles");
        roles.MapRoleEndpoints();

        return app;
    }
}
