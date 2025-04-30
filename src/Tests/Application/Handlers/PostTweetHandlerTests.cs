using Application.Tweets.Handlers;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Handlers;

/// <summary>
/// Unit tests for <see cref="PostTweetHandler"/>.
/// </summary>
public class PostTweetHandlerTests
{
    [Fact]
    public async Task Handle_Should_CreateTweet_When_ContentIsValid()
    {
        // Arrange
        var repositoryMock = new Mock<ITweetRepository>();
        var loggerMock = new Mock<ILogger<PostTweetHandler>>();
        var handler = new PostTweetHandler(repositoryMock.Object, loggerMock.Object);

        var command = new PostTweetCommand
        {
            UserId = Guid.NewGuid(),
            Content = "Hello World!"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        repositoryMock.Verify(r => r.CreateAsync(It.IsAny<Tweet>()), Times.Once);
    }
}