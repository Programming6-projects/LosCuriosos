namespace DistributionCenter.Api.Controllers.Concretes;

using DistributionCenter.Api.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

[Route("api/reminders")]
public class RemindersController : BaseApiController
{
    [HttpGet()]
    public IActionResult Get()
    {
        return Ok("Hello from RemindersController");
    }
}
