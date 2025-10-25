namespace PersonsService.Domain.Core;

public interface IAuditing
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}

