using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class TweetRepository : ITweetRepository
{
    private readonly ApplicationDbContext _context;

    public TweetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Tweet tweet)
    {
        _context.Tweets.Add(tweet);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Tweet>> GetTweetsByUserIdAsync(Guid userId)
    {
        return await _context.Tweets
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}