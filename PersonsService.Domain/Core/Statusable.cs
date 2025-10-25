using PersonsService.Domain.Enum;
using PersonsService.Domain.Exceptions;

namespace PersonsService.Domain.Core;

public abstract class Statusable<TEntity> : Entity<TEntity>
    where TEntity : class
{
    public Status Status { get; set; }

    public bool IsActive => Status == Status.Active;
    public bool IsInactive => Status == Status.Inactive;
    public bool IsBlocked => Status == Status.Blocked;
    public bool IsDeleted => Status == Status.Delete;

    public virtual void ChangeStatus(Status status)
    {
        if (IsDeleted)
        {
            throw new CannotChangeStatusOfADeletedEntityException();
        }

        Status = status;
    }

    public virtual void Activate()
    {
        ChangeStatus(Status.Active);
    }
    public virtual void Inactivate()
    {
        ChangeStatus(Status.Inactive);
    }
    public virtual void Block()
    {
        ChangeStatus(Status.Blocked);
    }
}
