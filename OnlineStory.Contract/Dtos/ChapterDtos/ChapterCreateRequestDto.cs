using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStory.Contract.Dtos.ChapterDtos;

public class ChapterCreateRequestDto
{
    public int ChapterNumber {  get; set; }
    public string ChapterTitle {  get; set; }
    public Guid StoryId {  get; set; }
    public List<ImageDto> Images {  get; set; }
}
public class ChapterUpdateRequestDto
{
    public int Id { get; set; }
    public int ChapterNumber { get; set; }
    public string ChapterTitle { get; set; }
    public Guid StoryId { get; set; }
    public List<ImagesUpdateDto> Images { get; set; }
}

public record ImageDto(IFormFile File, int Order);

public record ImagesUpdateDto(IFormFile? File, int Order, int? ImageChapterId);