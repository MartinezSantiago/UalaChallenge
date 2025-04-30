using Application.Common.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Auth;
using Moq;
using System;
using Xunit;

namespace Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for <see cref="JwtService"/>.
/// </summary>
public class JwtServiceTests
{
    [Fact]
    public void Implements_IJwtService_Interface()
    {
        typeof(IJwtService).IsAssignableFrom(typeof(JwtService)).Should().BeTrue();
    }
}