using API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Tests.API.Controllers;

/// <summary>
/// Unit tests for <see cref="API.Controllers.TweetsController"/>.
/// </summary>
public class TweetsControllerTests
{
    [Fact]
    public async Task PostTweet_ReturnsUnauthorized_When_NoUser()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new TweetsController(mediatorMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.PostTweet(new PostTweetCommand());

        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task PostTweet_ReturnsCreated_When_Valid()
    {
        // Arrange
        var tweetId = Guid.NewGuid();
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<PostTweetCommand>(), default)).ReturnsAsync(tweetId);

        var controller = new TweetsController(mediatorMock.Object);

        var userGuid = Guid.NewGuid();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, userGuid.ToString())
        }, "mock"));
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var command = new PostTweetCommand { Content = "test" };

        // Act
        var result = await controller.PostTweet(command) as CreatedAtActionResult;

        // Assert
        result!.Value.Should().BeEquivalentTo(new { id = tweetId });
    }
}