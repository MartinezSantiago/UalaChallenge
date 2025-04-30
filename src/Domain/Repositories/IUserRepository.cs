using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsByUsernameAsync(string username);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(Guid id);
    Task CreateAsync(User user);
}