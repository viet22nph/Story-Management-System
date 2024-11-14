
namespace OnlineStory.Domain.Abstractions.Entities
{
    public interface IAuditable: IUserTracking, IDateTracking, ISoftDelete
    {
    }
}
