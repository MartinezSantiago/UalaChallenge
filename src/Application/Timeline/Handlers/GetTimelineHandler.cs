using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Timeline.Queries;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Timeline.Handlers;

public class GetTimelineHandler : IRequestHandler<GetTimelineQuery, IEnumerable<Tweet>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IFollowRepository _followRepository;
    private readonly ICacheService _cacheService;
    private readonly ILogger<GetTimelineHandler> _logger;

    public GetTimelineHandler(
        ITweetRepository tweetRepository,
        IFollowRepository followRepository,
        ICacheService cacheService,
        ILogger<GetTimelineHandler> logger)
    {
        _tweetRepository = tweetRepository;
        _followRepository = followRepository;
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<IEnumerable<Tweet>> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetTimeline for UserId: {UserId}", request.UserId);

        var cacheKey = CacheKeyBuilder.Timeline(request.UserId);
        var cachedTimeline = await _cacheService.GetAsync<IEnumerable<Tweet>>(cacheKey, cancellationToken);

        if (cachedTimeline != null && cachedTimeline.Any())
        {
            _logger.LogInformation("Returning timeline from cache for UserId: {UserId}", request.UserId);
            return cachedTimeline;
        }

        var followedIds = await _followRepository.GetFollowedUserIdsAsync(request.UserId);
        var allTweets = new List<Tweet>();

        foreach (var followedId in followedIds)
        {
            var tweets = await _tweetRepository.GetTweetsByUserIdAsync(followedId);
            allTweets.AddRange(tweets);
        }

        var orderedTweets = allTweets.OrderByDescending(t => t.CreatedAt).Take(50).ToList();

        await _cacheService.SetAsync(cacheKey, orderedTweets, TimeSpan.FromMinutes(5), cancellationToken);

        _logger.LogInformation("Timeline generated and cached for UserId: {UserId}", request.UserId);

        return orderedTweets;
    }
}
