using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ITweetRepository
{
    Task CreateAsync(Tweet tweet);
    Task<IEnumerable<Tweet>> GetTweetsByUserIdAsync(Guid userId);
}