using Core.Exceptions;

namespace Clinical.Domain.Exceptions;
public sealed class MedicalRecordNotFoundException : NotFoundException
{
    public MedicalRecordNotFoundException(Guid id)
        : base($"prontu�rio com {id} n�o encontrado")
    {
    }
}
