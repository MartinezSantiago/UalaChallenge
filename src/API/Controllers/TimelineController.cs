using Application.Timeline.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers;

public class TimelineController : BaseApiController
{
    private readonly IMediator _mediator;

    public TimelineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeline()
    {
        var userId = GetUserId();
        if (userId is null)
            return Unauthorized();

        var query = new GetTimelineQuery { UserId = userId.Value };
        var timeline = await _mediator.Send(query);

        return Ok(timeline);
    }
}
