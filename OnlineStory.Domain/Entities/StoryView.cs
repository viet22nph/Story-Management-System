

using Microsoft.EntityFrameworkCore;
using OnlineStory.Domain.Abstractions;

namespace OnlineStory.Domain.Entities;

public class StoryView: EntityBase<int>
{
    public Guid StoryId { get; set; }
    public int Count { get; set; }
    public DateTime ViewDate { get; set; }
    public Story Story { get; set; }

    public StoryView(DateTime viewDate)
    {
        ViewDate = viewDate;
        Count = 1;
    }

}
