using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Application.UserCases.V1.Queries.Comment;
using OnlineStory.Contract.Dtos.CommentDtos;
using OnlineStory.Presentation.Abstractions;
using static OnlineStory.Contract.Services.V1.Comment.Command;
using static OnlineStory.Contract.Services.V1.Comment.Query;
namespace OnlineStory.Presentation.Controllers;

[Route("api/comment")]
public class CommentController : ApiController
{
    public CommentController(ISender sender) : base(sender)
    {
    }

    [HttpPost("chapter")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateNewCommentChapter([FromBody] CommentChapterRequest request)
    {

        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var command = new CreateCommentChapterCommand(request.Content, Guid.Parse(userId), request.ChapterId, request.CommentParent);
        var result = await sender.Send(command);
        return result.Match(_ => Created(), Problem);
    }
    [HttpPost("story")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateNewCommentStory([FromBody] CommentStoryRequest request)
    {

        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var command = new CreateCommentStoryCommand(request.Content, Guid.Parse(userId), request.StoryId, request.CommentParent);
        var result = await sender.Send(command);
        return result.Match(_ => Created(), Problem);
    }

    [HttpGet("chapter/{chapterId:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //cparent  => commentParent
    public async Task<IActionResult> GetCommentChapter(int chapterId, int? cparent,int pageIndex =1, int pageSize =20)
    {
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        if (pageSize < 0)
        {
            pageSize = 20;
        }
        var query = new GetCommentChapterQuery(chapterId, cparent, pageIndex, pageSize);
        var result = await sender.Send(query);
        return result.Match(data => Ok(data), Problem);
    }
    [HttpGet("story/{storyId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //cparent  => commentParent
    public async Task<IActionResult> GetCommentStory(Guid storyId, int? cparent, int pageIndex=1, int pageSize=20)
    {
        if (pageIndex < 1)
        {
            pageIndex = 1;
        }
        if (pageSize < 0)
        {
            pageSize = 20;
        }
        var query = new GetCommentStoryQuery(storyId, cparent, pageIndex, pageSize);
        var result = await sender.Send(query);
        return result.Match(data => Ok(data), Problem);
    }
    [HttpDelete("{id:int}/chapter/{chaperId:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCommentChapter(int id, int chaperId)
    {

        var command = new RemoveCommentChapterCommand(id, chaperId);
        var result = await sender.Send(command);
        return result.Match(_ => NoContent(), Problem);
    }
    [HttpDelete("{id:int}/story/{storyId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCommentStory(int id, Guid storyId)
    {

        var command = new RemoveCommentStoryCommand(id, storyId);
        var result = await sender.Send(command);
        return result.Match(_ => NoContent(), Problem);
    }
}
