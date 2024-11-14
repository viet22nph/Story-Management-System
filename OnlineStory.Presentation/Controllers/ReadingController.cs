

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Contract.Dtos.ReadingHistory;
using OnlineStory.Presentation.Abstractions;
using static OnlineStory.Contract.Services.V1.ReadingHistory.Command;
using static OnlineStory.Contract.Services.V1.ReadingHistory.Query;

namespace OnlineStory.Presentation.Controllers
{
    [Route("api/reading")]
    public class ReadingController : ApiController
    {
        public ReadingController(ISender sender) : base(sender)
        {
        }
        [HttpPost("mark-as-read")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MarkChapterAsRead([FromBody] MarkChapterAsReadRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var command = new MarkChapterAsReadCommand(Guid.Parse(userId), request.StoryId, request.ChapterId);
            var result = await sender.Send(command);
            return result.Match(_ => Created(), Problem);
        }
        [HttpGet("story-history")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserReadingHistory(int pageIndex =1, int pageSize=20)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetUserRedingHistoryQuery(Guid.Parse(userId), pageIndex,   pageSize);
            var result = await sender.Send(query);
            return result.Match(data=> Ok(data), Problem);
        }
    }
}
