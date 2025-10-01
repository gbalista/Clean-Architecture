using System.Reflection;
using FluentValidation;
using Core;
using Core.Origin;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Core.DataAccess.Persistence;
using Core.IAM.Auth;
using Core.IAM.Identity;
using Core.IAM;
using Core.IAM.Tenant;
using Core.IAM.Tenant.Endpoints;
using Core.IAM.Auth.Jwt;

namespace Core.IAM;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureIAMFramework(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ConfigureDatabase();
        builder.Services.ConfigureMultitenancy();
        builder.Services.ConfigureIdentity();
        builder.Services.ConfigureJwtAuth();

        // Define module assemblies
        var assemblies = new Assembly[]
        {
            typeof(IAMCore).Assembly,            
        };

        // Register validators
        builder.Services.AddValidatorsFromAssemblies(assemblies);
        
        //register mediatr
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
        });


        return builder;
    }

    public static WebApplication UseIAMFramework(this WebApplication app)
    {        
        app.UseMultitenancy();
        app.UseExceptionHandler();
        app.MapTenantEndpoints();
        app.MapIdentityEndpoints();

        // Current user middleware
        app.UseMiddleware<CurrentUserMiddleware>();

        return app;
    }
}
