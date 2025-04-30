using API.Controllers;
using Application.Timeline.Queries;
using Domain.Entities;
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
/// Unit tests for <see cref="API.Controllers.TimelineController"/>.
/// </summary>
public class TimelineControllerTests
{
    [Fact]
    public async Task GetTimeline_ReturnsUnauthorized_When_NoUser()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new TimelineController(mediatorMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        // Act
        var result = await controller.GetTimeline();

        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task GetTimeline_ReturnsOk_When_Valid()
    {
        // Arrange
        var sampleTweets = new[] { new Tweet(Guid.NewGuid(), "hi") };
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<GetTimelineQuery>(), default)).ReturnsAsync(sampleTweets);

        var controller = new TimelineController(mediatorMock.Object);

        var userGuid = Guid.NewGuid();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, userGuid.ToString())
        }, "mock"));
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = await controller.GetTimeline() as OkObjectResult;

        // Assert
        result!.Value.Should().BeEquivalentTo(sampleTweets);
    }
}