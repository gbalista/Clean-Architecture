using Core.Exceptions;

namespace Clinical.Domain.Exceptions;
public sealed class PatientNotFoundException : NotFoundException
{
    public PatientNotFoundException(Guid id)
        : base($"paciente com {id} não encontrado")
    {
    }
}
