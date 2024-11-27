
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Contract.Dtos.ChapterDtos;
using OnlineStory.Domain.Security;
using OnlineStory.Presentation.Abstractions;
using OnlineStory.Presentation.Attributes;
using static OnlineStory.Contract.Services.V1.Chapter.Command;
using static OnlineStory.Contract.Services.V1.Chapter.Query;
namespace OnlineStory.Presentation.Controllers;

[Route("api/chapter")]
public class ChapterController : ApiController
{
    public ChapterController(ISender sender) : base(sender)
    {
    }
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [AppAuthorize(resource:Resources.StoryManager, action:Actions.Create)]
    public async Task<IActionResult> CreateChapter([FromForm] ChapterCreateRequestDto request)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var command = new CreateChapterCommand(request.ChapterNumber, request.ChapterTitle, request.StoryId, request.Images, Guid.Parse(userId));
        var result = await sender.Send(command);
        return result.Match(_ => Created(), Problem);
    }
    [HttpGet("{storySlug}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChaptersPagination(string storySlug, int pageIndex = 1, int pageSize = 50)
    {
        var query = new GetChapterListPaginationQuery(storySlug, pageIndex, pageSize);
        var result = await sender.Send(query);
        return result.Match(data => Ok(new
        {
            Data = data
        }), Problem);
    }
    [HttpGet("{storySlug}/_all")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChapters(string storySlug)
    {
        var query = new GetChapterListQuery(storySlug);
        var result = await sender.Send(query);
        return result.Match(data => Ok(new {Chapters = data}), Problem);
    }

    [HttpGet("{storySlug}/{chapterSlug}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChapter(string storySlug, string chapterSlug)
    {
        var query = new GetChapterBySlugQuery(storySlug, chapterSlug);
        var result = await sender.Send(query);
        return result.Match(data => Ok(data), Problem);
    }

    [HttpGet("{storySlug}/_list")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStoryChapterListOnly(string storySlug)
    {
        var query = new GetStoryChapterListOnlyQuery(storySlug);
        var result = await sender.Send(query);
        return result.Match(data => Ok( data), Problem);
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AppAuthorize(resource: Resources.StoryManager, action: Actions.Update)]
    public async Task<IActionResult> UpdateChapter([FromForm] ChapterUpdateRequestDto request)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var command = new UpdateChapterCommand(request.Id, request.ChapterNumber, request.ChapterTitle, request.StoryId, request.Images, Guid.Parse(userId));
        var result = await sender.Send(command);
        return result.Match(_ => NoContent(), Problem);   
    }


}
