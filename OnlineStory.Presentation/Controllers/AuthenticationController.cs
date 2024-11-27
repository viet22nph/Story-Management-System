using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Presentation.Abstractions;
using static OnlineStory.Contract.Services.V1.Authentication.Command;
using static OnlineStory.Contract.Services.V1.Authentication.Query;

namespace OnlineStory.Presentation.Controllers;
[Route("api/auth")]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender) : base(sender)
    {
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var result = await sender.Send(request);
        return result.Match(_ => Ok(new
        {
            Message = "Create new account successfully."
        }), Problem);
    }
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginQuery request)
    {
        var result = await sender.Send(request);
        return result.Match(data => Ok(data), Problem);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenQuery request)
    {
        var result = await sender.Send(request);
        return result.Match(data => Ok(data), Problem);
    }
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var result = await sender.Send(new GetMeQuery(Guid.Parse(userId)));
        return result.Match(data => Ok(data), Problem);
    }

}
