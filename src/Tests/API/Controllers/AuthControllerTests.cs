using API.Controllers;
using Application.Auth.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.API.Controllers;

/// <summary>
/// Unit tests for <see cref="API.Controllers.AuthController"/>.
/// </summary>
public class AuthControllerTests
{
    [Fact]
    public async Task SignUp_ReturnsCreatedAtAction()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<SignUpCommand>(), default)).ReturnsAsync(userId);

        var controller = new AuthController(mediatorMock.Object);

        // Act
        var result = await controller.SignUp(new SignUpCommand()) as CreatedAtActionResult;

        // Assert
        result!.Value.Should().BeEquivalentTo(new { id = userId });
    }

    [Fact]
    public async Task SignIn_ReturnsOkWithToken()
    {
        // Arrange
        var token = "abc123";
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(m => m.Send(It.IsAny<SignInCommand>(), default)).ReturnsAsync(token);

        var controller = new AuthController(mediatorMock.Object);

        // Act
        var result = await controller.SignIn(new SignInCommand()) as OkObjectResult;

        // Assert
        result!.Value.Should().BeEquivalentTo(new { token });
    }
}