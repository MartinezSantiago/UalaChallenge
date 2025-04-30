using MediatR;
using Domain.Entities;
using System.Collections.Generic;
using System;

namespace Application.Timeline.Queries;

public class GetTimelineQuery : IRequest<IEnumerable<Tweet>>
{
    public Guid UserId { get; set; }
}