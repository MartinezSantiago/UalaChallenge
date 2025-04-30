using Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        var validationResult = ValidationProblemIfInvalid();
        if (validationResult != null) return validationResult;

        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(SignUp), new { id = userId }, new { id = userId });
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
    {
        var validationResult = ValidationProblemIfInvalid();
        if (validationResult != null) return validationResult;

        var token = await _mediator.Send(command);
        return Ok(new { token });
    }
}
