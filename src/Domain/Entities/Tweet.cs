using System;

namespace Domain.Entities;

public class Tweet
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    private Tweet() { }

    public Tweet(Guid userId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.");

        if (content.Length > 280)
            throw new ArgumentException("Content exceeds 280 characters.");

        Id = Guid.NewGuid();
        UserId = userId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }
}