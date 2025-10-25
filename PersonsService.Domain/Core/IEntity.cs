namespace PersonsService.Domain.Core;

public interface IEntity<in TEntity> : IAuditing
    where TEntity : class
{
    int Id { get; }
    bool IsPersisted();
}
