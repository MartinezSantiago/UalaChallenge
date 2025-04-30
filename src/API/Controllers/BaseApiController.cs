using API.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public abstract class BaseApiController : ControllerBase
{
    protected Guid? GetUserId() => User.GetUserId();

    protected IActionResult ValidationProblemIfInvalid()
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        return null!;
    }
}
