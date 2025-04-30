using System;

namespace Application.Common.Helpers;

public static class CacheKeyBuilder
{
    public static string Timeline(Guid userId) => $"timeline:{userId}";
    public static string UserProfile(Guid userId) => $"userProfile:{userId}";
    public static string TweetsByUser(Guid userId) => $"tweets:{userId}";
}
