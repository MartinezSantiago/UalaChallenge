using Application.Follows.Commands;
using Application.Follows.Handlers;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Handlers;

/// <summary>
/// Unit tests for <see cref="FollowUserHandler"/>.
/// </summary>
public class FollowUserHandlerTests
{
    [Fact]
    public async Task Handle_Should_FollowUser_When_CommandIsValid()
    {
        // Arrange
        var repoMock = new Mock<IFollowRepository>();
        var loggerMock = new Mock<ILogger<FollowUserHandler>>();
        var handler = new FollowUserHandler(repoMock.Object, loggerMock.Object);

        var command = new FollowUserCommand
        {
            FollowerId = Guid.NewGuid(),
            FollowedId = Guid.NewGuid()
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        repoMock.Verify(r => r.FollowAsync(It.IsAny<Follow>()), Times.Once);
    }
}