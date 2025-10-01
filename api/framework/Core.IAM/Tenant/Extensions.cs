using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.Stores.DistributedCacheStore;
using Core.IAM.Tenant.Persistence;
using Core.IAM.Tenant.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Core.DataAccess.Persistence.Services;
using Core.Persistence.Tenant;
using Core.DataAccess.Persistence;
using Core.IAM.Tenant.Abstractions;
using Core.DataAcess.Persistence;
using Core.Auth.Identity;

namespace Core.IAM.Tenant;
public static class Extensions
{
    public static IServiceCollection ConfigureMultitenancy(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddTransient<IConnectionStringValidator, ConnectionStringValidator>();
        services.BindDbContext<TenantDbContext>();
        services
            .AddMultiTenant<ArcTenantInfo>(config =>
            {
                // to save database calls to resolve tenant
                // this was happening for every request earlier, leading to ineffeciency
                config.Events.OnTenantResolveCompleted = async (context) =>
                {
                    if (context.MultiTenantContext.StoreInfo is null) return;
                    if (context.MultiTenantContext.StoreInfo.StoreType != typeof(DistributedCacheStore<ArcTenantInfo>))
                    {
                        var sp = ((HttpContext)context.Context!).RequestServices;
                        var distributedCacheStore = sp
                            .GetService<IEnumerable<IMultiTenantStore<ArcTenantInfo>>>()!
                            .FirstOrDefault(s => s.GetType() == typeof(DistributedCacheStore<ArcTenantInfo>));

                        await distributedCacheStore!.TryAddAsync(context.MultiTenantContext.TenantInfo!);
                    }
                    await Task.FromResult(0);
                };
            })
            .WithClaimStrategy(ArcClaims.Tenant)
            .WithHeaderStrategy(TenantConstants.Identifier)
            .WithDelegateStrategy(async context =>
            {
                if (context is not HttpContext httpContext)
                    return null;
                if (!httpContext.Request.Query.TryGetValue("tenant", out var tenantIdentifier) || string.IsNullOrEmpty(tenantIdentifier))
                    return null;
                return await Task.FromResult(tenantIdentifier.ToString());
            })
            .WithDistributedCacheStore(TimeSpan.FromMinutes(60))
            .WithEFCoreStore<TenantDbContext, ArcTenantInfo>();
        services.AddScoped<ITenantService, TenantService>();
        return services;
    }

    public static WebApplication UseMultitenancy(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);
        app.UseMultiTenant();

        // set up tenant store
        var tenants = TenantStoreSetup(app);

        // set up tenant databases
        app.SetupTenantDatabases(tenants);

        return app;
    }

    private static IApplicationBuilder SetupTenantDatabases(this IApplicationBuilder app, IEnumerable<ArcTenantInfo> tenants)
    {
        foreach (var tenant in tenants)
        {
            // create a scope for tenant
            using var tenantScope = app.ApplicationServices.CreateScope();

            //set current tenant so that the right connection string is used
            tenantScope.ServiceProvider.GetRequiredService<IMultiTenantContextSetter>()
                .MultiTenantContext = new MultiTenantContext<ArcTenantInfo>()
                {
                    TenantInfo = tenant
                };

            // using the scope, perform migrations / seeding
            var initializers = tenantScope.ServiceProvider.GetServices<IDbInitializer>();
            foreach (var initializer in initializers)
            {
                initializer.MigrateAsync(CancellationToken.None).Wait();
                initializer.SeedAsync(CancellationToken.None).Wait();
            }
        }
        return app;
    }

    private static IEnumerable<ArcTenantInfo> TenantStoreSetup(IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();

        // tenant master schema migration
        var tenantDbContext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
        if (tenantDbContext.Database.GetPendingMigrations().Any())
        {
            tenantDbContext.Database.Migrate();
            Log.Information("applied database migrations for tenant module");
        }

        // default tenant seeding
        if (tenantDbContext.TenantInfo.Find(TenantConstants.Root.Id) is null)
        {
            var rootTenant = new ArcTenantInfo(
                TenantConstants.Root.Id,
                TenantConstants.Root.Name,
                string.Empty,
                TenantConstants.Root.EmailAddress);

            rootTenant.SetValidity(DateTime.UtcNow.AddYears(1));
            tenantDbContext.TenantInfo.Add(rootTenant);
            tenantDbContext.SaveChanges();
            Log.Information("configured default tenant data");
        }

        // get all tenants from store
        var tenantStore = scope.ServiceProvider.GetRequiredService<IMultiTenantStore<ArcTenantInfo>>();
        var tenants = tenantStore.GetAllAsync().Result;

        //dispose scope
        scope.Dispose();

        return tenants;
    }
}
