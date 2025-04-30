using Application.Follows.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers;

public class FollowsController : BaseApiController
{
    private readonly IMediator _mediator;

    public FollowsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{userIdToFollow}")]
    public async Task<IActionResult> FollowUser(Guid userIdToFollow)
    {
        var followerId = GetUserId();
        if (followerId is null)
            return Unauthorized();

        var command = new FollowUserCommand
        {
            FollowerId = followerId.Value,
            FollowedId = userIdToFollow
        };

        await _mediator.Send(command);
        return NoContent();
    }
}
