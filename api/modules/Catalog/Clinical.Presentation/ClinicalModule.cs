using Carter;
using Core.DataAcess.Persistence;
using Core.DataAccess.Persistence;
using Clinical.Domain;
using Clinical.Presentation.Endpoints.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Clinical.Persistence;

namespace ClArch.Starter.WebApi.Catalog.Infrastructure;
public static class ClinicalModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("clinical") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var productGroup = app.MapGroup("medicalRecords").WithTags("medicalRecords");
            productGroup.MapMedicalRecordCreationEndpoint();
            productGroup.MapGetMedicalRecordEndpoint();
            productGroup.MapGetMedicalRecordListEndpoint();
            productGroup.MapMedicalRecordUpdateEndpoint();
            productGroup.MapMedicalRecordDeleteEndpoint();

            var brandGroup = app.MapGroup("patients").WithTags("patients");
            brandGroup.MapPatientCreationEndpoint();
            brandGroup.MapGetPatientEndpoint();
            brandGroup.MapGetPatientListEndpoint();
            brandGroup.MapPatientUpdateEndpoint();
            brandGroup.MapPatientDeleteEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterClinicalServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<ClinicalDbContext>();
        builder.Services.AddScoped<IDbInitializer, ClinicalDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<MedicalRecord>, ClinicalRepository<MedicalRecord>>("clinical:medicalRecords");
        builder.Services.AddKeyedScoped<IReadRepository<MedicalRecord>, ClinicalRepository<MedicalRecord>>("clinical:medicalRecords");
        builder.Services.AddKeyedScoped<IRepository<Patient>, ClinicalRepository<Patient>>("clinical:patients");
        builder.Services.AddKeyedScoped<IReadRepository<Patient>, ClinicalRepository<Patient>>("clinical:patients");
        return builder;
    }
    public static WebApplication UseClinicalModule(this WebApplication app)
    {
        return app;
    }
}
