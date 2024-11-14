
namespace OnlineStory.Domain.Abstractions.Entities;

public interface IUserTracking
{
    public Guid CreateBy { get; set; }
    public Guid? UpdateBy { get; set; }

}
