

namespace OnlineStory.Domain.Abstractions.Entities;

    
    public interface IEntityBase<T>
    {
        public T Id { get; set; }
    }
