
namespace OnlineStory.Domain.Abstractions.Entities;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeleteAt { get; set; }
    public void Undo() {
        IsDeleted = false;
        DeleteAt = null;
    }
}
