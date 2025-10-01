using MediatR;

namespace Clinical.Application.MedicalRecords.Get.v1;
public class GetMedicalRecordRequest : IRequest<MedicalRecordResponse>
{
    public Guid Id { get; set; }
    public GetMedicalRecordRequest(Guid id) => Id = id;
}
