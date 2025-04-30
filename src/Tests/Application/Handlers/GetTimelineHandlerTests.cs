using Application.Common.Interfaces;
using Application.Timeline.Handlers;
using Application.Timeline.Queries;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Handlers;

/// <summary>
/// Unit tests for <see cref="GetTimelineHandler"/>.
/// </summary>
public class GetTimelineHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnTimelineFromCache_When_CacheExists()
    {
        // Arrange
        var tweets = new List<Tweet> { new Tweet(Guid.NewGuid(), "Cached Tweet") };
        var cacheMock = new Mock<ICacheService>();
        cacheMock.Setup(c => c.GetAsync<IEnumerable<Tweet>>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(tweets);

        var tweetRepoMock = new Mock<ITweetRepository>();
        var followRepoMock = new Mock<IFollowRepository>();
        var loggerMock = new Mock<ILogger<GetTimelineHandler>>();

        var handler = new GetTimelineHandler(tweetRepoMock.Object, followRepoMock.Object, cacheMock.Object, loggerMock.Object);

        // Act
        var result = await handler.Handle(new GetTimelineQuery { UserId = Guid.NewGuid() }, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(tweets);
    }
}