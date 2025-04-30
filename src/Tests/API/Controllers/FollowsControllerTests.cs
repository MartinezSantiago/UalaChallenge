using API.Controllers;
using Application.Follows.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.API.Controllers;

/// <summary>
/// Unit tests for <see cref="API.Controllers.FollowsController"/>.
/// </summary>
public class FollowsControllerTests
{
    [Fact]
    public async Task FollowUser_ReturnsUnauthorized_When_NoUser()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new FollowsController(mediatorMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.FollowUser(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task FollowUser_ReturnsNoContent_When_Valid()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        mediatorMock
            .Setup(m => m.Send(It.IsAny<FollowUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        var controller = new FollowsController(mediatorMock.Object);

        var userGuid = Guid.NewGuid();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                                            new Claim(ClaimTypes.NameIdentifier, userGuid.ToString())
                                        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = await controller.FollowUser(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}