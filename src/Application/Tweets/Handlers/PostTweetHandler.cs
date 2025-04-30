using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Tweets.Handlers;

public class PostTweetHandler : IRequestHandler<PostTweetCommand, Guid>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly ILogger<PostTweetHandler> _logger;

    public PostTweetHandler(ITweetRepository tweetRepository, ILogger<PostTweetHandler> logger)
    {
        _tweetRepository = tweetRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(PostTweetCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
            throw new BadRequestException("Tweet content cannot be empty.");

        _logger.LogInformation("Posting new tweet for UserId: {UserId}", request.UserId);

        var tweet = new Tweet(request.UserId, request.Content);
        await _tweetRepository.CreateAsync(tweet);

        _logger.LogInformation("Tweet posted with Id: {TweetId}", tweet.Id);

        return tweet.Id;
    }
}
