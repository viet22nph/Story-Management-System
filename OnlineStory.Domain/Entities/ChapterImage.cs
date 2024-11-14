

using OnlineStory.Domain.Abstractions;
using OnlineStory.Domain.Entities;

namespace Domain.Entities
{
    public class ChapterImage : EntityBase<int>
    {
        public int ChapterId { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset UploadedAt { get; set; } // Date and time the image was uploaded
        public virtual Chapter Chapter { get; set; }
        public ChapterImage(string imageUrl) {
            ImageUrl = imageUrl;
            UploadedAt = DateTimeOffset.UtcNow;
        }
        // Constructor with parameters
        public ChapterImage(int chapterId,string imageUrl)
        {
            ChapterId = chapterId;
            ImageUrl = !string.IsNullOrWhiteSpace(ImageUrl)? imageUrl: throw new ArgumentNullException(nameof(imageUrl));
            UploadedAt = DateTimeOffset.UtcNow;
        }
    }
}
