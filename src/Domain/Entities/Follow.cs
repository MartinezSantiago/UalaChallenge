using System;

namespace Domain.Entities;

public class Follow
{
    public Guid Id { get; private set; }
    public Guid FollowerId { get; private set; }
    public Guid FollowedId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Follow() { }

    public Follow(Guid followerId, Guid followedId)
    {
        if (followerId == followedId)
            throw new ArgumentException("Cannot follow yourself.");

        Id = Guid.NewGuid();
        FollowerId = followerId;
        FollowedId = followedId;
        CreatedAt = DateTime.UtcNow;
    }
}