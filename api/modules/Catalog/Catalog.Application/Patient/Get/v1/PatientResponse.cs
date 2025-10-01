namespace Clinical.Application.Patients.Get.v1;
public sealed record PatientResponse(Guid? Id, string Name, string? Notes);
