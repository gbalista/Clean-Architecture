using Clinical.Application.Patients.Get.v1;

namespace Clinical.Application.MedicalRecords.Get.v1;
public sealed record MedicalRecordResponse(Guid? Id, string Name, string? Description, PatientResponse? Patient);
