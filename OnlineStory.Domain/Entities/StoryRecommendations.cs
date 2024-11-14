

using Domain.Entities.Identity;
using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Abstractions.Entities;

namespace OnlineStory.Domain.Entities;

public class StoryRecommendations: EntityBase<Guid>,IDateTracking
{
    public Guid UserId { get; set; }
    public Guid StoryId { get; set; }
    public double Score {  get; set; }
    public DateTimeOffset CreatedDate { get ; set ; }
    public DateTimeOffset? ModifiedDate { get ; set ; }
    public virtual Story Story { get; set; }
    public virtual AppUser User { get; set; }


}
