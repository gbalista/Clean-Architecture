using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAcess.Persistence;
using Core.IAM.Identity.Permissions;
using Core.Persistence.Tenant;
using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Builder;

namespace Clinical.Application;

public static class Extensions
{
    public static IApplicationBuilder SetupPermissionsClinical(this IApplicationBuilder app)
    {
        ArcPermissions.AddIfNotExists(
            new("View Patients", ArcActions.View, "Patients", IsBasic: true),
            new("Search Patients", ArcActions.Search, "Patients", IsBasic: true),
            new("Create Patients", ArcActions.Create, "Patients"),
            new("Update Patients", ArcActions.Update, "Patients"),
            new("Delete Patients", ArcActions.Delete, "Patients"),

            new("View Medical Records", ArcActions.View, "MedicalRecords", IsBasic: true),
            new("Search Medical Records", ArcActions.Search, "MedicalRecords", IsBasic: true),
            new("Create Medical Records", ArcActions.Create, "MedicalRecords"),
            new("Update Medical Records", ArcActions.Update, "MedicalRecords"),
            new("Delete Medical Records", ArcActions.Delete, "MedicalRecords")            
        );

        return app;
    }
}
