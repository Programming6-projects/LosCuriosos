namespace DistributionCenter.Api.Controllers.Interfaces;

using Microsoft.AspNetCore.Mvc;

public interface IApiController
{
    ActionResult Problem(IList<string> errors);
}
