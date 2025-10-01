using Ardalis.Specification;
using Core.DataAccess.Paging;
using Core.DataAccess.Specifications;
using Clinical.Application.MedicalRecords.Get.v1;
using Clinical.Domain;

namespace Clinical.Application.MedicalRecords.Search.v1;
public class SearchMedicalRecordsSpecs : EntitiesByPaginationFilterSpec<MedicalRecord, MedicalRecordResponse>
{
    public SearchMedicalRecordsSpecs(SearchMedicalRecordsCommand command)
        : base(command) =>
        Query
            .Include(p => p.Patient)
            .OrderBy(c => c.Title, !command.HasOrderBy())
            .Where(p => p.PatientId == command.PatientId!.Value, command.PatientId.HasValue);
}
