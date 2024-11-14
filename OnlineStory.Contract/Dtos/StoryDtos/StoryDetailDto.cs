using OnlineStory.Contract.Dtos.CountryDtos;
using OnlineStory.Contract.Dtos.GenreDtos;

namespace OnlineStory.Contract.Dtos.StoryDtos
{
    public class StoryDetailDto
    {
        public Guid Id { get; set; }
        public string StoryTitle { get; set; }
        public string? AnotherStoryTitle { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string Thumbnail { get; set; }
        public string StoryStatus { get; set; }
        public string Audience { get; set; }
        public string Slug {  get; set; }
        public int View { get; set; }
        public int NumberOfFollowers { get; set; }
        public List<GenreDto>? Genres { get; set; }
        public CountryDto Country { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
