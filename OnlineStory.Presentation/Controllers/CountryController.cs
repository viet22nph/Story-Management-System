

using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Contract.Extensions;
using OnlineStory.Contract.Share;
using OnlineStory.Presentation.Abstractions;
using static OnlineStory.Contract.Services.V1.Authentication.Command;
using static OnlineStory.Contract.Services.V1.Country.Query;

namespace OnlineStory.Presentation.Controllers;

[Route("api/country")]
public class CountryController : ApiController
{
    public CountryController(ISender sender) : base(sender)
    {
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountries(string? sortColumn, string? sortBy)
    {
        var sortOrder = sortBy.GetSortOrder();
        var result = await sender.Send(new GetCountiesQuery(sortColumn, sortOrder));
        return result.Match(data => Ok(data), Problem);
    }
}
