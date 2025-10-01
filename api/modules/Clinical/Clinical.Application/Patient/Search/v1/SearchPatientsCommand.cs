using Clinical.Application.Patients.Get.v1;
using MediatR;
using Core.DataAccess.Paging;

namespace Clinical.Application.Patients.Search.v1;

public class SearchPatientsCommand : PaginationFilter, IRequest<PagedList<PatientResponse>>
{
    public string? Name { get; set; }
    public string? Notes { get; set; }
}
