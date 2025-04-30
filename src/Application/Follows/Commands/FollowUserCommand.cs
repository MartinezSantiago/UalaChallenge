using MediatR;
using System;

namespace Application.Follows.Commands;

public class FollowUserCommand : IRequest<Unit>
{
    public Guid FollowerId { get; set; }
    public Guid FollowedId { get; set; }
}