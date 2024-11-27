

using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Entities;

namespace Domain.Entities
{
    public class ChapterImage : EntityBase<int>
    {
        public int ChapterId { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public DateTimeOffset UploadedAt { get; set; } // Date and time the image was uploaded
        public virtual Chapter Chapter { get; set; }
        public ChapterImage(string imageUrl, int order) {
            ImageUrl = imageUrl;
            Order = order;
            UploadedAt = DateTimeOffset.UtcNow;
        }
        // Constructor with parameters
    }
}
