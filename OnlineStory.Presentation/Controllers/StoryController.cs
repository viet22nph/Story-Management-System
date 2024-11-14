

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Contract.Extensions;
using OnlineStory.Contract.Share;
using OnlineStory.Domain.Entities.Identity;
using OnlineStory.Domain.Security;
using OnlineStory.Presentation.Abstractions;
using OnlineStory.Presentation.Attributes;
using StackExchange.Redis;
using static OnlineStory.Contract.Services.V1.Story.Command;
using static OnlineStory.Contract.Services.V1.Story.Query;
using static OnlineStory.Contract.Services.V1.UserFollowStory.Command;
using static OnlineStory.Contract.Services.V1.UserFollowStory.Query;

namespace OnlineStory.Presentation.Controllers;
[Route("api/story")]
public class StoryController : ApiController
{
    public StoryController(ISender sender) : base(sender)
    {
    }

    [AppAuthorize(resource: Resources.StoryManager, action: Actions.Create)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateNewStory([FromForm] CreateStoryCommand request)
    {
        var result = await sender.Send(request);
        return result.Match(_ =>Created(), Problem);
    }
    [HttpPut("{storyId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateStory(Guid storyId,[FromForm] UpdateStoryCommand request)
    {
        request = request with { StoryId = storyId };
        var result = await sender.Send(request);
        return result.Match(_ => NoContent(), Problem);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStories(string? searchTerm = null, string? sortColumn = null, string? orderBy = null, int? pageIndex = 1, int? pageSize = 10)
    {
        if (pageIndex <= 0)
        {
            pageIndex = 1;
        }
        if (pageSize <= 0 || pageSize >= 100)
        {
            pageSize = 10;
        }
        var sort = orderBy.GetSortOrder();
        var query = new GetStoriesQuery(searchTerm, sortColumn, sort, pageIndex ?? 1, pageSize ?? 10);
        var rs = await sender.Send(query);
        return rs.Match(data => Ok(data), Problem);
    }
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStoryById(Guid id)
    {
        var query = new GetStoryByIdQuery(id);
        var rs = await sender.Send(query);
        return rs.Match(data => Ok(data), Problem);
    }
    [HttpGet("{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStoryBySlug(string slug)
    {
        var query = new GetStoryBySlugQuery(slug);
        var rs = await sender.Send(query);
        return rs.Match(data => Ok(data), Problem);
    }
    [HttpGet("genre/{genreSlug}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStoriesByGenreSlug(string genreSlug, string? sortColumn = null, string? sortBy = null, string? status = null, int? pageIndex =1, int? pageSize =20)
    {
        if (pageIndex <= 0)
        {
            pageIndex = 1;
        }
        if (pageSize <= 0 || pageSize >= 100)
        {
            pageSize = 10;
        }
        var sort = sortBy.GetSortOrder();
        var query = new GetStoriesByGenreQuery(genreSlug, status, sortColumn, sort, pageIndex ??1, pageSize?? 20 );
        var rs = await sender.Send(query);
        return rs.Match(data => Ok(data), Problem);
    }

    [HttpPost("increment-view/{storyId}")]
    public async Task<IActionResult> IncrementViewStory(Guid storyId)
    {
        var command = new IncrementViewCountStoryCommand(storyId);
        var  result = await sender.Send(command);
        return result.Match(_=> NoContent(), Problem);
    }
    [HttpPost("{storyId:guid}/follow")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> FollowStory(Guid storyId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var command = new FollowStoryCommand(storyId, Guid.Parse(userId));
        var result = await sender.Send(command);
        return result.Match(_ => NoContent(), Problem);
    }
    [HttpGet("followed-stories")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> StoriesUserFollow(int? pageIndex = 1, int? pageSize = 20)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var query = new GetStoriesUserFollowQuery(Guid.Parse(userId), pageIndex ?? 1,pageSize?? 20 );
        var result = await sender.Send(query);  
        return result.Match(data => Ok(data), Problem);
    }
}
