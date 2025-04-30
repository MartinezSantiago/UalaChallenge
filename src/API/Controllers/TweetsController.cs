using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers;

public class TweetsController : BaseApiController
{
    private readonly IMediator _mediator;

    public TweetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PostTweet([FromBody] PostTweetCommand command)
    {
        var validationResult = ValidationProblemIfInvalid();
        if (validationResult != null) return validationResult;

        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        command.UserId = userId.Value;

        var tweetId = await _mediator.Send(command);
        return CreatedAtAction(nameof(PostTweet), new { id = tweetId }, new { id = tweetId });
    }
}
