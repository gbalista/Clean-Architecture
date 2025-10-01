using MediatR;

namespace Clinical.Application.Patients.Get.v1;
public class GetPatientRequest : IRequest<PatientResponse>
{
    public Guid Id { get; set; }
    public GetPatientRequest(Guid id) => Id = id;
}
