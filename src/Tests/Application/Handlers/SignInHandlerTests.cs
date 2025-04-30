using Application.Auth.Commands;
using Application.Auth.Handlers;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Infrastructure.Auth;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Handlers;

/// <summary>
/// Unit tests for <see cref="SignInHandler"/>.
/// </summary>
public class SignInHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnToken_When_CredentialsAreValid()
    {
        // Arrange
        var user = new User("testuser", BCrypt.Net.BCrypt.HashPassword("password"));
        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetByUsernameAsync("testuser")).ReturnsAsync(user);
        var jwtMock = new Mock<IJwtService>();
        jwtMock.Setup(j => j.GenerateToken(user)).Returns("token");

        var handler = new SignInHandler(repoMock.Object, jwtMock.Object);

        // Act
        var token = await handler.Handle(new SignInCommand { Username = "testuser", Password = "password" }, CancellationToken.None);

        // Assert
        token.Should().Be("token");
    }

    [Fact]
    public async Task Handle_Should_ThrowUnauthorized_When_InvalidCredentials()
    {
        // Arrange
        var repoMock = new Mock<IUserRepository>();
        repoMock.Setup(r => r.GetByUsernameAsync("wrong")).ReturnsAsync((User)null);
        var jwtMock = new Mock<IJwtService>();

        var handler = new SignInHandler(repoMock.Object, jwtMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedException>(()
            => handler.Handle(new SignInCommand { Username = "wrong", Password = "pass" }, CancellationToken.None));
    }
}