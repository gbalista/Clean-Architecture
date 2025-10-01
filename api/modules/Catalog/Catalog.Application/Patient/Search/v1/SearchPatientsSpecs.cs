using Ardalis.Specification;
using Core.DataAccess.Paging;
using Core.DataAccess.Specifications;
using Clinical.Application.Patients.Get.v1;
using Clinical.Domain;

namespace Clinical.Application.Patients.Search.v1;
public class SearchPatientsSpecs : EntitiesByPaginationFilterSpec<Patient, PatientResponse>
{
    public SearchPatientsSpecs(SearchPatientsCommand command)
        : base(command) =>
        Query
            .OrderBy(c => c.Name, !command.HasOrderBy())
            .Where(b => b.Name.Contains(command.Keyword), !string.IsNullOrEmpty(command.Keyword));
}
