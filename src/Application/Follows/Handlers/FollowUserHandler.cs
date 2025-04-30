using Application.Follows.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Follows.Handlers;

public class FollowUserHandler : IRequestHandler<FollowUserCommand, Unit>
{
    private readonly IFollowRepository _followRepository;
    private readonly ILogger<FollowUserHandler> _logger;

    public FollowUserHandler(IFollowRepository followRepository, ILogger<FollowUserHandler> logger)
    {
        _followRepository = followRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UserId {FollowerId} follows UserId {FollowedId}", request.FollowerId, request.FollowedId);

        var follow = new Follow(request.FollowerId, request.FollowedId);
        await _followRepository.FollowAsync(follow);

        return Unit.Value;
    }
}
