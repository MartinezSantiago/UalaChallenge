using Application.Common.Interfaces;
using FluentAssertions;
using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Infrastructure.Services;

/// <summary>
/// Unit tests for <see cref="CacheService"/>.
/// </summary>
public class CacheServiceTests
{
    [Fact]
    public async Task Implements_ICacheService_Interface()
    {
        typeof(ICacheService).IsAssignableFrom(typeof(CacheService)).Should().BeTrue();
    }
}