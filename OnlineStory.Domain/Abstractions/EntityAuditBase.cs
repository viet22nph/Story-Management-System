using OnlineStory.Domain.Abstractions.Entities;

namespace OnlineStory.Domain.Abstractions;

public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IAuditable
{
    public Guid CreateBy { get ; set ; }
    public Guid? UpdateBy { get ; set ; }
    public DateTimeOffset CreatedDate { get ; set ; }
    public DateTimeOffset? ModifiedDate { get ; set ; }
    public bool IsDeleted { get ; set ; }
    public DateTimeOffset? DeleteAt { get ; set ; }
}
