
namespace OnlineStory.Domain.Entities;

public class StoryGenre
{
    public Guid StoryId { get; set; }
    public int GenreId { get; set; }
    public virtual Story Story { get; set; }
    public virtual Genre Genre { get; set; }

    public StoryGenre()
    {

    }
    public StoryGenre(Story story, Genre genre)
    {
        StoryId = story.Id;
        GenreId = genre.Id;
        Genre = genre;
        Story = story;
    }
}
