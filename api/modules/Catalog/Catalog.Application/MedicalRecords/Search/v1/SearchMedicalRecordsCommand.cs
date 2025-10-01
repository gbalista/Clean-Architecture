using Core.DataAccess.Paging;
using Clinical.Application.MedicalRecords.Get.v1;
using MediatR;

namespace Clinical.Application.MedicalRecords.Search.v1;

public class SearchMedicalRecordsCommand : PaginationFilter, IRequest<PagedList<MedicalRecordResponse>>
{
    public Guid? PatientId { get; set; }    
}
