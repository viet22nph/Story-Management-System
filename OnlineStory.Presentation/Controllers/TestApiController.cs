

using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStory.Presentation.Abstractions;

namespace OnlineStory.Presentation.Controllers
{
    [Route("api/test")]
    public class TestApiController : ApiController
    {
        public TestApiController(ISender sender) : base(sender)
        {
        }
        [HttpGet]
        public ActionResult Test() {
            return Ok(new
            {
                Message = "Test success"
            });
        }
    }
}
