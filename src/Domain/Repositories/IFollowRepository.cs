using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IFollowRepository
{
    Task FollowAsync(Follow follow);
    Task<IEnumerable<Guid>> GetFollowedUserIdsAsync(Guid followerId);
}