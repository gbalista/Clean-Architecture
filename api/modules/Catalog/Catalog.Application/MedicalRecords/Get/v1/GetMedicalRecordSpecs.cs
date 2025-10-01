using Ardalis.Specification;
using Clinical.Domain;

namespace Clinical.Application.MedicalRecords.Get.v1;

public class GetMedicalRecordSpecs : Specification<MedicalRecord, MedicalRecordResponse>
{
    public GetMedicalRecordSpecs(Guid id)
    {
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Patient);
    }
}
