using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class FollowRepository : IFollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task FollowAsync(Follow follow)
    {
        _context.Follows.Add(follow);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Guid>> GetFollowedUserIdsAsync(Guid followerId)
    {
        return await _context.Follows
            .Where(f => f.FollowerId == followerId)
            .Select(f => f.FollowedId)
            .ToListAsync();
    }
}