namespace PersonsService.Domain.Exceptions;

public class CannotChangeStatusOfADeletedEntityException : DomainException
{
    public CannotChangeStatusOfADeletedEntityException() : base("Não é possível mudar o status de uma entidade deletada.")
    {
    }
}

