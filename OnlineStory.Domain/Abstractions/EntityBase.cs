
using MediatR;
using OnlineStory.Domain.Abstractions.Entities;

namespace OnlineStory.Domain.Abstractions
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {

        public TKey Id { get; set; }

        private int? _requestedHashCode;
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(this.Id, default);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<TKey>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            EntityBase<TKey> item = (EntityBase<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return EqualityComparer<TKey>.Default.Equals(item.Id, this.Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(EntityBase<TKey> left, EntityBase<TKey> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(EntityBase<TKey> left, EntityBase<TKey> right)
        {
            return !(left == right);
        }
    }
}
