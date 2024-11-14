


using OnlineStory.Contract.Extensions;
using OnlineStory.Domain.Abstractions;

namespace OnlineStory.Domain.Entities
{
    public class Genre : EntityBase<int>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug {  get; set; }
        private List<StoryGenre> _storyGenres;
        public IReadOnlyCollection<StoryGenre> StoryGenres => _storyGenres.AsReadOnly();
        private Genre()
        {
            _storyGenres = new List<StoryGenre>();
            
        }
        public Genre(string name, string description): this()
        {
            Name = !string.IsNullOrWhiteSpace(name) ?name: throw new ArgumentNullException(nameof(name));
            Description = !string.IsNullOrWhiteSpace(description) ? description : throw new ArgumentNullException(nameof(Description));
            Slug = Name.ToSlug();
        }
    }
}
    