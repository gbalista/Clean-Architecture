using ClArch.WebApi.Host;
using Clinical.Application;
using Core;
using Core.IAM;
using Core.Logging.Serilog;
using Serilog;

StaticLogger.EnsureInitialized();
Log.Information("server booting up..");
try
{
    var builder = WebApplication.CreateBuilder(args);

    

    builder.ConfigureArcFramework();
    builder.ConfigureIAMFramework();
    builder.RegisterModules();

    var app = builder.Build();
    
    //Inicializa as permissões dos módulos
    app.SetupPermissionsClinical();

    app.UseIAMFramework();
    app.UseArcFramework();    
    app.UseModules();
    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex.Message, "unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("server shutting down..");
    await Log.CloseAndFlushAsync();
}
